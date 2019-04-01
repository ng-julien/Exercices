namespace Demo.Authentication.Models
{
    using System;

    internal class OktaToken
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public int ExpiresIn { get; set; }

        public bool IsValidAndNotExpiring =>
            !string.IsNullOrEmpty(this.AccessToken) && this.ExpiresAt > DateTime.UtcNow.AddSeconds(30);

        public string Scope { get; set; }

        public string TokenType { get; set; }
    }
}