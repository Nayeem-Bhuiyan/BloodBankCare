using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService.Interfaces
{
   public interface IContactUsService
    {
        Task<IEnumerable<ContactUS>> GetAllContactUS();
        Task<ContactUS> GetContactUSById(int? id);
        Task<int> SaveContactUS(ContactUS model);
        Task<bool> DeleteContactUSById(int? id);
    }
}
