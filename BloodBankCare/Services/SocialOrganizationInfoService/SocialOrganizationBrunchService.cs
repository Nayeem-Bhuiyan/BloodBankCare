using BloodBankCare.Data;
using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using BloodBankCare.Services.SocialOrganizationInfoService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.SocialOrganizationInfoService
{
    public class SocialOrganizationBrunchService: ISocialOrganizationBrunchService
	{
		public readonly AppDbContext _context;

		public SocialOrganizationBrunchService(AppDbContext context)
		{
			_context = context;
		}


		#region SocialOrganizationBrunch

		public async Task<IEnumerable<SocialOrganizationBrunch>> GetAllSocialOrganizationBrunch()
		{
			return await _context.SocialOrganizationBrunches.Include(s=>s.SocialOrganization).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<SocialOrganizationBrunch> GetSocialOrganizationBrunchById(int? id)
		{
			return await _context.SocialOrganizationBrunches.FindAsync(id);
		}



		public async Task<int> SaveSocialOrganizationBrunch(SocialOrganizationBrunch model)
		{
			if (model.Id != 0)
				_context.SocialOrganizationBrunches.Update(model);
			else
				_context.SocialOrganizationBrunches.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteSocialOrganizationBrunchById(int? id)
		{
			_context.SocialOrganizationBrunches.Remove(_context.SocialOrganizationBrunches.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
