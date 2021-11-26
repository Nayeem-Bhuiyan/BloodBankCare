using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BloodBankCare.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankCare.Data.Entity.MasterData;
using BloodBankCare.Data.Entity.NoticeBoard;
using BloodBankCare.Data.Entity.Home;
using BloodBankCare.Data.Entity.Complain;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Data.Entity.Admin;
using BloodBankCare.Data.Entity.Address;
using BloodBankCare.Data.Entity.SocialOrganizationInfo;
using BloodBankCare.Data.Entity.ApplicationUsers;

namespace BloodBankCare.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        internal object tblName;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor _httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = _httpContextAccessor;
        }


        #region Settings Configs
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {

            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.User?.Identity?.Name)
                ? _httpContextAccessor.HttpContext.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).createdAt = DateTime.Now;
                    ((Base)entity.Entity).createdBy = currentUsername;
                }
                else
                {
                    entity.Property("createdAt").IsModified = false;
                    entity.Property("createdBy").IsModified = false;
                    ((Base)entity.Entity).updatedAt = DateTime.Now;
                    ((Base)entity.Entity).updatedBy = currentUsername;
                }

            }
        }
        #endregion

        #region userInfo
        public DbSet<UserType> userTypes { get; set; }
        #endregion

        #region MasterData
        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<Country> Countries  { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations  { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Thana> Thanas { get; set; }
        public DbSet<RelationShip> RelationShips { get; set; }
        #endregion

        #region Social Organization
        public DbSet<SocialOrganization> SocialOrganizations { get; set; }
        public DbSet<SocialOrganizationBrunch> SocialOrganizationBrunches { get; set; }
        public DbSet<SocialOrganizationDetails> SocialOrganizationDetails { get; set; }
        #endregion

        #region NoticeBoard
        public DbSet<NoticeBoardInfo> NoticeBoardInfos { get; set; }
        #endregion


        #region Home
        public DbSet<BloodCampaign> BloodCampaigns { get; set; }
        public DbSet<ContactUS> ContactUs { get; set; }
        public DbSet<DailyNewsUpdate> DailyNewsUpdates { get; set; }
        public DbSet<GeneralFAQS> GeneralFAQs { get; set; }
        public DbSet<HospitalDetails> HospitalDetails { get; set; }
        public DbSet<OtherBloodBankDetails> OtherBloodBankDetails { get; set; }
        public DbSet<PhotoGallery> PhotoGalleries { get; set; }
        public DbSet<WellWisherMessage> WellWisherMessages { get; set; }
        #endregion

        #region ComplainInfo
        public DbSet<ComplainInformation> ComplainInformations { get; set; }
        public DbSet<ComplainInformationDetails> ComplainInformationDetails { get; set; }
        public DbSet<ComplainSolveInfo> ComplainSolveInfos { get; set; }
        public DbSet<ComplainSolveInfoDetails> ComplainSolveInfoDetails { get; set; }
        #endregion

        #region BloodBank
        public DbSet<BloodRequestInfo> BloodRequestInfos { get; set; }
        public DbSet<BloodRequestReceivedInfo> BloodRequestReceivedInfos { get; set; }
        public DbSet<DonationRecordInfo> DonationRecordInfos { get; set; }
        public DbSet<DonorInformation> DonorInformations { get; set; }
        #endregion

        #region Admin
        public DbSet<AdminPanelInfo> AdminPanelInfos { get; set; }
        #endregion

        #region AddressInfo
        public DbSet<AddressInfo> AddressInfos { get; set; }
        #endregion
    }
}
