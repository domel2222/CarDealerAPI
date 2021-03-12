using AutoMapper;
using CarDealerAPI.Contexts;
using CarDealerAPI.DTOS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Services
{
    public class DealerService
    {
        private readonly DealerDbContext _dealerDbContext;
        private readonly IMapper _mapper;

        public DealerService(DealerDbContext dealerDbContext, IMapper mapper)
        {
            this._dealerDbContext = dealerDbContext;
            this._mapper = mapper;
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


    }
}
