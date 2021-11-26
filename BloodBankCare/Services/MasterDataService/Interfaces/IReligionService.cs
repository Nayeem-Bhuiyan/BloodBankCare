using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface IReligionService
    {
        Task<IEnumerable<Religion>> GetAllReligion();
        Task<Religion> GetReligionById(int? id);
        Task<int> SaveReligion(Religion model);
        Task<bool> DeleteReligionById(int? id);
    }
}
