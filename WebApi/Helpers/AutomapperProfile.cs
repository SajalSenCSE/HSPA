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
        }
    }
}