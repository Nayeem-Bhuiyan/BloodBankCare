using BloodBankCare.Data;
using BloodBankCare.Data.Entity.Complain;
using BloodBankCare.Services.ComplainService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.ComplainService
{
    public class ComplainInformationDetailsService: IComplainInformationDetailsService
	{
		public readonly AppDbContext _context;

		public ComplainInformationDetailsService(AppDbContext context)
		{
			_context = context;
		}


		#region ComplainInformationDetails

		public async Task<IEnumerable<ComplainInformationDetails>> GetAllComplainInformationDetails()
		{
			return await _context.ComplainInformationDetails.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<ComplainInformationDetails> GetComplainInformationDetailsById(int? id)
		{
			return await _context.ComplainInformationDetails.FindAsync(id);
		}



		public async Task<int> SaveComplainInformationDetails(ComplainInformationDetails model)
		{
			if (model.Id != 0)
				_context.ComplainInformationDetails.Update(model);
			else
				_context.ComplainInformationDetails.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteComplainInformationDetailsById(int? id)
		{
			_context.ComplainInformationDetails.Remove(_context.ComplainInformationDetails.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}


		#endregion
	}
}
