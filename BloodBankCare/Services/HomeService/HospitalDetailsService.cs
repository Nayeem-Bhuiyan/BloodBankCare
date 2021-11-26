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
    public class HospitalDetailsService: IHospitalDetailsService
	{
		public readonly AppDbContext _context;

		public HospitalDetailsService(AppDbContext context)
		{
			_context = context;
		}


		#region HospitalDetails

		public async Task<IEnumerable<HospitalDetails>> GetAllHospitalDetails()
		{
			return await _context.HospitalDetails.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<HospitalDetails> GetHospitalDetailsById(int? id)
		{
			return await _context.HospitalDetails.FindAsync(id);
		}



		public async Task<int> SaveHospitalDetails(HospitalDetails model)
		{
			if (model.Id != 0)
				_context.HospitalDetails.Update(model);
			else
				_context.HospitalDetails.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteHospitalDetailsById(int? id)
		{
			_context.HospitalDetails.Remove(_context.HospitalDetails.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
