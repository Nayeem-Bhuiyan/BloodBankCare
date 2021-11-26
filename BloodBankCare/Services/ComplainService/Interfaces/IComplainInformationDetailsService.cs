using BloodBankCare.Data.Entity.Complain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.ComplainService.Interfaces
{
   public interface IComplainInformationDetailsService
    {
        Task<IEnumerable<ComplainInformationDetails>> GetAllComplainInformationDetails();
        Task<ComplainInformationDetails> GetComplainInformationDetailsById(int? id);
        Task<int> SaveComplainInformationDetails(ComplainInformationDetails model);
        Task<bool> DeleteComplainInformationDetailsById(int? id);
    }
}
