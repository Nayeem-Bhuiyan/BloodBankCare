using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface IGenderService
    {
        Task<IEnumerable<Gender>> GetAllGender();
        Task<Gender> GetGenderById(int? id);
        Task<int> SaveGender(Gender model);
        Task<bool> DeleteGenderById(int? id);
    }
}
