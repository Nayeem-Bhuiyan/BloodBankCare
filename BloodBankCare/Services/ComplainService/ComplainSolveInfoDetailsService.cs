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
    public class ComplainSolveInfoDetailsService: IComplainSolveInfoDetailsService
	{
		public readonly AppDbContext _context;

		public ComplainSolveInfoDetailsService(AppDbContext context)
		{
			_context = context;
		}


		#region ComplainSolveInfoDetails

		public async Task<IEnumerable<ComplainSolveInfoDetails>> GetAllComplainSolveInfoDetails()
		{
			return await _context.ComplainSolveInfoDetails.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<ComplainSolveInfoDetails> GetComplainSolveInfoDetailsById(int? id)
		{
			return await _context.ComplainSolveInfoDetails.FindAsync(id);
		}

		public async Task<ComplainSolveInfoDetails> GetComplainSolveInfoDetailsByComDetailsId(int? id)
		{
			return await _context.ComplainSolveInfoDetails.Where(x=>x.ComplainInformationDetailsId==id).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<int> SaveComplainSolveInfoDetails(ComplainSolveInfoDetails model)
		{
			if (model.Id != 0)
				_context.ComplainSolveInfoDetails.Update(model);
			else
				_context.ComplainSolveInfoDetails.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteComplainSolveInfoDetailsById(int? id)
		{
			_context.ComplainSolveInfoDetails.Remove(_context.ComplainSolveInfoDetails.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
        #endregion
    }
}
