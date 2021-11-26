using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface IProfessionService
    {
        Task<IEnumerable<Profession>> GetAllProfession();
        Task<Profession> GetProfessionById(int? id);
        Task<int> SaveProfession(Profession model);
        Task<bool> DeleteProfessionById(int? id);

    }
}
