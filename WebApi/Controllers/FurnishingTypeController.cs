using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Interface;

namespace WebApi.Controllers
{
    public class FurnishingTypeController:BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FurnishingTypeController(IUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        [HttpGet("list")] //api/furnishingtype/list
        public async Task<IActionResult> GetFurnishingTypes()
        {
            var fTypes=await _uow.FurnishingTypeRepository.GetFurnishingTypeAsync();
            var fTypeDto=_mapper.Map<IEnumerable<KeyValuePairDto>>(fTypes);
            return Ok(fTypeDto);

        }
    }
}