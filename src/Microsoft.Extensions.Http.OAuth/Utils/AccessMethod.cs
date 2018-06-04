using Cure.Http.OAuth;
using Cure.Http.OAuth.Abstractions;

namespace Microsoft.Extensions.Http.OAuth
{
    /// <summary>
    /// Service descriptor to simply configuration scenarios when specifying API protection resource implementations.
    /// </summary>
    public class AccessMethod
    {
        public static readonly AccessMethod QueryString =
            new AccessMethod("query", new BearerToken.QueryStringAccessMethod());

        public static readonly AccessMethod Header =
            new AccessMethod("header", new BearerToken.AuthorizationHeaderAccessMethod());

        protected AccessMethod(string name, IAccessMethod method)
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
    }
}