using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Data.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly HousingDbContext _db;

        public CityRepository(HousingDbContext db)
        {
            _db = db;
        }
        public void AddCity(City city)
        {
            _db.Cities.Add(city);
        }

        public void DeleteCity(int id)
        {
            var city=_db.Cities.Find(id);
            _db.Cities.Remove(city);
        }

        public async Task<City> FindCity(int id)
        {
            return await _db.Cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _db.Cities.ToListAsync();
        }

        
    }
}