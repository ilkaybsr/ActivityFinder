using Business.DTO;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IUserService
    {
        Task<bool> CreateUser(CreateUserViewModel createUser);

        Task<UserLoginResult> Login(UserLoginViewModel request);
    }
}
