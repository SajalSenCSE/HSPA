using System.Threading.Tasks;

namespace WebApi.Interface
{
    public interface IUnitOfWork
    {
         public ICityRepository CityRepository { get;}

         Task<bool> SaveAsync();
    }
}