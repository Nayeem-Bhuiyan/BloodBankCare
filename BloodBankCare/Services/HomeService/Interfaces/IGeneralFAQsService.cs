using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService.Interfaces
{
   public interface IGeneralFAQsService
    {
        Task<IEnumerable<GeneralFAQS>> GetAllGeneralFAQS();
        Task<GeneralFAQS> GetGeneralFAQSById(int? id);
        Task<int> SaveGeneralFAQS(GeneralFAQS model);
        Task<bool> DeleteGeneralFAQSById(int? id);
    }
}
