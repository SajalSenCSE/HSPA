using System.Linq;
using AutoMapper;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<City,CityDto>().ReverseMap();

            CreateMap<City,CityUpdateDto>().ReverseMap();


            CreateMap<Property,PropertyListDto>()
                    .ForMember(p=>p.City,opt=>opt.MapFrom(src=>src.City.Name))
                    .ForMember(p=>p.PropertyType,opt=>opt.MapFrom(p=>p.PropertyType.Name))
                    .ForMember(p=>p.Country,opt=>opt.MapFrom(src=>src.City.Country))
                    .ForMember(p=>p.Photo,opt=>opt
                                   .MapFrom(src=>src.Photos
                                   .FirstOrDefault(p=>p.IsPrimary).ImageUrl))
                    .ForMember(p=>p.FurnishingType,opt=>opt.MapFrom(src=>src.FurnishingType.Name));

            CreateMap<Property,PropertyDetailDto>()
                    .ForMember(p=>p.City,opt=>opt.MapFrom(src=>src.City.Name))
                    .ForMember(p=>p.PropertyType,opt=>opt.MapFrom(p=>p.PropertyType.Name))
                    .ForMember(p=>p.Country,opt=>opt.MapFrom(src=>src.City.Country))
                    .ForMember(p=>p.FurnishingType,opt=>opt.MapFrom(src=>src.FurnishingType.Name));   


            CreateMap<PropertyType,KeyValuePairDto>().ReverseMap();           
            CreateMap<FurnishingType,KeyValuePairDto>().ReverseMap();  
            CreateMap<Property,PropertyDto>().ReverseMap();
            CreateMap<Photo,PhotoDto>().ReverseMap();
        }
    }
}