using AutoMapper;
using Domain.Dtos.HomePage;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Header, HeaderDto>().ReverseMap();

        }
        
    }
}
