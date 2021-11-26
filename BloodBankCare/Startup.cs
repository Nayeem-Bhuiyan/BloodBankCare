using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using BloodBankCare.Data;
using BloodBankCare.Data.Entity;
using BloodBankCare.Services.AuthService;
using BloodBankCare.Services.AuthService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Services.MasterDataService.Interfaces;
using BloodBankCare.Services.MasterDataService;
using BloodBankCare.Services.BloodbankService.Interfaces;
using BloodBankCare.Services.BloodbankService;
using BloodBankCare.Services.HomeService.Interfaces;
using BloodBankCare.Services.HomeService;
using BloodBankCare.Services.AdminPanelServices.Interfaces;
using BloodBankCare.Services.AdminPanelServices;
using BloodBankCare.Services.NoticeBoardService.Interfaces;
using BloodBankCare.Services.NoticeBoardService;
using BloodBankCare.Services.ComplainService.Interfaces;
using BloodBankCare.Services.ComplainService;
using BloodBankCare.Services.SocialOrganizationInfoService.Interfaces;
using BloodBankCare.Services.SocialOrganizationInfoService;
using BloodBankCare.Services.AddressInfoService.Interfaces;
using BloodBankCare.Services.AddressInfoService;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace BloodBankCare
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
           
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });


            #region Cookie

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                    {
                        options.Cookie.Name = ".AdventureWorks.Session";
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                        options.LoginPath = "/Auth/Account/login";
                        options.LogoutPath = "/Auth/Account/Logout";
                        options.AccessDeniedPath = "/Auth/Account/AccessDenied";
                        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                        options.Cookie.SameSite = SameSiteMode.Strict;
                    });

            #endregion


            services.AddHttpContextAccessor();

            #region App Database Settings
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection")));


            services.AddMemoryCache();
            //.AddDefaultTokenProviders();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ERPDbContext>();
            #endregion


            #region Auth Related Settings
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;


            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LogoutPath = "/Auth/Account/Logout";
                options.LoginPath = "/Auth/Account/Login";
                options.AccessDeniedPath = "/Auth/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion


            #region Areas Config
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Areas/{2}/{0}" + RazorViewEngine.ViewExtension);
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            #endregion

            #region UserService
            services.AddScoped<IUserInfoes, UserInfoes>();
            #endregion

            #region PDF
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            #endregion

            services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions
                   .PropertyNamingPolicy = null;
             });


            //services.AddControllersWithViews();
            services.AddRazorPages();


            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = true;
            });


            #region MasterDataService
            services.AddScoped<IBloodGroupService, BloodGroupService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IThanaService, ThanaService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IProfessionService, ProfessionService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IReligionService, ReligionService>();
            services.AddScoped<IRelationShipService, RelationShipService>();
            #endregion
            #region AddressInfoService
            services.AddScoped<IAddressInfoService, AddressInfoService>();
            #endregion
            #region BloodBankService
            services.AddScoped<IBloodRequestInfoService, BloodRequestInfoService>();
            services.AddScoped<IBloodRequestReceivedInfoService, BloodRequestReceivedInfoService>();
            services.AddScoped<IDonationRecordInfoService, DonationRecordInfoService>();
            services.AddScoped<IDonorInformationService, DonorInformationService>();
            #endregion

            #region HomeService
            services.AddScoped<IPhotoGalleryService, PhotoGalleryService>();
            services.AddScoped<IWellWisherMessageService, WellWisherMessageService>();
            services.AddScoped<IBloodCampaignService, BloodCampaignService>();
            services.AddScoped<IDailyNewsUpdateService, DailyNewsUpdateService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IHospitalDetailsService, HospitalDetailsService>();
            services.AddScoped<IOtherBloodBankDetailsService, OtherBloodBankDetailsService>();
            services.AddScoped<IGeneralFAQsService, GeneralFAQsService>();
            #endregion

            #region AdminPanelService
            services.AddScoped<IAdminPanelService, AdminPanelService>();
            #endregion
            #region NoticeBoardService
            services.AddScoped<INoticeBoardInfoService, NoticeBoardInfoService>();
            #endregion
            #region ComplainServiceS
            services.AddScoped<IComplainInformationDetailsService, ComplainInformationDetailsService>();
            services.AddScoped<IComplainInformationService, ComplainInformationService>();
            services.AddScoped<IComplainSolveInfoDetailsService, ComplainSolveInfoDetailsService>();
            services.AddScoped<IComplainSolveInfoService, ComplainSolveInfoService>();
            #endregion

            #region SocialOrganizatonServiceS
            services.AddScoped<ISocialOrganizationService, SocialOrganizationService>();
            services.AddScoped<ISocialOrganizationBrunchService, SocialOrganizationBrunchService>();
            services.AddScoped<ISocialOrganizationDetailsService, SocialOrganizationDetailsService>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "MyArea",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });



        }
    }
}
