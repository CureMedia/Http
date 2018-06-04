using System.Threading;
using System.Threading.Tasks;

namespace Cure.Http.OAuth.Abstractions
{
    /// <summary>
    /// Interface to request a new <see cref="Token" /> from the OAuth token endpoint.
    /// </summary>
    public interface ITokenClient
    {
        /// <summary>
        /// Request a new <see cref="Token" /> for <paramref name="token" />.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TokenRequestResult> Request(
            Token token,
            CancellationToken cancellationToken);
    }
}