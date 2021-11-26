using BloodBankCare.Data.Entity.Home;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Home.Models
{
    public class PhotoGalleryViewModel
    {
        public int PhotoGalleryId { get; set; }
        public IFormFile Image { get; set; }
        public string userId { get; set; }
        public string fileUrl { get; set; }
        public string fileType { get; set; }  //image/video/file
        public string headLineText { get; set; }
        public string footerText { get; set; }
        public string DisplaySectionName { get; set; }  //kon place e eta dekhabo tar nam

        public virtual PhotoGallery photoGallery { get; set; }
        public virtual IEnumerable<PhotoGallery> photoGalleries { get; set; }
    }
}
