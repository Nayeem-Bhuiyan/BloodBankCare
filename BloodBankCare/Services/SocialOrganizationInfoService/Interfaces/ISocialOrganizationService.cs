using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.SocialOrganizationInfoService.Interfaces
{
  public interface ISocialOrganizationService
    {
        Task<IEnumerable<SocialOrganization>> GetAllSocialOrganization();
        Task<SocialOrganization> GetSocialOrganizationById(int? id);
        Task<int> SaveSocialOrganization(SocialOrganization model);
        Task<bool> DeleteSocialOrganizationById(int? id);

    }
}
