using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.SocialOrganizationInfoService.Interfaces
{
   public interface ISocialOrganizationBrunchService
    {
        Task<IEnumerable<SocialOrganizationBrunch>> GetAllSocialOrganizationBrunch();
        Task<SocialOrganizationBrunch> GetSocialOrganizationBrunchById(int? id);
        Task<int> SaveSocialOrganizationBrunch(SocialOrganizationBrunch model);
        Task<bool> DeleteSocialOrganizationBrunchById(int? id);
    }
}
