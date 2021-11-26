using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService.Interfaces
{
   public interface IHospitalDetailsService
    {
        Task<IEnumerable<HospitalDetails>> GetAllHospitalDetails();
        Task<HospitalDetails> GetHospitalDetailsById(int? id);
        Task<int> SaveHospitalDetails(HospitalDetails model);
        Task<bool> DeleteHospitalDetailsById(int? id);
    }
}
