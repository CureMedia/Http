using System.Threading.Tasks;

namespace Cure.Http.OAuth.Abstractions
{
    public interface ITokenStore
    {
        Task<Token> Get();

        Task Update(Token token);
    }
}