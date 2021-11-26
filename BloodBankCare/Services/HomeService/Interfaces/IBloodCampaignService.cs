using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService.Interfaces
{
   public interface IBloodCampaignService
    {
        Task<IEnumerable<BloodCampaign>> GetAllBloodCampaign();
        Task<BloodCampaign> GetBloodCampaignById(int? id);
        Task<int> SaveBloodCampaign(BloodCampaign model);
        Task<bool> DeleteBloodCampaignById(int? id);
    }
}
