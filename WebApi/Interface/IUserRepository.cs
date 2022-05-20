using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interface
{
    public interface IUserRepository
    {
         Task<User> Authenticate(string Username,string Password);
         void Register(string userName,string password);
         Task<bool> UserAlreadyExists(string userName);
    }
}