using AutoMapper;
using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Exceptions;
using CarDealerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Services
{
    public class CarService : ICarService, ICarService1
    {
        private readonly DealerDbContext _dealerDbContext;
        private readonly IMapper _mapper;

        public CarService(DealerDbContext dealerDbContext, IMapper mapper)
        {
            this._dealerDbContext = dealerDbContext;
            this._mapper = mapper;
        }

        public int CreateNewCar(int dealerId, CarCreateDTO newCar)
        {
            var dealer = _dealerDbContext.Dealers.FirstOrDefault(d => d.Id == dealerId);
            if (dealer == null) throw new NotFoundException("Dealer not found");


            var carE = _mapper.Map<Car>(newCar);
            carE.DealerId = dealerId;
            _dealerDbContext.Add(carE);
            _dealerDbContext.SaveChanges();

            return carE.Id;
        }

        public CarReadDTO GetCarById(int dealerId, int carId)
        {
            var dealer = _dealerDbContext.Dealers.FirstOrDefault(d => d.Id == dealerId);
            if (dealer == null) throw new NotFoundException("Dealer not found");

            var car = _dealerDbContext.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null || car.DealerId != dealerId) throw new NotFoundException("Car not found");

            var carToRead = _mapper.Map<CarReadDTO>(car);

            return carToRead;
        }

        public List<CarReadDTO> GetAllCarForDealer(int dealerId)
        {
            var dealer = _dealerDbContext.Dealers
                            .Include(c => c.Cars)
                            .FirstOrDefault(d => d.Id == dealerId);

            if (dealer == null) throw new NotFoundException("Dealer not found");

            var cars = _mapper.Map<List<CarReadDTO>>(dealer.Cars);

            return cars;
        }
    }
}
