namespace Cure.Http.OAuth
{
    /// <summary>
    /// Extensions methods for common task performed on <see cref="TokenValidationResult" />.
    /// </summary>
    public static partial class TokenValidationResultExtensions
    {
        /// <summary>
        /// Check if the <paramref name="result" /> failed due to missing <c>access_token</c>.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsMissingAccessToken(this TokenValidationResult result)
        {
            return string.Equals(result.Message, "MissingAccessToken");
        }

        /// <summary>
        /// Check fi the <paramref name="result" /> failed due to token expiration.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool HasTokenExpired(this TokenValidationResult result)
        {
            return string.Equals(result.Message, "TokenExpired");
        }
    }
}