using CarDealerAPI.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DealerController : ControllerBase
    {
        private readonly DealerDbContext _dealerDbContext;

        public DealerController(DealerDbContext dealerDbContext)
        {
            this._dealerDbContext = dealerDbContext;
        }

        public ActionResult<IEnumerable>
    }
}
