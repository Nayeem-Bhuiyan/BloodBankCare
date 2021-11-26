using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Data.Entity.Address
{
    public class AddressInfo: Base
    {


        public string userId { get; set; }

        public string careOf { get; set; }
        public string houseNo { get; set; }
        public string  villageName{ get; set; }
        public string  postOffice{ get; set; }
        public int? addressType { get; set; }  //present Or Permanent




        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int? DistrictId { get; set; }
        public virtual District District { get; set; }

        public int? ThanaId { get; set; }
        public virtual Thana Thana { get; set; }

        public int? DonorInformationId { get; set; }
        public virtual DonorInformation DonorInformation { get; set; }



    }
}
