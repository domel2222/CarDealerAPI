using AutoMapper;
using CarDealerAPI.Authorization;
using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Exceptions;
using CarDealerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace CarDealerAPI.Services
{
    public class DealerService : IDealerService
    {
        private readonly DealerDbContext _dealerDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DealerService> _logger;
        private readonly IAuthorizationService _authorizationService;

        public DealerService(DealerDbContext dealerDbContext, IMapper mapper, ILogger<DealerService> logger, IAuthorizationService authorizationService)
        {
            this._dealerDbContext = dealerDbContext;
            this._mapper = mapper;
            this._logger = logger;
            this._authorizationService = authorizationService;
        }

        public DealerReadDTO GetDealerById(int id)
        {
            var dealer = _dealerDbContext
                .Dealers
                .Include(r => r.Address)
                .Include(r => r.Cars)
                .FirstOrDefault(r => r.Id == id);

            if (dealer == null) throw new NotFoundException("dealer not found");

            var result = _mapper.Map<DealerReadDTO>(dealer);

            return result;
        }
        public IEnumerable<DealerReadDTO> GetAllDealers()
        {
            var dealers = _dealerDbContext
                .Dealers
                .Include(r => r.Address)
                .Include(r => r.Cars)
                .ToList();

            var dealersDto = _mapper.Map<List<DealerReadDTO>>(dealers);

            return dealersDto;

        }

        public int CreateDealer(DealerCreateDTO createDto, int userId)
        {
            var dealer = _mapper.Map<Dealer>(createDto);

            dealer.CreatedById = userId;
            _dealerDbContext.Add(dealer);
            _dealerDbContext.SaveChanges();

            return dealer.Id;
        }

        public void DeleteDealer(int id, ClaimsPrincipal user)
        {

            _logger.LogWarning($"Dealer with: {id} DELETE action invoke");
            _logger.LogError($"Dealer with: {id} DELETE action invoke uuuuuuuuuuuuu");

            var dealer = _dealerDbContext
                .Dealers
                .FirstOrDefault(r => r.Id == id);

            if (dealer == null) throw new NotFoundException("dealer not found");

            var authResult = _authorizationService.AuthorizeAsync(user, dealer, new ResouceOperationRequirement(ResouceOperation.Delete)).Result;

            if (!authResult.Succeeded)
            {
                throw new ForbiddenExc("Access denied");
            }

            _dealerDbContext.Dealers.Remove(dealer);
            _dealerDbContext.SaveChanges();

            
        }

        public void UpdateDealer(DealerUpdateDTO dto, int id, ClaimsPrincipal user)
        {
            var dealer = _dealerDbContext
                .Dealers
                .FirstOrDefault(a => a.Id == id);

            if (dealer == null)
                throw new NotFoundException("dealer not found");

            var authResult = _authorizationService.AuthorizeAsync(user, dealer, new ResouceOperationRequirement(ResouceOperation.Update)).Result;

            if (!authResult.Succeeded)
            {
                throw new ForbiddenExc("Access denied");
            }

            dealer.DealerName = dto.DealerName;
            dealer.Description = dto.Description;
            dealer.TestDrive = dto.TestDrive;

            _dealerDbContext.SaveChanges();

            
        }
    }
}
