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
    public class ContactUsService: IContactUsService
    {
		public readonly AppDbContext _context;

		public ContactUsService(AppDbContext context)
		{
			_context = context;
		}


		#region ContactUS

		public async Task<IEnumerable<ContactUS>> GetAllContactUS()
		{
			return await _context.ContactUs.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<ContactUS> GetContactUSById(int? id)
		{
			return await _context.ContactUs.FindAsync(id);
		}



		public async Task<int> SaveContactUS(ContactUS model)
		{
			if (model.Id != 0)
				_context.ContactUs.Update(model);
			else
				_context.ContactUs.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeleteContactUSById(int? id)
		{
			_context.ContactUs.Remove(_context.ContactUs.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
        #endregion
    }
}
