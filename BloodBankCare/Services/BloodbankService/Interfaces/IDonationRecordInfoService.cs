using BloodBankCare.Data.Entity.Bloodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.BloodbankService.Interfaces
{
   public interface IDonationRecordInfoService
    {
        Task<IEnumerable<DonationRecordInfo>> GetAllDonationRecordInfo();
        IEnumerable<DonationRecordInfo> GetAllDonationRecordInfoList(); 
        Task<DonationRecordInfo> GetDonationRecordInfoById(int? id);
        Task<int> SaveDonationRecordInfo(DonationRecordInfo model);
        Task<bool> DeleteDonationRecordInfoById(int? id);
        Task<DonationRecordInfo> GetDonationRecordByUserId(string DonorUserId, string RequestUserId);
        Task<DonationRecordInfo> GetDonationRecordByUserAndRequestId(string DonorUserId, string RequestUserId, int? bloodRequestInfoId);
        Task<DonationRecordInfo> GetLastDonationRecordByUserId(string DonorUserId);
        Task<IEnumerable<DonationRecordInfo>> GetDonationRecordByDonorUserId(string id);
        Task<int> TotalDonationByBloodGroup(int? id);
    }
}
