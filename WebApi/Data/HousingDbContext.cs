using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class HousingDbContext:DbContext
    {
        public HousingDbContext(DbContextOptions<HousingDbContext> options):base(options)
        {
            
        }
        public  DbSet<City> Cities{get;set;}

        public DbSet<User> Users { get; set; }

        public DbSet<Property> Properties { get; set; }
        public DbSet<FurnishingType> FurnishingTypes { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        
    }
}