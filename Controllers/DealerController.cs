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
            

            //var dealersDTO = dealers.Select(r => new DealerDTO()
            //{
            //    DealerName = r.DealerName,
            //    Category = r.Category,
            //    City = r.Address.City,  and so on 

            //} ;

            
            

            return Ok(dealersDto);
        }

        [HttpGet("{id}")]
        public ActionResult<DealerReadDTO> GetOneDealer ([FromRoute]int id)
        {
            

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            return Created($"api/Dealer/{dealer.Id}", null);

        }
    }
}
