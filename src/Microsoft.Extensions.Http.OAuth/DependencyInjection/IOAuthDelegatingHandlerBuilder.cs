using Cure.Http.OAuth.Abstractions;

namespace Microsoft.Extensions.Http.OAuth.DependencyInjection
{
    public interface IOAuthDelegatingHandlerBuilder
    {
        /// <summary>
        /// Name of the Http client.
        /// </summary>
        string ClientName { get; }

        string OptionsName { get; }

        /// <summary>
        /// Name of the <see cref="AccessMethod" /> to use.
        /// </summary>
        string AccessMethod { get; }

        string TokenEndpoint { get; }

        string ClientId { get; }

        string ClientSecret { get; }

        ITokenStore TokenStore { get; }
    }
}