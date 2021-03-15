using CarDealerAPI.DTOS;
using CarDealerAPI.Extensions;
using System.Collections.Generic;
using System.Security.Claims;

namespace CarDealerAPI.Services
{
    public interface IDealerService
    {
        int CreateDealer(DealerCreateDTO createDto);
        IEnumerable<DealerReadDTO> GetAllDealers(DealerQuerySearch query);
        DealerReadDTO GetDealerById(int id);
        void DeleteDealer(int id);
        void UpdateDealer(DealerUpdateDTO dto, int id);
    }
}