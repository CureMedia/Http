namespace Cure.Http.OAuth.Abstractions
{
    /// <summary>
    /// Provider for <see cref="IAccessMethod"/>
    /// </summary>
    public interface IAccessMethodProvider
    {
        /// <summary>
        /// Create the appropriate <see cref="IAccessMethod"/> based on <paramref name="method"/>.
        /// </summary>
        /// <param name="method">Access method name.</param>
        /// <returns></returns>
        IAccessMethod Create(string method);
    }
}