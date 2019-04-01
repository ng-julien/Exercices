namespace Microsoft.Extensions.DependencyInjection
{
    using Configuration;

    using Demo.Authentication.Queries;
    using Demo.Authentication.Settings;

    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection ConfigureAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IGetTokenQuery, GetTokenQuery>();
            return services;
        }
    }
}