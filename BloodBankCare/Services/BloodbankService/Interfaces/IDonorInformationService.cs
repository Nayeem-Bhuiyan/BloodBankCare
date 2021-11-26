using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.BloodbankService.Interfaces
{
   public interface IDonorInformationService
    {
        Task<IEnumerable<DonorInformation>> GetAllDonorInformation();
        Task<DonorInformation> GetDonorInformationById(int? id);
        Task<int> SaveDonorInformation(DonorInformation model);
        Task<bool> DeleteDonorInformationById(int? id);
        Task<DonorInformation> GetDonorInformationByUserId(string UserId);
        Task<IEnumerable<ApplicationUser>> GetDonorInfoByBloodGroup(int? id);
    }
}
