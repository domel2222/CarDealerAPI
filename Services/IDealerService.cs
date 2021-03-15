using CarDealerAPI.DTOS;
using System.Collections.Generic;
using System.Security.Claims;

namespace CarDealerAPI.Services
{
    public interface IDealerService
    {
        int CreateDealer(DealerCreateDTO createDto);
        IEnumerable<DealerReadDTO> GetAllDealers(string searchPhrases);
        DealerReadDTO GetDealerById(int id);
        void DeleteDealer(int id);
        void UpdateDealer(DealerUpdateDTO dto, int id);
    }
}