using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
//using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly HousingDbContext _db;

        public CityController(HousingDbContext db)
        {
            _db = db;
        }

        [HttpGet] //api/City
        public IActionResult GetCities(){
            var cities=_db.Cities.ToList();
            return Ok(cities);
        }
    }
}