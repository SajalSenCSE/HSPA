using System.Threading.Tasks;

namespace WebApi.Interface
{
    public interface IUnitOfWork
    {
         public ICityRepository CityRepository { get;}
         public IUserRepository UserRepository{get;}

         Task<bool> SaveAsync();
    }
}