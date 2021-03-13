using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly DealerDbContext _dealerDbContext;

        public AccountService(DealerDbContext dealerDbContext)
        {
            this._dealerDbContext = dealerDbContext;
        }

        public void RegisterUser(UserCreateDTO userDto)
        {
            var newUser = new User()
            {
                DateOfBirth = userDto.DateOfBirth,
                Email = userDto.Email,
                Nationality = userDto.Nationality,
                RoleId = userDto.RoleId
            };

            _dealerDbContext.Add(newUser);
            _dealerDbContext.SaveChanges();
        }
    }
}
