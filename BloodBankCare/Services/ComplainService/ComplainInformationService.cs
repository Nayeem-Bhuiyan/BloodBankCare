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
    public class ComplainInformationService: IComplainInformationService
	{
		public readonly AppDbContext _context;

		public ComplainInformationService(AppDbContext context)
		{
			_context = context;
		}


		#region ComplainInformation

		public async Task<IEnumerable<ComplainInformation>> GetAllComplainInformation()
		{
			return await _context.ComplainInformations.Include(x=>x.ComplainInformationDetails).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<ComplainInformation> GetComplainInformationById(int? id)
		{
			return await _context.ComplainInformations.FindAsync(id);
		}


		public async Task<ComplainInformation> GetComplainInfoByDetailsId(int? id)
		{
			return await _context.ComplainInformations.Where(c => c.ComplainInformationDetailsId == id).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<int> SaveComplainInformation(ComplainInformation model)
		{
			if (model.Id != 0)
				_context.ComplainInformations.Update(model);
			else
				_context.ComplainInformations.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteComplainInformationById(int? id)
		{
			_context.ComplainInformations.Remove(_context.ComplainInformations.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}


		public async Task<bool> DeleteComplainInfoByDetailsId(int? id)
		{
			_context.ComplainInformations.Remove(_context.ComplainInformations.Where(c => c.ComplainInformationDetailsId == id).FirstOrDefault());
			return 1 == await _context.SaveChangesAsync();
		}

		#endregion
	}
}
