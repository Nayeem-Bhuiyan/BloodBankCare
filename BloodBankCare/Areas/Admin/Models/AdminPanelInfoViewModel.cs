using BloodBankCare.Data.Entity.Admin;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Admin.Models
{
    public class AdminPanelInfoViewModel
    {

        public int AdminPanelInfoId { get; set; }
        public string name { get; set; }
        public string userId { get; set; }
        public string profession { get; set; }
        public string instituteName { get; set; }
        public string instituteType { get; set; }  //business,govt,pvt
        public string designation { get; set; }
        public string address { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile UploadImage { get; set; }
        public int? role { get; set; }  //1=admin,2=volunteer
        public string contactNo { get; set; }

        public virtual AdminPanelInfo adminPanelInfo { get; set; }
        public virtual IEnumerable<AdminPanelInfo> adminPanelInfos { get; set; }
    }
}
