using System.Collections.Generic;
using System.Linq;
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
        [Authorize]
        public async Task<IActionResult> AddPropertyPhoto(IFormFile file,int id)
        {
            var resultPhoto=await _photoService.UploadPhotoAsync(file);
            if(resultPhoto.Error != null)
                return BadRequest(resultPhoto.Error.Message);
            var property=await _uow.PropertyRepository.GetPropertyByIdAsync(id);
            var photo=new Photo()
            {
                ImageUrl=resultPhoto.SecureUrl.AbsoluteUri,
                PublicId=resultPhoto.PublicId
            };
            if(property.Photos.Count==0) 
            {
                photo.IsPrimary=true;
            }   
            property.Photos.Add(photo);
            await _uow.SaveAsync();
            return Ok(201);    
        }

        //api/property/set-primary-photo/1/wxihpah3sdt1l6qcyixx
        [HttpPost("set-primary-photo/{id}/{photoPublicId}")] 
        [AllowAnonymous]
        public async Task<IActionResult> SetPrimaryPhoto(int id, string photoPublicId )
        {
            var userId=GetUserId();

            var property=await _uow.PropertyRepository.GetPropertyByIdAsync(id);

            if(property==null)
                return BadRequest("Property Not exist in database");
            if(property.PostedBy!=userId)
                return BadRequest("You are not authorize");   

            var photo=property.Photos.FirstOrDefault(p=>p.PublicId==photoPublicId);     

            if(photo == null)
                return BadRequest("No such Property or photo exists");
            if(photo.IsPrimary)
                return BadRequest("This photo is already a primary photo");
            var currentPrimary=property.Photos.FirstOrDefault(p=>p.IsPrimary);
            if(currentPrimary != null)
                currentPrimary.IsPrimary=false;    
            photo.IsPrimary=true;
            if(await _uow.SaveAsync())
                return Ok(201);
            return BadRequest("Some Unkhown Error happend");       
        }
    }
}