using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string NameMark { get; set; }
        public string Model { get; set; }
        
        public decimal Price { get; set; }
        public int DealerId { get; set; }
        public virtual Dealer Dealr { get; set; }

    }
}
