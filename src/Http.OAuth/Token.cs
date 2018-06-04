using System;

namespace Cure.Http.OAuth
{
    /// <summary>
    /// OAuth2 values related to <c>access_token</c> and <c>refresh_token</c>.
    /// </summary>
    public class Token
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string TokenType { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public bool HasRefreshToken => !string.IsNullOrEmpty(RefreshToken);

        public bool HasExpirationDate => ExpiresAt.HasValue;
    }
}