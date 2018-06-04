using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cure.Http.OAuth.Abstractions;

namespace Cure.Http.OAuth
{
    /// <summary>
    /// A <see cref="DelegatingHandler" /> wrapper for <see cref="ITokenNegotiator" /> for token management when accessing
    /// resources protected by OAuth.
    /// </summary>
    public class AccessTokenDelegatingHandler : DelegatingHandler
    {
        private readonly ITokenNegotiator _negotiator;

        public AccessTokenDelegatingHandler(ITokenNegotiator negotiator) => _negotiator = negotiator;

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // 1. Get token
            var token = await _negotiator.GetToken(cancellationToken);
            // 2. Validate
            var validate = _negotiator.Validate(token);
            if (validate.Failed)
            {
                // Initiate negoiatition
                token = await _negotiator.RenewToken(validate, cancellationToken);
            }

            // 3. Set access token
            _negotiator.SetAccessToken(request, token.AccessToken);
            // 4. Execute request
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode != HttpStatusCode.Unauthorized)
            {
                // Accessed to protected resource was successful
                return response;
            }

            // Read WWW-Authenticate Header value and act
            validate.ParseWwwAuthenticate(response.GetWwwAuthenticateHeader());
            // Dispose previous attempt since no longer brings any value
            response.Dispose();
            // 5. Renew token
            token = await _negotiator.RenewToken(validate, cancellationToken);
            // 6. Set access token
            _negotiator.SetAccessToken(request, token.AccessToken);
            // 7. Execute request
            response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                await _negotiator.UpdateToken(token, cancellationToken);
            }

            return response;
        }
    }
}