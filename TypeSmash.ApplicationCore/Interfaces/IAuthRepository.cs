using System.Threading.Tasks;
using TypeSmash.ApplicationCore.Entities;

namespace TypeSmash.ApplicationCore.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserOut> EmailLogin(string email, string password);
        Task<UserOut> UsernameLogin(string email, string password);
        Task<UserOut> Register(string email, string username, string password);
    }
}