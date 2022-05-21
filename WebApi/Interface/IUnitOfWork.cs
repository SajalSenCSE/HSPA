using System.Threading.Tasks;

namespace WebApi.Interface
{
    public interface IUnitOfWork
    {
         public ICityRepository CityRepository { get;}
         public IUserRepository UserRepository{get;}
        
         public IPropertyRepository PropertyRepository{get;}

         Task<bool> SaveAsync();
    }
}