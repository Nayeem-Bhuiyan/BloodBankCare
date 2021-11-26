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
    public class DistrictService: IDistrictService
	{
		public readonly AppDbContext _context;

		public DistrictService(AppDbContext context)
		{
			_context = context;
		}


		#region District

		public async Task<IEnumerable<District>> GetDistrictsByCountryId(int? id)
		{
			return await _context.Districts.Where(x=>x.CountryId==id).AsNoTracking().ToListAsync();
		}

		public async Task<IEnumerable<District>> GetAllDistrict()
		{
			return await _context.Districts.Include(c=>c.Country).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<District> GetDistrictById(int? id)
		{
			return await _context.Districts.FindAsync(id);
		}



		public async Task<int> SaveDistrict(District model)
		{
			if (model.Id != 0)
				_context.Districts.Update(model);
			else
				_context.Districts.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteDistrictById(int? id)
		{
			_context.Districts.Remove(_context.Districts.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}



		#endregion
	}
}
