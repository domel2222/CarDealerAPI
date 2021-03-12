using AutoMapper;
using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public DealerController(DealerDbContext dealerDbContext, IMapper mapper)
        {
            this._dealerDbContext = dealerDbContext;
            this._mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<DealerReadDTO>> GetAllDealers()
        {
            var dealers = _dealerDbContext
                .Dealers
                .Include(r => r.Address)
                .Include(r => r.Cars)
                .ToList();

            //var dealersDTO = dealers.Select(r => new DealerDTO()
            //{
            //    DealerName = r.DealerName,
            //    Category = r.Category,
            //    City = r.Address.City,  and so on 

            //} ;

            var dealersDto = _mapper.Map<List<DealerReadDTO>>(dealers);
            

            return Ok(dealersDto);
        }

        [HttpGet("{id}")]
        public ActionResult<Dealer> GetOneDealer ([FromRoute]int id)
        {
            var dealer = _dealerDbContext
                .Dealers
                .Include(r => r.Address)
                .Include(r => r.Cars)
                .FirstOrDefault(r => r.Id == id);

            if (dealer is null)
            {
                return NotFound();
            }

            var dealerDto = _mapper.Map<DealerReadDTO>(dealer);

            return Ok(dealerDto);
        }
        [HttpPost]
        public ActionResult CreateDealer([FromBody] DealerCreateDTO createDto)
        {
            var dealer = _mapper.Map<Dealer>(createDto);

            _dealerDbContext.Add(dealer);
            _dealerDbContext.SaveChanges();

            return Created($"api/Dealer/{dealer.Id}", null);

        }
    }
}
