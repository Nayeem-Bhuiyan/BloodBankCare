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
    public class CountryService: ICountryService
	{
		public readonly AppDbContext _context;

		public CountryService(AppDbContext context)
		{
			_context = context;
		}


		#region Country

		public async Task<IEnumerable<Country>> GetAllCountry()
		{
			return await _context.Countries.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<Country> GetCountryById(int? id)
		{
			return await _context.Countries.FindAsync(id);
		}



		public async Task<int> SaveCountry(Country model)
		{
			if (model.Id != 0)
				_context.Countries.Update(model);
			else
				_context.Countries.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteCountryById(int? id)
		{
			_context.Countries.Remove(_context.Countries.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
