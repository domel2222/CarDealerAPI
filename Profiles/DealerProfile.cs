using AutoMapper;
using CarDealerAPI.DTOS;
using CarDealerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Profiles
{
    public class DealerProfile : Profile
    {
        public DealerProfile()
        {
            CreateMap<Dealer, DealerDTO>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.Country, c => c.MapFrom(s => s.Address.Country));

            CreateMap<Car, CarDTO>().ReverseMap();
        }
    }
}
