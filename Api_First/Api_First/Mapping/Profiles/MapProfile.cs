using Api_First.DTOs;
using Api_First.DTOs.Car;
using Api_First.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_First.Mapping.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Car, CarGetDto>();
            CreateMap<Engine, EngineInCarGetDto>();
            CreateMap<Car, CarListItemDto>();
            //CreateMap<List<Car>, ListDto<CarListItemDto>>();
        }
    }
}
