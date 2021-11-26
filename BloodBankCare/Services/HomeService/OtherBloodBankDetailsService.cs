using BloodBankCare.Data;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Services.HomeService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService
{
    public class OtherBloodBankDetailsService: IOtherBloodBankDetailsService
	{
		public readonly AppDbContext _context;

		public OtherBloodBankDetailsService(AppDbContext context)
		{
			_context = context;
		}


		#region OtherBloodBankDetails

		public async Task<IEnumerable<OtherBloodBankDetails>> GetAllOtherBloodBankDetails()
		{
			return await _context.OtherBloodBankDetails.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<OtherBloodBankDetails> GetOtherBloodBankDetailsById(int? id)
		{
			return await _context.OtherBloodBankDetails.FindAsync(id);
		}



		public async Task<int> SaveOtherBloodBankDetails(OtherBloodBankDetails model)
		{
			if (model.Id != 0)
				_context.OtherBloodBankDetails.Update(model);
			else
				_context.OtherBloodBankDetails.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteOtherBloodBankDetailsById(int? id)
		{
			_context.OtherBloodBankDetails.Remove(_context.OtherBloodBankDetails.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
