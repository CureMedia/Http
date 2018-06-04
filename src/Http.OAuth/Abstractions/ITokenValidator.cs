namespace Cure.Http.OAuth.Abstractions
{
    /// <summary>
    /// Validator for <see cref="Token"/>.
    /// </summary>
    public interface ITokenValidator
    {
        /// <summary>
        /// Validate the <paramref name="token"/>.
        /// </summary>
        /// <param name="token">Token to validate.</param>
        /// <param name="clock">Instance of <see cref="ISystemClock"/> to validate with.</param>
        /// <returns>An <see cref="TokenValidationResult"/> containing the result of validation.</returns>
        TokenValidationResult Validate(Token token, ISystemClock clock);
    }
}