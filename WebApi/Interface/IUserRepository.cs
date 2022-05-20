using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interface
{
    public interface IUserRepository
    {
         Task<User> Authenticate(string Username,string Password);
    }
}