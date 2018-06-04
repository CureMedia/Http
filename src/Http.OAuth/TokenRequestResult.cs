namespace Cure.Http.OAuth
{
    public class TokenRequestResult
    {
        public TokenRequestResult(Token token) => Token = token;

        private TokenRequestResult()
        {
        }

        public bool Succeeded => !Failed;

        public bool Failed => Token == null;

        public Token Token { get; }

        public string Message { get; private set; }

        public static TokenRequestResult FromError(string message)
        {
            return new TokenRequestResult
            {
                Message = message
            };
        }
    }
}