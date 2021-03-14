using CarDealerAPI.DTOS;
using System.Collections.Generic;
using System.Security.Claims;

namespace CarDealerAPI.Services
{
    public interface IDealerService
    {
        int CreateDealer(DealerCreateDTO createDto, int userId);
        IEnumerable<DealerReadDTO> GetAllDealers();
        DealerReadDTO GetDealerById(int id);
        void DeleteDealer(int id, ClaimsPrincipal user);
        void UpdateDealer(DealerUpdateDTO dto, int id, ClaimsPrincipal user);
    }
}