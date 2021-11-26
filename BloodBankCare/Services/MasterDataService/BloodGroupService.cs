using BloodBankCare.Data;
using BloodBankCare.Data.Entity.MasterData;
using BloodBankCare.Services.MasterDataService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.MasterDataService
{
    public class BloodGroupService: IBloodGroupService
	{
		public readonly AppDbContext _context;

		public BloodGroupService(AppDbContext context)
		{
			_context = context;
		}


		#region BloodGroup

		public async Task<IEnumerable<BloodGroup>> GetAllBloodGroup()
		{
			return await _context.BloodGroups.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<BloodGroup> GetBloodGroupById(int? id)
		{
			return await _context.BloodGroups.FindAsync(id);
		}



		public async Task<int> SaveBloodGroup(BloodGroup model)
		{
			if (model.Id != 0)
				_context.BloodGroups.Update(model);
			else
				_context.BloodGroups.Add(model);
			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteBloodGroupById(int? id)
		{
			_context.BloodGroups.Remove(_context.BloodGroups.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
