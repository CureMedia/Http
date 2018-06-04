using System.Threading;
using System.Threading.Tasks;

namespace Cure.Http.OAuth.Abstractions
{
    public interface ITokenManager
    {
        Task<Token> Token(CancellationToken cancellationToken = default(CancellationToken));
    }
}