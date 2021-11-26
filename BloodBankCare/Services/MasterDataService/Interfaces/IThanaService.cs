using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface IThanaService
    {
        Task<IEnumerable<Thana>> GetThanasByDistrictId(int? id);
        Task<IEnumerable<Thana>> GetAllThana();
        Task<Thana> GetThanaById(int? id);
        Task<int> SaveThana(Thana model);
        Task<bool> DeleteThanaById(int? id);
    }
}
