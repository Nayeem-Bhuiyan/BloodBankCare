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
    public class BloodRequestReceivedInfoService: IBloodRequestReceivedInfoService
	{
		public readonly AppDbContext _context;

		public BloodRequestReceivedInfoService(AppDbContext context)
		{
			_context = context;
		}


		#region BloodRequestReceivedInfo

		public async Task<IEnumerable<BloodRequestReceivedInfo>> GetAllBloodRequestReceivedInfo()
		{
			return await _context.BloodRequestReceivedInfos.Include(x=>x.BloodRequestInfo).Include(x=>x.BloodRequestInfo.BloodGroup).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}
		

		public async Task<IEnumerable<BloodRequestReceivedInfo>> GetDonorAcceptedList(string userId)
		{
			return await _context.BloodRequestReceivedInfos.Include(x => x.BloodRequestInfo).Include(x => x.BloodRequestInfo.BloodGroup).Where(x => x.remarks == "DonorAccepted" && x.acceptedBy== userId).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<IEnumerable<BloodRequestReceivedInfo>> GetUserBloodRequestReceivedList()
		{
			return await _context.BloodRequestReceivedInfos.Include(x => x.BloodRequestInfo).Include(x => x.BloodRequestInfo.BloodGroup).Where(x=>x.remarks== "UserRequest").OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<BloodRequestReceivedInfo> GetBloodRequestReceivedInfoById(int? id)
		{
			return await _context.BloodRequestReceivedInfos.FindAsync(id);
		}

		public async Task<BloodRequestReceivedInfo> GetBloodRequestReceivedInfoByUserId(string DonorUserId,int? requestId)
		{
			return await _context.BloodRequestReceivedInfos.Where(x=>x.acceptedBy== DonorUserId && x.BloodRequestInfoId==requestId).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<BloodRequestReceivedInfo>> GetBloodRequestReceivedInfoByDonorUserId(string DonorUserId)
		{
			return await _context.BloodRequestReceivedInfos.Where(x => x.acceptedBy == DonorUserId).AsNoTracking().ToListAsync();
		}

		public async Task<BloodRequestReceivedInfo> GetBloodRequestReceivedInfoByReceivedId(int? requestReceivedId, int? requestId)
		{
			return await _context.BloodRequestReceivedInfos.Where(x => x.Id == requestReceivedId && x.BloodRequestInfoId == requestId && x.isDonated==2).Include(x=>x.BloodRequestInfo).Include(x=>x.BloodRequestInfo.BloodGroup).Include(x => x.BloodRequestInfo.RelationShip).AsNoTracking().FirstOrDefaultAsync();
		}


		public async Task<IEnumerable<BloodRequestReceivedInfo>> GetBloodRequestReceivedInfoByRequestId(int? id)
		{
			return await _context.BloodRequestReceivedInfos.Where(x=>x.BloodRequestInfoId==id).AsNoTracking().ToListAsync();
		}



		public async Task<BloodRequestReceivedInfo> GetBloodRequestReceivedInfoOfUserRequest(int? id)
		{
			return await _context.BloodRequestReceivedInfos.Where(x => x.BloodRequestInfoId == id && x.remarks == "UserRequest").AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<int> SaveBloodRequestReceivedInfo(BloodRequestReceivedInfo model)
		{
			if (model.Id != 0)
				_context.BloodRequestReceivedInfos.Update(model);
			else
				_context.BloodRequestReceivedInfos.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteBloodRequestReceivedInfoById(int? id)
		{
			_context.BloodRequestReceivedInfos.Remove(_context.BloodRequestReceivedInfos.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}


		public async Task<bool> DeleteMultipleBloodRequestReceivedInfo(List<BloodRequestReceivedInfo> bloodRequestReceivedInfos)
		{
			_context.BloodRequestReceivedInfos.RemoveRange(bloodRequestReceivedInfos);
			return 1 == await _context.SaveChangesAsync();
		}

		#endregion
	}
}
