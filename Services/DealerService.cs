using AutoMapper;
using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using CarDealerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Services
{
    public class DealerService : IDealerService
    {
        private readonly DealerDbContext _dealerDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DealerService> _logger;

        public DealerService(DealerDbContext dealerDbContext, IMapper mapper, ILogger<DealerService> logger)
        {
            this._dealerDbContext = dealerDbContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public DealerReadDTO GetDealerById(int id)
        {
            var dealer = _dealerDbContext
                .Dealers
                .Include(r => r.Address)
                .Include(r => r.Cars)
                .FirstOrDefault(r => r.Id == id);

            if (dealer is null) return null;

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

        public int CreateDealer(DealerCreateDTO createDto)
        {
            var dealer = _mapper.Map<Dealer>(createDto);

            _dealerDbContext.Add(dealer);
            _dealerDbContext.SaveChanges();

            return dealer.Id;
        }

        public bool DeleteDealer(int id)
        {

            _logger.LogWarning($"Dealer with: {id} DELETE action invoke");
            _logger.LogError($"Dealer with: {id} DELETE action invoke uuuuuuuuuuuuu");

            var dealer = _dealerDbContext
                .Dealers
                .FirstOrDefault(r => r.Id == id);

            if (dealer is null) return false;

            _dealerDbContext.Dealers.Remove(dealer);
            _dealerDbContext.SaveChanges();

            return true;
        }

        public bool UpdateDealer(DealerUpdateDTO dto, int id)
        {
            var dealer = _dealerDbContext
                .Dealers
                .FirstOrDefault(a => a.Id == id);

            if (dealer is null) return false;

            dealer.DealerName = dto.DealerName;
            dealer.Description = dto.Description;
            dealer.TestDrive = dto.TestDrive;

            _dealerDbContext.SaveChanges();

            return true;
        }
    }
}
