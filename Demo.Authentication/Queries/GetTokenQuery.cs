namespace Demo.Authentication.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Authentication;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;

    using Models;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using Settings;

    public interface IGetTokenQuery
    {
        Task<string> Get();
    }

    internal class GetTokenQuery : IGetTokenQuery
    {
        private readonly IOptions<Okta> oktaSettings;

        private OktaToken token = new OktaToken();

        public GetTokenQuery(IOptions<Okta> oktaSettings)
        {
            this.oktaSettings = oktaSettings;
        }

        public async Task<string> Get()
        {
            if (!this.token.IsValidAndNotExpiring)
            {
                this.token = await this.GetNewAccessToken();
            }

            return this.token.AccessToken;
        }

        private async Task<OktaToken> GetNewAccessToken()
        {
            var httpClientHandler = new HttpClientHandler
                                        {
                                            Proxy = new WebProxy(
                                                        "http://PRAFAPXYVIPADM.siege.axa-fr.intraxa:8080")
                                                        {
                                                            Credentials
                                                                = CredentialCache
                                                                    .DefaultNetworkCredentials,
                                                        }
                                        };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12;
            using (var client = new HttpClient(httpClientHandler))
            {
                var clientId = this.oktaSettings.Value.ClientId;
                var clientSecret = this.oktaSettings.Value.ClientSecret;
                var clientCredential = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(clientCredential));

                var postMessage = new Dictionary<string, string>
                                      {
                                          { "grant_type", "client_credentials" },
                                          { "scope", "openId" }
                                      };
                var request = new HttpRequestMessage(HttpMethod.Post, this.oktaSettings.Value.TokenUrl)
                                  {
                                      Content = new FormUrlEncodedContent(postMessage)
                                  };

                try
                {
                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var snakeCaseSerializerSettings = new JsonSerializerSettings
                                                              {
                                                                  ContractResolver = new DefaultContractResolver
                                                                                         {
                                                                                             NamingStrategy =
                                                                                                 new
                                                                                                     SnakeCaseNamingStrategy()
                                                                                         }
                                                              };
                        var oktaToken = JsonConvert.DeserializeObject<OktaToken>(json, snakeCaseSerializerSettings);
                        oktaToken.ExpiresAt = DateTime.UtcNow.AddSeconds(this.token.ExpiresIn);
                        return oktaToken;
                    }

                    this.token.AccessToken = await response.Content.ReadAsStringAsync();
                }
                catch (Exception exception)
                {
                    throw exception;
                }

                throw new AuthenticationException("Unable to retrieve access token from Okta");
            }
        }
    }
}