using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.BloodbankService.Interfaces
{
   public interface IBloodRequestReceivedInfoService
    {
        Task<IEnumerable<BloodRequestReceivedInfo>> GetAllBloodRequestReceivedInfo();
        Task<IEnumerable<BloodRequestReceivedInfo>> GetUserBloodRequestReceivedList();
        Task<IEnumerable<BloodRequestReceivedInfo>> GetDonorAcceptedList(string userId);
        Task<BloodRequestReceivedInfo> GetBloodRequestReceivedInfoById(int? id);
        Task<int> SaveBloodRequestReceivedInfo(BloodRequestReceivedInfo model);
        Task<bool> DeleteBloodRequestReceivedInfoById(int? id);
        Task<IEnumerable<BloodRequestReceivedInfo>> GetBloodRequestReceivedInfoByRequestId(int? id);
        Task<BloodRequestReceivedInfo> GetBloodRequestReceivedInfoOfUserRequest(int? id);
        Task<bool> DeleteMultipleBloodRequestReceivedInfo(List<BloodRequestReceivedInfo> bloodRequestReceivedInfos);
        Task<BloodRequestReceivedInfo> GetBloodRequestReceivedInfoByUserId(string DonorUserId, int? requestId);
        Task<BloodRequestReceivedInfo> GetBloodRequestReceivedInfoByReceivedId(int? requestReceivedId, int? requestId);
        Task<IEnumerable<BloodRequestReceivedInfo>> GetBloodRequestReceivedInfoByDonorUserId(string DonorUserId);
    }
}
