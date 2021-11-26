using BloodBankCare.Data;
using BloodBankCare.Data.Entity.Address;
using BloodBankCare.Services.AddressInfoService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.AddressInfoService
{
    public class AddressInfoService: IAddressInfoService
	{
		public readonly AppDbContext _context;

		public AddressInfoService(AppDbContext context)
		{
			_context = context;
		}


		#region AddressInfo

		public async Task<IEnumerable<AddressInfo>> GetAllAddressInfo()
		{
			return await _context.AddressInfos.OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<AddressInfo> GetAddressInfoById(int? id)
		{
			return await _context.AddressInfos.FindAsync(id);
		}

		public async Task<AddressInfo> GetAddressInfoByDonorId(int? id)
		{
			return await _context.AddressInfos.Where(x=>x.DonorInformationId==id).AsNoTracking().FirstOrDefaultAsync();
		}



		public async Task<AddressInfo> GetDonorPresentAddress(int? id)  //addressType=1(present Address)
		{
			return await _context.AddressInfos.Where(x => x.DonorInformationId == id && x.addressType == 1).Include(x=>x.Country).Include(x => x.Thana).Include(x => x.District).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<AddressInfo> GetDonorPermanentAddress(int? id)  //addressType=0(Permanent Address)
		{
			return await _context.AddressInfos.Where(x => x.DonorInformationId == id && x.addressType==0).Include(x => x.Country).Include(x=>x.Thana).Include(x=>x.District).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<int> SaveAddressInfo(AddressInfo model)
		{
			if (model.Id != 0)
				_context.AddressInfos.Update(model);
			else
				_context.AddressInfos.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteAddressInfoById(int? id)
		{
			_context.AddressInfos.Remove(_context.AddressInfos.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
        #endregion
    }
}
