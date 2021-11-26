using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.BloodbankService.Interfaces
{
  public  interface IBloodRequestInfoService
    {
        Task<IEnumerable<BloodRequestInfo>> GetAllBloodRequestInfo();
        Task<BloodRequestInfo> GetBloodRequestInfoById(int? id);
        Task<int> SaveBloodRequestInfo(BloodRequestInfo model);
        Task<bool> DeleteBloodRequestInfoById(int? id);
        Task<IEnumerable<BloodRequestInfo>> GetBloodRequestInfoByUserId(string id);
    }
}
