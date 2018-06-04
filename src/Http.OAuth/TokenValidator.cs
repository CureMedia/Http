using System;
using Cure.Http.OAuth.Abstractions;

namespace Cure.Http.OAuth
{
    /// <inheritdoc />
    public class TokenValidator : ITokenValidator
    {
        /// <inheritdoc />
        public TokenValidationResult Validate(Token token, ISystemClock clock)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (IsMissingToken(token))
            {
                return TokenValidationResult.IsMissingToken(token);
            }

            if (HasTokenExpired(token.ExpiresAt, clock))
            {
                return TokenValidationResult.TokenExpired(token);
            }
            return TokenValidationResult.Valid(token);
        }

        protected static bool IsMissingToken(Token token) => string.IsNullOrEmpty(token.AccessToken);

        protected virtual bool HasTokenExpired(
            DateTime? expirationDate,
            ISystemClock clock)
        {
            if (expirationDate.HasValue)
            {
                return clock.Now >= expirationDate.Value;
            }

            return false;
        }
    }    
}
