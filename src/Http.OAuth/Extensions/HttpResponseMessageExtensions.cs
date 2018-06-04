using System.Net.Http;

namespace Cure.Http.OAuth
{
    /// <summary>
    /// Extensions methods for <see cref="HttpResponseMessage"/> instances.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Get the first non null and empty value from <paramref name="message" /> WWW-Authenticate header.
        /// </summary>
        /// <param name="message">Response message.</param>
        /// <returns>Header parameter if exists; otherwise <code>null</code>.</returns>
        public static string GetWwwAuthenticateHeader(this HttpResponseMessage message)
        {
            using (var enumerator = message.Headers.WwwAuthenticate.GetEnumerator())
            {
                do
                {
                    var header = enumerator.Current;
                    if (!string.IsNullOrEmpty(header?.Parameter))
                    {
                        return header.Parameter;
                    }
                } while (enumerator.MoveNext());
            }

            return null;
        }
    }
}