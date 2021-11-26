using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.MasterData;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloodBankCare.Data.Entity.ApplicationUsers
{
    public class ApplicationUser : IdentityUser
    {

        public int? userTypeId { get; set; }
        public UserType userType { get; set; }
        public int? isActive { get; set; }

        public DateTime? birthDate { get; set; }

        public int? GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        public int? BloodGroupId { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }

        public DateTime? createdAt { get; set; }
        [MaxLength(120)]
        public string createdBy { get; set; }

        public DateTime? updatedAt { get; set; }
        [MaxLength(120)]
        public string updatedBy { get; set; }


        public virtual ICollection<DonorInformation> DonorInformations { get; set; }

    }
}
