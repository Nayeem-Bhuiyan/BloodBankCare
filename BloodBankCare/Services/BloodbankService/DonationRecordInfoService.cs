using BloodBankCare.Data;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Services.BloodbankService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.BloodbankService
{
    public class DonationRecordInfoService: IDonationRecordInfoService
	{
		public readonly AppDbContext _context;

		public DonationRecordInfoService(AppDbContext context)
		{
			_context = context;
		}


		#region DonationRecordInfo

		public async Task<IEnumerable<DonationRecordInfo>> GetAllDonationRecordInfo()
		{
			return await _context.DonationRecordInfos.Include(x=>x.BloodGroup).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}


		public IEnumerable<DonationRecordInfo> GetAllDonationRecordInfoList()
		{
			return  _context.DonationRecordInfos.Include(x => x.BloodGroup).OrderByDescending(x => x.Id).AsNoTracking().ToList();
		}

		public async Task<DonationRecordInfo> GetDonationRecordInfoById(int? id)
		{
			return await _context.DonationRecordInfos.FindAsync(id);
		}


		public async Task<int> TotalDonationByBloodGroup(int? id)
		{
			return await _context.DonationRecordInfos.Where(x=>x.BloodGroupId==id).AsNoTracking().CountAsync();
		}

		public async Task<DonationRecordInfo> GetDonationRecordByUserId(string DonorUserId,string RequestUserId)
		{
			return await _context.DonationRecordInfos.Where(x=>x.donorUserId== DonorUserId&&x.userId== RequestUserId).AsNoTracking().FirstOrDefaultAsync();
		}


		public async Task<DonationRecordInfo> GetDonationRecordByUserAndRequestId(string DonorUserId, string RequestUserId,int? bloodRequestInfoId)
		{
			return await _context.DonationRecordInfos.Where(x => x.donorUserId == DonorUserId && x.userId == RequestUserId && x.BloodRequestInfoId==bloodRequestInfoId).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<DonationRecordInfo> GetLastDonationRecordByUserId(string DonorUserId)
		{
			return await _context.DonationRecordInfos.Where(x => x.donorUserId == DonorUserId).OrderByDescending(x=>x.Id).AsNoTracking().FirstOrDefaultAsync();
		}





		public async Task<int> SaveDonationRecordInfo(DonationRecordInfo model)
		{
			if (model.Id != 0)
				_context.DonationRecordInfos.Update(model);
			else
				_context.DonationRecordInfos.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteDonationRecordInfoById(int? id)
		{
			_context.DonationRecordInfos.Remove(_context.DonationRecordInfos.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}


		public async Task<IEnumerable<DonationRecordInfo>> GetDonationRecordByDonorUserId(string id)
		{
			return await _context.DonationRecordInfos.Include(x=>x.BloodGroup).Where(x=>x.donorUserId==id).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		#endregion
	}
}
