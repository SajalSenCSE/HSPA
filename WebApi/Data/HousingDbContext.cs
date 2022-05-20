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
        
    }
}