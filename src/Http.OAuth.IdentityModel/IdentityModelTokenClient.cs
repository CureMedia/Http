using System;
using System.Threading;
using System.Threading.Tasks;
using Cure.Http.OAuth.Abstractions;
using IdentityModel.Client;

namespace Cure.Http.OAuth.IdentityModel
{
    /// <inheritdoc />
    /// <summary>
    /// Request a <see cref="T:Cure.Http.OAuth.Token" /> using <see cref="P:Cure.Http.OAuth.IdentityModel.IdentityModelTokenClientAdapter.TokenClient" />.
    /// </summary>
    public class IdentityModelTokenClientAdapter : ITokenClient
    {
        /// <summary>
        /// Create an instance of <see cref="IdentityModelTokenClientAdapter"/> using <paramref name="tokenClient"/>.
        /// </summary>
        /// <param name="tokenClient">The client to request a new token.</param>
        public IdentityModelTokenClientAdapter(TokenClient tokenClient) =>
            TokenClient = tokenClient;

        public TokenClient TokenClient { get; }

        /// <inheritdoc />
        public async Task<TokenRequestResult> Request(
            Token token, 
            CancellationToken cancellationToken)
        {
            TokenResponse response;
            if (token.HasRefreshToken)
            {
                response =
                    await TokenClient.RequestRefreshTokenAsync(token.RefreshToken,
                        cancellationToken: cancellationToken);
            }
            else
            {
                response = await TokenClient.RequestClientCredentialsAsync(cancellationToken: cancellationToken);
            }

            return FromResponse(response);
        }

        private static TokenRequestResult FromResponse(TokenResponse response)
        {
            if (response.IsError)
            {
                var message = $"[{response.ErrorType.ToString()}] {response.Error} - {response.ErrorDescription}";
                return TokenRequestResult.FromError(message);
            }

            var token = new Token
            {
                AccessToken = response.AccessToken,
                ExpiresAt = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
                RefreshToken = response.RefreshToken,
                TokenType = response.TokenType
            };
            return new TokenRequestResult(token);
        }
    }
}