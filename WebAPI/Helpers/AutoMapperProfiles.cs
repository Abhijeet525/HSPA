using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>();

            CreateMap<CityDto, City>()
                .ForMember(m => m.LastUpdatedBy, opt => opt.MapFrom(src => 1000))
                .ForMember(dest => dest.LastUpdatedOn, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
