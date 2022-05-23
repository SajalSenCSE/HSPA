using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PropertyController:BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public PropertyController(IUnitOfWork uow,IMapper mapper,IPhotoService photoService)
        {
            _uow = uow;
            _mapper = mapper;
            _photoService = photoService;
        }
        
        [HttpGet("list/{sellRent}")] ///...api/property/type
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var properties = await _uow.PropertyRepository.GetPropertiesAsync(sellRent);

            var propertyListDto = _mapper.Map<IEnumerable<PropertyListDto>>(properties);

            return Ok(propertyListDto);
        }

        [HttpGet("detail/{id}")] ///...api/property/type
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await _uow.PropertyRepository.GetPropertyDetailAsync(id);

            var propertyDetailDto = _mapper.Map<PropertyDetailDto>(property);

            return Ok(propertyDetailDto);
        }

        [HttpPost("add")] //api/property/add
        [Authorize]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDto)
        {
            var property=_mapper.Map<Property>(propertyDto);
            var userId=GetUserId();
                property.PostedBy=userId;
                property.LastUpdatedBy=userId;
            _uow.PropertyRepository.AddProperty(property);    
            await _uow.SaveAsync();
            return StatusCode(201);
        }


        [HttpPost("add/photo/{id}")] //...add/photo/1
        public async Task<IActionResult> AddPropertyPhoto(IFormFile file,int id)
        {
            var resultPhoto=await _photoService.UploadPhotoAsync(file);
            if(resultPhoto.Error != null)
                return BadRequest(resultPhoto.Error.Message);
            return Ok(201);    
        }
    }
}