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
    public class BloodRequestInfoService: IBloodRequestInfoService
	{
		public readonly AppDbContext _context;

		public BloodRequestInfoService(AppDbContext context)
		{
			_context = context;
		}


		#region BloodRequestInfo

		public async Task<IEnumerable<BloodRequestInfo>> GetAllBloodRequestInfo()
		{
			return await _context.BloodRequestInfos.Include(x=>x.BloodGroup).Include(r=>r.RelationShip).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<BloodRequestInfo> GetBloodRequestInfoById(int? id)
		{
			return await _context.BloodRequestInfos.FindAsync(id);
		}



		public async Task<int> SaveBloodRequestInfo(BloodRequestInfo model)
		{
			if (model.Id != 0)
				_context.BloodRequestInfos.Update(model);
			else
				_context.BloodRequestInfos.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteBloodRequestInfoById(int? id)
		{
			_context.BloodRequestInfos.Remove(_context.BloodRequestInfos.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}



		public async Task<IEnumerable<BloodRequestInfo>> GetBloodRequestInfoByUserId(string id)
		{
			return await _context.BloodRequestInfos.Include(x => x.BloodGroup).Include(r => r.RelationShip).Where(x=>x.userId==id).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		#endregion
	}
}
