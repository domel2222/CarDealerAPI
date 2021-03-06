using AutoMapper;
using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Extensions;
using CarDealerAPI.Models;
using CarDealerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[Authorize]
    public class DealerController : ControllerBase
    {
        private readonly IDealerService _dealerService;

        public DealerController(IDealerService dealerService)
        {
            this._dealerService = dealerService;
        }

        [HttpGet]
        //[Authorize(Policy = "ColorEyes")]
        //[Authorize(Policy = "ColorEyes")]
        //[Authorize(Policy = "OnlyForEagles")]
        


        public ActionResult<IEnumerable<DealerReadDTO>> GetAllDealers([FromQuery] DealerQuerySearch query)
        {

            //var Drugilogger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            //Drugilogger.Info("hurraddizała");
            //Logger logger = LogManager.GetLogger("databaseLogger");

           //_dealerService.   logger.Info("lipdssdgsgsgsdg");



            //var dealersDTO = dealers.Select(r => new DealerDTO()
            //{
            //    DealerName = r.DealerName,
            //    Category = r.Category,
            //    City = r.Address.City,  and so on 

            //} ;
            //try
            //{
                var dealersDTO = _dealerService.GetAllDealers(query);

                return Ok(dealersDTO);
            //}
            //catch (Exception)
            //{

                //return this.StatusCode(StatusCodes.Status500InternalServerError, "Database fail");
            //}
               
        }

        [HttpGet("{id}")]
        //[Authorize(Policy = "HasNation")]
        public ActionResult<DealerReadDTO> GetOneDealer (int id)
        {
            //create for this repository and try as Task
            var dealer = _dealerService.GetDealerById(id);

            return Ok(dealer);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,Dealer Manager")]
        //[Authorize(Roles = "Dealer Manager")] // calim role must have in JWT
        // test for user 
        public ActionResult CreateDealer(DealerCreateDTO createDto)
        {
            //use []  from client and iterate by them ..... :)
            // hot to itterate for DTO  form body
            //var userId =   int.Parse(User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value);
            //foreach (var item in createDto)
            //{

            //}
            
            
            var id = _dealerService.CreateDealer(createDto);
            
            

            return Created($"api/Dealer/{id}", null);

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDealer(int id)
        {
            //_dealerService.DeleteDealer(id, User);
            _dealerService.DeleteDealer(id);

            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateDealer(DealerUpdateDTO dto, int id)
        {
             //_dealerService.UpdateDealer(dto, id, User);
             _dealerService.UpdateDealer(dto, id);

            return Ok();
        }
    }
}
