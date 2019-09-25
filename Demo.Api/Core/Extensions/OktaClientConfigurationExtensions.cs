namespace Demo.Api.Core.Extensions
{
    using System.Net;

    using Okta.Sdk.Configuration;

    internal static class OktaClientConfigurationExtensions
    {
        public static void SetProxy(this OktaClientConfiguration configuration, IWebProxy proxy)
        {
            var credentials = proxy.GetCredential<NetworkCredential>();
            if (credentials == null)
            {
                return;
            }

            configuration.Proxy = new ProxyConfiguration
                                      {
                                          Password = credentials.Password,
                                          Username = credentials.UserName,
                                          Host = proxy.GetAddress().ToString()
                                      };
        }
    }
}