using AutoMapper;
using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Models;
using CarDealerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
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
            //try
            //{
                var dealersDTO = _dealerService.GetAllDealers();

                return Ok(dealersDTO);
            //}
            //catch (Exception)
            //{

                //return this.StatusCode(StatusCodes.Status500InternalServerError, "Database fail");
            //}
               
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<DealerReadDTO> GetOneDealer (int id)
        {
            //create for this repository and try as Task
            var dealer = _dealerService.GetDealerById(id);

            return Ok(dealer);
        }
        [HttpPost]
        public ActionResult CreateDealer(DealerCreateDTO createDto)
        {

            var id = _dealerService.CreateDealer(createDto);

            return Created($"api/Dealer/{id}", null);

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDealer(int id)
        {
            _dealerService.DeleteDealer(id);

            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateDealer(DealerUpdateDTO dto, int id)
        {
             _dealerService.UpdateDealer(dto, id);

            return Ok();
        }
    }
}
