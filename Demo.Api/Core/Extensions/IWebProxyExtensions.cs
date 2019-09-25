namespace Demo.Api.Core.Extensions
{
    using System;
    using System.Net;

    internal static class IWebProxyExtensions
    {
        public static Uri GetAddress(this IWebProxy proxy)
        {
            return (proxy as WebProxy)?.Address;
        }

        public static TCredentials GetCredential<TCredentials>(this IWebProxy webProxy)
            where TCredentials : class, ICredentials
        {
            return webProxy.Credentials as TCredentials;
        }
    }
}