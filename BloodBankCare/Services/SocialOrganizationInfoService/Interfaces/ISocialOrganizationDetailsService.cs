using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.SocialOrganizationInfoService.Interfaces
{
   public interface ISocialOrganizationDetailsService
    {
        Task<IEnumerable<SocialOrganizationDetails>> GetAllSocialOrganizationDetails();
        Task<SocialOrganizationDetails> GetSocialOrganizationDetailsById(int? id);
        Task<int> SaveSocialOrganizationDetails(SocialOrganizationDetails model);
        Task<bool> DeleteSocialOrganizationDetailsById(int? id);
    }
}
