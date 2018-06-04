using System;
using Cure.Http.OAuth.Abstractions;
using Cure.Http.OAuth.IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using AccessTokenDelegatingHandler = Cure.Http.OAuth.AccessTokenDelegatingHandler;

namespace Microsoft.Extensions.Http.OAuth
{
    public static class OAuthHttpClientConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientBuilder"></param>
        /// <param name="accessMethod"></param>
        /// <param name="tokenEndpoint"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tokenStore"></param>
        /// <returns></returns>
        public static void AddOAuth(
            this IHttpClientBuilder clientBuilder,
            AccessMethod accessMethod,
            string tokenEndpoint,
            string clientId,
            string clientSecret,
            ITokenStore tokenStore)
        {
            clientBuilder.AddOAuth(
                accessMethod.Name,
                tokenEndpoint,
                clientId,
                clientSecret,
                tokenStore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientBuilder"></param>
        /// <param name="accessMethod"></param>
        /// <param name="tokenEndpoint"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tokenStore"></param>
        /// <returns></returns>
        public static void AddOAuth(
            this IHttpClientBuilder clientBuilder,
            string accessMethod,
            string tokenEndpoint,
            string clientId,
            string clientSecret,
            ITokenStore tokenStore)
        {
            if (string.IsNullOrEmpty(accessMethod))
            {
                throw new ArgumentNullException(nameof(accessMethod));
            }
            if (string.IsNullOrEmpty(tokenEndpoint))
            {
                throw new ArgumentNullException(nameof(tokenEndpoint));
            }
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentNullException(nameof(clientId));
            }
            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new ArgumentNullException(nameof(clientSecret));
            }

            if (tokenStore == null)
            {
                throw new ArgumentNullException(nameof(tokenStore));
            }

            clientBuilder.Services.AddScoped(_ => tokenStore);
            clientBuilder.Services.AddScoped(s =>
            {
                var client = new TokenClient(
                    tokenEndpoint,
                    clientId,
                    clientSecret);
                return new IdentityModelTokenClientAdapter(client);
            });
            clientBuilder.Services.AddScoped(s =>
            {
                var tokenManager = s.GetRequiredService<ITokenNegotiator>();
                return new AccessTokenDelegatingHandler(tokenManager);
            });
            clientBuilder.AddHttpMessageHandler<AccessTokenDelegatingHandler>();
        }
    }
}