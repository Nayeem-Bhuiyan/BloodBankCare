using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface IDistrictService
    {
        Task<IEnumerable<District>> GetDistrictsByCountryId(int? id);
        Task<IEnumerable<District>> GetAllDistrict();
        Task<District> GetDistrictById(int? id);
        Task<int> SaveDistrict(District model);
        Task<bool> DeleteDistrictById(int? id);
    }
}
