using BloodBankCare.Data.Entity.Address;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Address.Models
{
    public class AddressInfoViewModel
    {
        public int AddressInfoId { get; set; }

        public string userId { get; set; }

        public string careOf { get; set; }
        public string houseNo { get; set; }
        public string villageName { get; set; }
        public string postOffice { get; set; }
        public int? addressType { get; set; }  //1=present Or 2=Permanent

        public int? CountryId { get; set; }
        public int? DistrictId { get; set; }
        public int? ThanaId { get; set; }
        public int? DonorInformationId { get; set; }

        public virtual DonorInformation donorInformation { get; set; }
        public virtual IEnumerable<DonorInformation> donorInformations { get; set; }

        public virtual Country country { get; set; }
        public virtual IEnumerable<Country> countries { get; set; }

        public virtual District district { get; set; }
        public virtual IEnumerable<District> districts { get; set; }

        public virtual Thana thana { get; set; }
        public virtual IEnumerable<Thana> thanas { get; set; }

        public virtual AddressInfo addressInfo { get; set; }
        public virtual IEnumerable<AddressInfo> addressInfos { get; set; }
    }
}
