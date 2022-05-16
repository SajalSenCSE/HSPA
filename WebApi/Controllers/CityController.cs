using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Repository;
using WebApi.Dtos;
using WebApi.Interface;
using WebApi.Models;
//using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork uow,IMapper mapper)
        {
            _uow=uow;
            _mapper = mapper;
        }

        [HttpGet] //api/City
        public async Task<IActionResult> GetCities()
        {
            var cities=await _uow.CityRepository.GetCitiesAsync();
            var citiesDto=_mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }


         
        [HttpPost("add")] //api/city/add
        public async Task<IActionResult> AddCity(CityDto city)
        {
            var acity= _mapper.Map<City>(city);
                acity.LastUpdatedBy=1;
                acity.LastUpdatedOn=DateTime.Now;
            _uow.CityRepository.AddCity(acity);
            await _uow.SaveAsync();
            return Ok(acity);
            
        }

        [HttpDelete("delete/{id}")] //api/city/delete
        public async Task<IActionResult> DeleteCity(int id)
        {
            _uow.CityRepository.DeleteCity(id);
            await _uow.SaveAsync();
            return Ok("Sucessfully Delete city");
        }

    }
}