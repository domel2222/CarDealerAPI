using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Exceptions;
using CarDealerAPI.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(DealerDbContext dealerDbContext, IPasswordHasher<User> passwordHasher)
        {
            this._dealerDbContext = dealerDbContext;
            this._passwordHasher = passwordHasher;
        }

        public string GenerateToken(UserLoginDTO login)
        {
            var user = _dealerDbContext.Users.FirstOrDefault(u => u.Email == login.Email);

            if (user == null) throw new BadRequestException("Username or password is wrong");

            return "mama";

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
            var passwordHashed = _passwordHasher.HashPassword(newUser, userDto.Password);
            newUser.PasswordHash = passwordHashed; 
            _dealerDbContext.Add(newUser);
            _dealerDbContext.SaveChanges();
        }
    }
}
