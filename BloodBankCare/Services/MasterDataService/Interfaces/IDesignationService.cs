using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService.Interfaces
{
   public interface IDesignationService
    {
        Task<IEnumerable<Designation>> GetAllDesignation();
        Task<Designation> GetDesignationById(int? id);
        Task<int> SaveDesignation(Designation model);
        Task<bool> DeleteDesignationById(int? id);
    }
}
