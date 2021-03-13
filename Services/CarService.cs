using AutoMapper;
using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Exceptions;
using CarDealerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Services
{
    public class CarService
    {
        private readonly DealerDbContext _dealerDbContext;
        private readonly IMapper _mapper;

        public CarService(DealerDbContext dealerDbContext, IMapper mapper)
        {
            this._dealerDbContext = dealerDbContext;
            this._mapper = mapper;
        }
        public int Create(int dealerId, CarCreateDTO newCar)
        {
            var dealer = _dealerDbContext.Dealers.FirstOrDefault(d => d.Id == dealerId);
            if (dealer == null) throw new NotFoundException("Dealer not found");

            var carE = _mapper.Map<Car>(newCar);
        }
    }
}
