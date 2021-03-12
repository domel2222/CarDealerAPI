using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DealerController : ControllerBase
    {
        private readonly DealerDbContext _dealerDbContext;

        public DealerController(DealerDbContext dealerDbContext)
        {
            this._dealerDbContext = dealerDbContext;
        }


        [HttpGet]
        public ActionResult<IEnumerable<DealerDTO>> GetAllDealers()
        {
            var dealers = _dealerDbContext
                .Dealers
                .ToList();

            //var dealersDTO = dealers.Select(r => new DealerDTO()
            //{
            //    DealerName = r.DealerName,
            //    Category = r.Category,
            //    City = r.Address.City,  and so on 

            //} ;
            

            return Ok(dealers);
        }

        [HttpGet("{id}")]
        public ActionResult<Dealer> GetOneDealer ([FromRoute]int id)
        {
            var dealer = _dealerDbContext
                .Dealers
                .FirstOrDefault(r => r.Id == id);

            if (dealer is null)
            {
                return NotFound();
            }

            return Ok(dealer);
        }
    }
}
