namespace Demo.Api.Core.Settings
{
    using System.Collections.Generic;

    public class OAuthSettings
    {
        public string Audience => this.ClientId;

        public string CallbackPath { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Issuer { get; set; }

        public string ResponseType { get; set; }

        public List<string> Scopes { get; set; }
    }
}