using CarDealerAPI.DTOS;
using CarDealerAPI.Services;
using Microsoft.AspNetCore.Mvc;
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
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            this._carService = carService;
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
        }

    }
}
