using BloodBankCare.Data.Entity.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.AdminPanelServices.Interfaces
{
   public interface IAdminPanelService
    {
        Task<IEnumerable<AdminPanelInfo>> GetAllAdminPanelInfo();
        Task<AdminPanelInfo> GetAdminPanelInfoById(int? id);
        Task<int> SaveAdminPanelInfo(AdminPanelInfo model);
        Task<bool> DeleteAdminPanelInfoById(int? id);
    }
}
