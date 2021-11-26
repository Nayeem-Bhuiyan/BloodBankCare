using BloodBankCare.Data;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Services.BloodbankService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.BloodbankService
{
    public class DonorInformationService: IDonorInformationService
	{
		public readonly AppDbContext _context;

		public DonorInformationService(AppDbContext context)
		{
			_context = context;
		}


		#region DonorInformation

		public async Task<IEnumerable<DonorInformation>> GetAllDonorInformation()
		{
			return await _context.DonorInformations.OrderByDescending(x => x.Id).Include(x => x.ApplicationUser).Include(x => x.Profession).Include(x => x.SocialOrganization).Include(x=>x.Religion).AsNoTracking().ToListAsync();
		}


		public async Task<DonorInformation> GetDonorInformationById(int? id)
		{
			return await _context.DonorInformations.FindAsync(id);
		}

		public async Task<DonorInformation> GetDonorInformationByUserId(string UserId)
		{
			return await _context.DonorInformations.Where(x=>x.userId== UserId).Include(x => x.ApplicationUser).Include(x => x.SocialOrganization).Include(x=>x.Religion).Include(x => x.Profession).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<int> SaveDonorInformation(DonorInformation model)
		{
			if (model.Id != 0)
				_context.DonorInformations.Update(model);
			else
				_context.DonorInformations.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteDonorInformationById(int? id)
		{
			_context.DonorInformations.Remove(_context.DonorInformations.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}


		public async Task<IEnumerable<ApplicationUser>> GetDonorInfoByBloodGroup(int? id)
		{
			return await _context.Users.Include(x => x.DonorInformations).Include(x => x.Gender).Include(x => x.BloodGroup).Where(x=>x.BloodGroupId==id).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}


		#endregion
	}
}
