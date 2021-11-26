using BloodBankCare.Data.Entity.Admin;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Data.Entity.NoticeBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Models
{
    public class HomePageViewModel
    {


        public IEnumerable<HospitalDetails> hospitalDetails { get; set; }
        public IEnumerable<BloodCampaign> bloodCampaigns  { get; set; }
        public IEnumerable<ContactUS> contactUs{ get; set; }
        public IEnumerable<DailyNewsUpdate> dailyNewsUpdates { get; set; }
        public IEnumerable<GeneralFAQS> generalFAQs { get; set; }
        public IEnumerable<OtherBloodBankDetails> otherBloodBankDetails { get; set; }
        public IEnumerable<PhotoGallery> photoGalleries { get; set; }
        public IEnumerable<WellWisherMessage> wellWisherMessages { get; set; }
        public IEnumerable<AdminPanelInfo> adminPanelInfos { get; set; }
        public IEnumerable<NoticeBoardInfo> noticeBoardInfos { get; set; }
        public DonationRecordInfo lastDonationRecord { get; set; }

        public IEnumerable<BloodGroupWiseDonorViewModel> bloodGroupWiseDonorViewModels { get; set; }
        public IEnumerable<ApplicationUser> applicationUsers { get; set; }
        public IEnumerable<DonorInformation> donors { get; set; }
        public IEnumerable<TopDonorViewModel> topDonorViewModels { get; set; }
        public CountRecordViewModel countRecordInfos { get; set; }
    }
}
