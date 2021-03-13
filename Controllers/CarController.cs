using CarDealerAPI.DTOS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Controllers
{
    [Route("api/{dealerId}/car")]
    [ApiController]
    [Produces("application/json")]
    public class CarController : ControllerBase
    {
        [HttpPost]
        public ActionResult CreateCar(int dealerId, CarCreateDTO carNew)
        {

        }
    }
}
