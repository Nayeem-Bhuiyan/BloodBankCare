using BloodBankCare.Data.Entity.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.AddressInfoService.Interfaces
{
   public interface IAddressInfoService
    {
        Task<IEnumerable<AddressInfo>> GetAllAddressInfo();
        Task<AddressInfo> GetAddressInfoById(int? id);
        Task<int> SaveAddressInfo(AddressInfo model);
        Task<bool> DeleteAddressInfoById(int? id);
        Task<AddressInfo> GetDonorPresentAddress(int? id);
        Task<AddressInfo> GetDonorPermanentAddress(int? id);
    }
}
