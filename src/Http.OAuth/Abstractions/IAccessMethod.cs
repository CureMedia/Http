using System.Net.Http;

namespace Cure.Http.OAuth.Abstractions
{
    /// <summary>
    /// Grant access to protected resources.
    /// </summary>
    public interface IAccessMethod
    {
        /// <summary>
        /// Authorize <paramref name="request" /> with <paramref name="accessToken" />.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage" /> that request access.</param>
        /// <param name="accessToken">Token to access protected resource.</param>
        void Set(HttpRequestMessage request, string accessToken);

        /// <summary>
        /// Get the <c>access_token</c> from <paramref name="request" />.
        /// </summary>
        /// <param name="request">The rquest to extract <c>access_token</c> from.</param>
        /// <returns>The <c>access_token</c> if present; otherwise <c>null</c>.</returns>
        string Get(HttpRequestMessage request);
    }
}