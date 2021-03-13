using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.DTOS
{
    public class UserCreateDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(150)]
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int RoleId { get; set } = 1;
    }
}
