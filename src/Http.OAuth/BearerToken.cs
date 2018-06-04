using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Cure.Http.OAuth.Abstractions;

namespace Cure.Http.OAuth
{
    /// <summary>
    /// Methods to access resources protected by OAuth2 as described in https://tools.ietf.org/html/rfc6750
    /// </summary>
    public static class BearerToken
    {
        /// <summary>
        /// Simply configuration scenarios when specifying API protection resource implementations.
        /// </summary>
        public class MethodType : IAccessMethodProvider
        {
            public static readonly MethodType QueryString = new MethodType("query", new QueryStringAccessMethod());

            public static readonly MethodType Header = new MethodType("header", new AuthorizationHeaderAccessMethod());

            protected MethodType(string name, IAccessMethod method)
            {
                Name = name;
                Method = method;
            }

            /// <summary>
            /// Method name
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// Implementation to set <c>access_token</c>.
            /// </summary>
            public IAccessMethod Method { get; }

            /// <inheritdoc />
            IAccessMethod IAccessMethodProvider.Create(string method)
            {
                return Method;
            }
        }

        /// <summary>
        /// Grant access to proteced resources via query parameter <c>access_token</c> as described in
        /// https://tools.ietf.org/html/rfc6750#section-2.3
        /// </summary>
        public sealed class QueryStringAccessMethod : IAccessMethod
        {
            public const string ParameterKey = "access_token";

            /// <inheritdoc />
            public void Set(HttpRequestMessage request, string accessToken)
            {
                var uri = request.RequestUri;
                var accessTokenUri = $"{uri}" +
                                     $"{(string.IsNullOrEmpty(uri.Query) ? "?" : "&")}" +
                                     $"{ParameterKey}={WebUtility.UrlDecode(accessToken)}";
                request.RequestUri = new Uri(accessTokenUri);
            }

            /// <inheritdoc />
            public string Get(HttpRequestMessage request)
            {
                var query = request.RequestUri.Query;
                if (string.IsNullOrEmpty(query))
                {
                    return string.Empty;
                }

                var @params = HttpUtility.ParseQueryString(query);
                return @params.Get(ParameterKey);
            }
        }

        /// <summary>
        /// Grant access to proteced resources via Authorization header as described in
        /// https://tools.ietf.org/html/rfc6750#section-2.1
        /// </summary>
        public sealed class AuthorizationHeaderAccessMethod : IAccessMethod
        {
            public const string Scheme = "Bearer";

            /// <inheritdoc />
            public void Set(HttpRequestMessage request, string accessToken)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(Scheme, accessToken);
            }

            /// <inheritdoc />
            public string Get(HttpRequestMessage request)
            {
                return request.Headers.Authorization.Parameter;
            }
        }
    }
}