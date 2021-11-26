using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountry();
        Task<Country> GetCountryById(int? id);
        Task<int> SaveCountry(Country model);
        Task<bool> DeleteCountryById(int? id);

    }
}
