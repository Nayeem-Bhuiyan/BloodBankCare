using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Home
{
    public class PhotoGallery:Base
    {
        public string userId { get; set; }
        public string fileUrl { get; set; }
        public string fileType { get; set; }  //image/video/file
        public string headLineText { get; set; }
        public string footerText { get; set; }
        public string DisplaySectionName { get; set; }  //kon place e eta dekhabo tar nam

    }
}
