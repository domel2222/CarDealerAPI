﻿using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Controllers
{
    [Route("api/dealer/{dealerId}/car")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly DealerDbContext _dealerDbContext;

        public CarController(ICarService carService, DealerDbContext dealerDbContext)
        {
            this._carService = carService;
            this._dealerDbContext = dealerDbContext;
        }

        [HttpPost]
        public ActionResult CreateCar([FromRoute] int dealerId, [FromBody] CarCreateDTO carNew)
        {
            var carId = _carService.CreateNewCar(dealerId, carNew);

            return Created($"api/dealer/{dealerId}/car/{carId}", null);
        }

        [HttpGet("{carId}")]
        public ActionResult<CarReadDTO> GetCarInDealer(int dealerId, int carId)
        {
            CarReadDTO car = _carService.GetCarById(dealerId, carId);

            return Ok(car);
        }

        //[HttpGet("user")]
        //public ActionResult GetUser()
        //{
        //        var cos = _dealerDbContext.Users
        //                        .Include(c => c.Role);


        //    return Ok(cos);
        //}

        [HttpGet]
        [Authorize(Policy = "DealerMinimum")]
        public ActionResult<List<CarReadDTO>>  GetAllCars(int dealerId)
        {
            var cars = _carService.GetAllCarForDealer(dealerId);

            return Ok(cars);
        }
        [HttpDelete]
        public  ActionResult DeleteAllCars(int dealerId)
        {
            _carService.DeleteAll(dealerId);

            return NoContent();
        }
        //delet one car
    }
}
