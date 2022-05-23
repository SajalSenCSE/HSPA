using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Data.Repository
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly HousingDbContext _db;

        public PropertyTypeRepository(HousingDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync()
        {
            return await _db.PropertyTypes.ToListAsync();
        }
    }
}