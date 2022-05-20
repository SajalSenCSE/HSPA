using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
    
    [Authorize]
    public class CityController : BaseController
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
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var acity= _mapper.Map<City>(city);
                acity.LastUpdatedBy=1;
                acity.LastUpdatedOn=DateTime.Now;
            _uow.CityRepository.AddCity(acity);
            await _uow.SaveAsync();
            return Ok(acity);
        }

        [HttpPut("update/{id}")] //api/city/update/1
        public async Task<IActionResult> UpdateCity(int id,CityDto cityDto)
        {
            if(id !=cityDto.Id)
                return BadRequest("Update Not Allow");

            var city=await _uow.CityRepository.FindCity(id);
                if(city==null)
                    return BadRequest("Update not allow");
                city.LastUpdatedBy=1;
                city.LastUpdatedOn=DateTime.Now;

            _mapper.Map(cityDto,city);

            await _uow.SaveAsync();    
            return StatusCode(200);
        }

        [HttpPut("updateCityName/{id}")] //api/city/update/1
        public async Task<IActionResult> UpdateCityName(int id,CityUpdateDto cityDto)
        {
            var city=await _uow.CityRepository.FindCity(id);
                city.LastUpdatedBy=1;
                city.LastUpdatedOn=DateTime.Now;

            _mapper.Map(cityDto,city);
            await _uow.SaveAsync();    
            return StatusCode(200);
        }


        [HttpPatch("update/{id}")] //api/city/update/1
        public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        {
            var city=await _uow.CityRepository.FindCity(id);
                city.LastUpdatedBy=2;
                city.LastUpdatedOn=DateTime.Now;
            cityToPatch.ApplyTo(city,ModelState);

            await _uow.SaveAsync();    
            return StatusCode(200);
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