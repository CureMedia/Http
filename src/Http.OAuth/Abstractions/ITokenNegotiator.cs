using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Cure.Http.OAuth.Abstractions
{
    /// <summary>
    /// Facade for access token management when accessing resources protected by OAuth.
    /// </summary>
    public interface ITokenNegotiator
    {
        /// <summary>
        /// Update the <see cref="ITokenStore" /> with <paramref name="token" />.
        /// </summary>
        /// <param name="token">Token to update.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateToken(Token token, CancellationToken cancellationToken);

        /// <summary>
        /// Set the <paramref name="accessToken" /> as defined by <see cref="IAccessMethod" /> implementor.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="accessToken"></param>
        void SetAccessToken(HttpRequestMessage request, string accessToken);

        /// <summary>
        /// Get the <see cref="Token" /> from <see cref="ITokenStore" />.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Token> GetToken(CancellationToken cancellationToken);

        /// <summary>
        /// Validate the <paramref name="token" />.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        TokenValidationResult Validate(Token token);

        /// <summary>
        /// Renew the <see cref="Token" /> for <paramref name="validationResult" />.
        /// </summary>
        /// <param name="validationResult">Instance of <see cref="TokenValidationResult" />.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Token> RenewToken(TokenValidationResult validationResult, CancellationToken cancellationToken);
    }
}