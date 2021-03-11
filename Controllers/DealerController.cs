﻿using CarDealerAPI.Contexts;
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

        public ActionResult<IEnumerable<Dealer>> GetAllDealers()
        {
            var dealers = _dealerDbContext
                .Dealers
                .ToList();

            return Ok(dealers);
        }
    }
}
