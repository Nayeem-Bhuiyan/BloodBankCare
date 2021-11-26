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
    public class DesignationService: IDesignationService
	{
		public readonly AppDbContext _context;

		public DesignationService(AppDbContext context)
		{
			_context = context;
		}


		#region Designation

		public async Task<IEnumerable<Designation>> GetAllDesignation()
		{
			return await _context.Designations.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<Designation> GetDesignationById(int? id)
		{
			return await _context.Designations.FindAsync(id);
		}



		public async Task<int> SaveDesignation(Designation model)
		{
			if (model.Id != 0)
				_context.Designations.Update(model);
			else
				_context.Designations.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteDesignationById(int? id)
		{
			_context.Designations.Remove(_context.Designations.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
