using BloodBankCare.Data.Entity.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Services.HomeService.Interfaces
{
   public interface IPhotoGalleryService
    {
        Task<IEnumerable<PhotoGallery>> GetAllPhotoGallery();
        Task<PhotoGallery> GetPhotoGalleryById(int? id);
        Task<int> SavePhotoGallery(PhotoGallery model);
        Task<bool> DeletePhotoGalleryById(int? id);
    }
}
