using System.Threading.Tasks;
using WebApi.Data.Repository;
using WebApi.Interface;

namespace WebApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HousingDbContext _db;

        public UnitOfWork(HousingDbContext db)
        {
            _db = db;
        }
        public ICityRepository CityRepository => new CityRepository(_db);

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync()>0;
        }
    }
}