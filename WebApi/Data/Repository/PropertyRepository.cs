using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Data.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly HousingDbContext _db;
        public PropertyRepository(HousingDbContext db)
        {
            _db=db;
        }

        public void AddProperty(Property property)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteProperty(Property property)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
           var properties= await _db.Properties
                        .Where(x=>x.SellRent == sellRent)
                        .Include(p=>p.PropertyType)
                        .Include(p=>p.FurnishingType)
                        .Include(p=>p.City)
                        .ToListAsync();
           return properties;
        }
    }
}