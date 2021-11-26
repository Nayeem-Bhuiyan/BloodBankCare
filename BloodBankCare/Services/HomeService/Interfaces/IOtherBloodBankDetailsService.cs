using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService.Interfaces
{
   public interface IOtherBloodBankDetailsService
    {
        Task<IEnumerable<OtherBloodBankDetails>> GetAllOtherBloodBankDetails();
        Task<OtherBloodBankDetails> GetOtherBloodBankDetailsById(int? id);
        Task<int> SaveOtherBloodBankDetails(OtherBloodBankDetails model);
        Task<bool> DeleteOtherBloodBankDetailsById(int? id);
    }
}
