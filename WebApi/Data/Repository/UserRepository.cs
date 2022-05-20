using System.Linq;
using System.Security.Cryptography;
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
        public async Task<User> Authenticate(string Username, string PasswordText)
        {
            var user= await _db.Users.FirstOrDefaultAsync(x=>x.UserName == Username );
            if(user==null || user.PasswordKey==null)
                return null;

            if(! MatchPasswordHash(PasswordText,user.Password,user.PasswordKey))    
                return null;
            return user;    

        }

        public void Register(string userName, string password)
        {
            byte[] passwordHash,passwordKey;
            using(var hmac=new HMACSHA512())
            {
                passwordKey=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            User user=new User()
            {
                UserName=userName,
                Password=passwordHash,
                PasswordKey=passwordKey
            };
            _db.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await _db.Users.AnyAsync(x=>x.UserName==userName);
        }


        private bool MatchPasswordHash(string passwordText,byte[] password,byte[]passwordKey)
        {
            using(var hmac=new HMACSHA512(passwordKey))
            {
                
                var passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
                for(int i=0;i<passwordHash.Length;i++)
                {
                    if(passwordHash[i]!=password[i])
                        return false;
                }
                return true;
            }
        }
    }
}