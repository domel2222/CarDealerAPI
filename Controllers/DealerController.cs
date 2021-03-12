using AutoMapper;
using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Models;
using CarDealerAPI.Services;
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
        private readonly IDealerService _dealerService;

        public DealerController(IDealerService dealerService)
        {
            this._dealerService = dealerService;
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


            var dealersDTO = _dealerService.GetAllDealers();

            return Ok(dealersDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<DealerReadDTO> GetOneDealer ([FromRoute]int id)
        {

            var dealer = _dealerService.GetDealerById(id);

            if (dealer is null)
            {
                return NotFound();
            }

            return Ok(dealer);
        }
        [HttpPost]
        public ActionResult CreateDealer([FromBody] DealerCreateDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _dealerService.CreateDealer(createDto);

            return Created($"api/Dealer/{id}", null);

        }
    }
}
