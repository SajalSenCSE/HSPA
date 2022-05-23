
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Interface;

namespace WebApi.Controllers
{
    public class PropertyTypeController:BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PropertyTypeController(IUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet("list")]   //api/propertytype/list
        public async Task<IActionResult> GetPropertyTypes()
        {
            var propertyTypes=await _uow.PropertyTypeRepository.GetPropertyTypesAsync();
            var propertyTypeDto=_mapper.Map<IEnumerable<KeyValuePairDto>>(propertyTypes);
            return Ok(propertyTypeDto);
        }

    }
}