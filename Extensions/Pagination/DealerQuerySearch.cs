using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Extensions
{
    public class DealerQuerySearch
    {
        public string search { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
