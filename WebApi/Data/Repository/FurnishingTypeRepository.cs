using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Data.Repository
{
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly HousingDbContext _db;

        public FurnishingTypeRepository(HousingDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypeAsync()
        {
            return await _db.FurnishingTypes.ToListAsync();
        }
    }
}