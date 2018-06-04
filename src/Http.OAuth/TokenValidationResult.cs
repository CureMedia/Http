namespace Cure.Http.OAuth
{
    public class TokenValidationResult
    {
        public TokenValidationResult(Token token) => Token = token;

        public string Message { get; private set; }

        public Token Token { get; }

        public bool Failed { get; private set; } = true;

        public bool Succeeded => !Failed;

        public static TokenValidationResult IsMissingToken(Token token)
        {
            return new TokenValidationResult(token)
            {
                Message = "MissingAccessToken"
            };
        }

        public static TokenValidationResult TokenExpired(Token token)
        {
            return new TokenValidationResult(token)
            {
                Message = "TokenExpired"
            };
        }

        public static TokenValidationResult Valid(Token token)
        {
            return new TokenValidationResult(token)
            {
                Failed = false
            };
        }

        /// <summary>
        /// Act upon The WWW-Authenticate Response Header Field as described by https://tools.ietf.org/html/rfc6750#section-3
        /// </summary>
        /// <param name="header"></param>
        public virtual void ParseWwwAuthenticate(string header)
        {
            // Protocol is {realm,scope}=<value>
            //if (header.StartsWith("realm"))
            //{
            //    var host = header.Substring("realm".Length + 1);
            //}
            //else if (header.StartsWith("scope"))
            //{
            //    var scope = header.Substring("scope".Length + 1);
            //}
        }

        /// <summary>
        /// Ensure that the validation is successfull. Throws exception otherwise
        /// </summary>
        public virtual void EnsureValidStatus()
        {

        }
    }
}