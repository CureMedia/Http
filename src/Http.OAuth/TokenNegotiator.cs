using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cure.Http.OAuth.Abstractions;

namespace Cure.Http.OAuth
{
    public class TokenNegotiator : ITokenNegotiator
    {
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1);
        private readonly TokenNegotiatorOptions _options;

        protected TimeSpan Timeout = TimeSpan.FromSeconds(5);

        public TokenNegotiator(TokenNegotiatorOptions options) => _options = options;

        public IAccessMethodProvider AccessMethodProvider { get; set; }

        public BearerToken.MethodType MethodType { get; set; }

        public IAccessMethod AccessMethod => AccessMethodProvider.Create(MethodType.Name);

        public ITokenStore TokenStore { get; set; }

        public ITokenValidator Validator { get; set; }

        public ITokenClient Client { get; set; }

        public async Task UpdateToken(Token token, CancellationToken cancellationToken)
        {
            await TokenStore.Update(token);
        }

        public void SetAccessToken(HttpRequestMessage request, string accessToken)
        {
            AccessMethod.Set(request, accessToken);
        }

        public async Task<Token> GetToken(CancellationToken cancellationToken)
        {
            // Acquire lock
            if (await _lock.WaitAsync(Timeout, cancellationToken).ConfigureAwait(false))
            {
                try
                {
                    return await TokenStore.Get().ConfigureAwait(false);
                }
                finally
                {
                    _lock.Release();
                }
            }

            return null;
        }

        public TokenValidationResult Validate(Token token)
        {
            return Validator.Validate(token, SystemClock.Default);
        }

        public async Task<Token> RenewToken(
            TokenValidationResult validationResult, 
            CancellationToken cancellationToken)
        {
            var token = validationResult.Token;
            var result = await Client.Request(token, cancellationToken);
            if (result.Succeeded)
            {
                _options.OnTokenRenewed?.Invoke(result.Token);
            }

            return result.Token;
        }

        private sealed class SystemClock : ISystemClock
        {
            public static readonly SystemClock Default = new SystemClock();

            public DateTime Now => DateTime.UtcNow;

            public DateTimeOffset NowOffset => DateTimeOffset.Now;
        }
    }
}