﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NameMark { get; set; }
        public string Model { get; set; }
        
        public decimal Price { get; set; }
        public int DealerId { get; set; }
        public virtual Dealer Dealr { get; set; }

    }
}
