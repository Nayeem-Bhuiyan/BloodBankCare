using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface IBloodGroupService
    {
        Task<IEnumerable<BloodGroup>> GetAllBloodGroup();
        Task<BloodGroup> GetBloodGroupById(int? id);
        Task<int> SaveBloodGroup(BloodGroup model);
        Task<bool> DeleteBloodGroupById(int? id);

    }
}
