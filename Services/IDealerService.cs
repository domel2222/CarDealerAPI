using CarDealerAPI.DTOS;
using System.Collections.Generic;

namespace CarDealerAPI.Services
{
    public interface IDealerService
    {
        void CreateDealer(DealerCreateDTO createDto);
        IEnumerable<DealerReadDTO> GetAllDealers();
        DealerReadDTO GetDealerById(int id);
    }
}