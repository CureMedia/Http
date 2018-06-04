using System;

namespace Cure.Http.OAuth
{
    public class TokenNegotiatorOptions
    {

        public Action<Token> OnTokenRenewed { get; set; }
    }
}
