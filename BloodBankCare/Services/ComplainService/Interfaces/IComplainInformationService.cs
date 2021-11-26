using BloodBankCare.Data.Entity.Complain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.ComplainService.Interfaces
{
   public interface IComplainInformationService
    {
        Task<IEnumerable<ComplainInformation>> GetAllComplainInformation();
        Task<ComplainInformation> GetComplainInformationById(int? id);
        Task<int> SaveComplainInformation(ComplainInformation model);
        Task<bool> DeleteComplainInformationById(int? id);
        Task<bool> DeleteComplainInfoByDetailsId(int? id);
        Task<ComplainInformation> GetComplainInfoByDetailsId(int? id);
    }
}
