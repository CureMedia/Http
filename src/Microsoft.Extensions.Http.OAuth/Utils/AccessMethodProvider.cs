using System;
using System.Collections.Generic;
using System.Linq;
using Cure.Http.OAuth.Abstractions;

namespace Microsoft.Extensions.Http.OAuth
{
    /// <inheritdoc />
    /// <summary>
    /// Default factory implementation of <see cref="T:Cure.Http.OAuth.Abstractions.IAccessMethodProvider" />.
    /// </summary>
    /// <remarks>
    /// Returns either <see cref="F:Microsoft.Extensions.Http.OAuth.AccessMethod.Header" /> or
    /// <see cref="F:Microsoft.Extensions.Http.OAuth.AccessMethod.QueryString" />.
    /// </remarks>
    public class AccessMethodProvider : IAccessMethodProvider
    {
        /// <inheritdoc />
        public IAccessMethod Create(string method)
        {
            foreach (var accessMethod in Methods())
            {
                if (accessMethod.Name.Equals(method))
                {
                    return accessMethod.Method;
                }
            }

            throw new ArgumentNullException(nameof(method),
                $"No implementation of {typeof(IAccessMethod).Name} was found for {method}. " +
                $"Available methods: {string.Join(",", Methods().Select(_ => _.Name))}");
        }

        protected virtual IEnumerable<AccessMethod> Methods()
        {
            yield return AccessMethod.Header;
            yield return AccessMethod.QueryString;
        }
    }
}