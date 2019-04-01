namespace Demo.Api.Core.Settings
{
    public class OAuthSettings
    {
        public string Audience => this.ClientId;

        public string CallbackPath { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Issuer { get; set; }

        public string ResponseType { get; set; }

        public string[] Scopes { get; set; }
    }
}