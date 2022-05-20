using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HousingDbContext _db;

        public UserRepository(HousingDbContext db)
        {
            _db = db;
        }
        public async Task<User> Authenticate(string Username, string Password)
        {
            return await _db.Users.FirstOrDefaultAsync(x=>x.UserName == Username && 
                                                          x.Password ==Password);
        }
    }
}