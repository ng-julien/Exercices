namespace Demo.Api
{
    using System;
    using System.Net;
    using System.Security.Claims;

    using Controllers;

    using Core;
    using Core.Constraints;
    using Core.Extensions;
    using Core.Routing;
    using Core.Settings;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Logging;
    using Microsoft.IdentityModel.Tokens;

    using Newtonsoft.Json;

    using Okta.Sdk;
    using Okta.Sdk.Configuration;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller}/{action}/{id?}",
                            defaults: new { controller = "Home", action = "Index" });
                    });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var credentials = CredentialCache.DefaultNetworkCredentials;
            var proxyUrl = this.Configuration.GetValue<string>("Proxy");
            if (!string.IsNullOrWhiteSpace(proxyUrl))
            {
                WebRequest.DefaultWebProxy = new WebProxy(proxyUrl)
                                                 {
                                                     Credentials = credentials
                                                 };
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12 |
                                                   SecurityProtocolType.Tls13;

            var oauthSettings = this.Configuration.GetSection(nameof(OAuthSettings)).Get<OAuthSettings>();

            var oktaClientConfiguration = this.Configuration.GetSection(nameof(OktaClientConfiguration))
                                              .Get<OktaClientConfiguration>();
            oktaClientConfiguration.SetProxy(WebRequest.DefaultWebProxy);
            services.AddTransient<IClaimsTransformation, GroupsToRolesTransformation>()
                    .AddScoped<IOktaClient>(_ => new OktaClient(oktaClientConfiguration))
                    .AddAuthentication(
                        options =>
                            {
                                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                            })
                    .AddCookie()
                    .AddOpenIdConnect(
                        options =>
                            {
                                options.ClientId = oauthSettings.ClientId;
                                options.ClientSecret = oauthSettings.ClientSecret;
                                options.Authority = oauthSettings.Issuer;
                                options.CallbackPath = oauthSettings.CallbackPath;
                                options.ResponseType = oauthSettings.ResponseType;
                                options.SaveTokens = true;
                                options.UseTokenLifetime = false;
                                options.GetClaimsFromUserInfoEndpoint = true;

                                oauthSettings.Scopes.ForEach(options.Scope.Add);

                                options.TokenValidationParameters = new TokenValidationParameters
                                                                        {
                                                                            ValidateIssuer = true,
                                                                            RoleClaimType = ClaimTypes.Role,
                                                                            ValidIssuer = oauthSettings.Issuer,
                                                                            ValidateAudience = true,
                                                                            ValidAudience = oauthSettings.Audience,
                                                                            ClockSkew = TimeSpan.FromMinutes(5),
                                                                            RequireSignedTokens = true,
                                                                            RequireExpirationTime = true,
                                                                            ValidateLifetime = true
                                                                        };
                            })
                    .Services
                    .Configure<RouteOptions>(
                        routeOptions =>
                            {
                                var constraintMap = routeOptions.ConstraintMap;
                                constraintMap.Add(
                                    "enum",
                                    typeof(EnumConstraint));
                            })
                    .ConfigureInfrastructure(this.Configuration)
                    .ConfigureApplication()
                    .Configure<CookiePolicyOptions>(
                        options =>
                            {
                                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                                options.CheckConsentNeeded = context => true;
                                options.MinimumSameSitePolicy = SameSiteMode.None;
                            })
                    .AddMvc(
                        options =>
                            {
                                options.Conventions.Add(
                                    new GenericControllerRouteConvention()
                                );
                            })
                    .AddNewtonsoftJson(
                        options => options.SerializerSettings.ReferenceLoopHandling =
                                       ReferenceLoopHandling.Ignore)
                    .ConfigureApplicationPartManager(
                        m =>
                            m.FeatureProviders.Add(
                                new GenericControllerFeatureProvider(typeof(AnimalsController<,,>))
                            ));
        }
    }
}