using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Interface;

namespace WebApi.Controllers
{
    public class PropertyController:BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PropertyController(IUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        
        [HttpGet("list/{sellRent}")] ///...api/property/type
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var properties = await _uow.PropertyRepository.GetPropertiesAsync(sellRent);

            var propertyListDto = _mapper.Map<IEnumerable<PropertyListDto>>(properties);

            return Ok(propertyListDto);
        }
    }
}