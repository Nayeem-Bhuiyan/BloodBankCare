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
    public class PhotoGalleryService: IPhotoGalleryService
	{
		public readonly AppDbContext _context;

		public PhotoGalleryService(AppDbContext context)
		{
			_context = context;
		}


		#region PhotoGallery

		public async Task<IEnumerable<PhotoGallery>> GetAllPhotoGallery()
		{
			return await _context.PhotoGalleries.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
		}

		public async Task<PhotoGallery> GetPhotoGalleryById(int? id)
		{
			return await _context.PhotoGalleries.FindAsync(id);
		}



		public async Task<int> SavePhotoGallery(PhotoGallery model)
		{
			if (model.Id != 0)
				_context.PhotoGalleries.Update(model);
			else
				_context.PhotoGalleries.Add(model);

			return await _context.SaveChangesAsync();

		}

		public async Task<bool> DeletePhotoGalleryById(int? id)
		{
			_context.PhotoGalleries.Remove(_context.PhotoGalleries.Find(id));
			return 1 == await _context.SaveChangesAsync();
		}
		#endregion
	}
}
