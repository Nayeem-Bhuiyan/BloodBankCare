using BloodBankCare.Areas.Bloodbank.Models;
using BloodBankCare.Data.Entity.Address;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Data.Entity.Bloodbank;
using BloodBankCare.Helpers;
using BloodBankCare.Models;
using BloodBankCare.Services.AddressInfoService.Interfaces;
using BloodBankCare.Services.BloodbankService.Interfaces;
using BloodBankCare.Services.MasterDataService.Interfaces;
using BloodBankCare.Services.SocialOrganizationInfoService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Bloodbank.Controllers
{
   [Area("Bloodbank")]
    public class DonorInfoController : Controller
    {
        public readonly IDonorInformationService donorInformationService;
        public readonly ISocialOrganizationService socialOrganizationService;
        public readonly IProfessionService professionService;
        public readonly IReligionService religionService;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly ICountryService countryService;
        public readonly IAddressInfoService AddressInfoService;
        public DonorInfoController(IDonorInformationService _DonorInformationService,
            ISocialOrganizationService _socialOrganizationService,
            IProfessionService _professionService,
            IReligionService _religionService,
            UserManager<ApplicationUser> userManager,
            ICountryService _countryService,
             IAddressInfoService _AddressInfoService
         )
        {
            donorInformationService = _DonorInformationService;
            socialOrganizationService = _socialOrganizationService;
            professionService = _professionService;
            religionService = _religionService;
            _userManager = userManager;
            countryService = _countryService;
            AddressInfoService = _AddressInfoService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DonorInformationViewModel model = new DonorInformationViewModel
            {
                donorInformations = await donorInformationService.GetAllDonorInformation(),
                religions=await religionService.GetAllReligion(),
                professions=await professionService.GetAllProfession(),
                socialOrganizations=await socialOrganizationService.GetAllSocialOrganization(),
                countries=await countryService.GetAllCountry()
            };
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> DonorByBloodGroup(int? id)
        {
            var data = await donorInformationService.GetDonorInfoByBloodGroup(id);
            if (data != null)
            {
                DonorInformationViewModel model = new DonorInformationViewModel
                {
                    donorInformationWithUserDetails = data

                };
                return View(model);
            }
            else
            {
                return View();
            }

        }


        #region DonorInfo

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DonorInformationViewModel model)
        {
            int ans = 0;
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var donor = await donorInformationService.GetDonorInformationByUserId(user.Id);
            try
            {
                string ImageUrl = "DefaultImage/NoImage.jpg";
                string fileName;
                string message = FileSave.SaveDonorImage(out fileName, model.DonorImage);
                if (message == "success")
                {
                    ImageUrl = "";
                    ImageUrl = fileName;
                }
                else
                {
                    return View(model);
                }

                if (user.Id!=null)
                {

                    DonorInformation data = new DonorInformation
                    {
                        Id = donor!=null?donor.Id: model.DonorInformationId,
                        imageUrl = ImageUrl,
                        userId=user.Id,
                        designation=model.designation,
                        age=model.age,
                        nationalId=model.nationalId,
                        SocialOrganizationId=model.SocialOrganizationId,
                        ProfessionId=model.ProfessionId,
                        ReligionId=model.ReligionId,
                        ApplicationUserID= user.Id,
                    };

                    ans=await donorInformationService.SaveDonorInformation(data);


                    if (ans==1 && donor==null)
                    {
                        //save donor address information

                        AddressInfo permanentAddress = new AddressInfo
                        {
                            Id = model.PermanentAddressId,
                            userId = user.Id,
                            careOf = model.careOf,
                            houseNo = model.houseNo,
                            villageName = model.villageName,
                            postOffice = model.postOffice,
                            addressType =0,  //0=Permanent,1=present
                           //fk
                            CountryId = model.CountryId,
                            DistrictId = model.DistrictId,
                            ThanaId = model.ThanaId,
                            DonorInformationId = data.Id,

                        };
                        //save permanent Address
                        await AddressInfoService.SaveAddressInfo(permanentAddress);

                        AddressInfo presentAddress = new AddressInfo();
                        if (model.addressType==1)
                        {
                             presentAddress = new AddressInfo
                            {
                                Id = model.PermanentAddressId,
                                userId = user.Id,
                                careOf = model.careOf,
                                houseNo = model.houseNo,
                                villageName = model.villageName,
                                postOffice = model.postOffice,
                                addressType = 1,  //0=Permanent,1=present
                                                  //fk
                                CountryId = model.CountryId,
                                DistrictId = model.DistrictId,
                                ThanaId = model.ThanaId,
                                DonorInformationId = data.Id,

                            };
                        }
                        else
                        {
                            presentAddress = new AddressInfo
                            {
                                Id = model.PresentAddressId,
                                userId = user.Id,
                                careOf = model.PresentcareOf,
                                houseNo = model.PresenthouseNo,
                                villageName = model.PresentvillageName,
                                postOffice = model.PresentpostOffice,
                                addressType = 1,  //0=Permanent,1=present
                                                  //fk
                                CountryId = model.PresentCountryId,
                                DistrictId = model.PresentDistrictId,
                                ThanaId = model.PresentThanaId,
                                DonorInformationId = data.Id,

                            };
                        }
                        //save Present Address
                        await AddressInfoService.SaveAddressInfo(presentAddress);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        //update donor address information
                       var permAddress= await AddressInfoService.GetDonorPermanentAddress(donor.Id);

                        AddressInfo permanentAddress = new AddressInfo
                        {
                            Id = permAddress.Id,
                            userId = user.Id,
                            careOf = model.careOf,
                            houseNo = model.houseNo,
                            villageName = model.villageName,
                            postOffice = model.postOffice,
                            addressType = 0,  //0=Permanent,1=present
                                              //fk
                            CountryId = model.CountryId,
                            DistrictId = model.DistrictId,
                            ThanaId = model.ThanaId,
                            DonorInformationId = data.Id,

                        };
                        //Update permanent Address
                        await AddressInfoService.SaveAddressInfo(permanentAddress);

                        var presAddress = await AddressInfoService.GetDonorPresentAddress(donor.Id);

                        AddressInfo presentAddress = new AddressInfo();
                        
                            presentAddress = new AddressInfo
                            {
                                Id = presAddress.Id,
                                userId = user.Id,
                                careOf = model.careOf,
                                houseNo = model.houseNo,
                                villageName = model.villageName,
                                postOffice = model.postOffice,
                                addressType = 1,  //0=Permanent,1=present
                                                  //fk
                                CountryId = model.CountryId,
                                DistrictId = model.DistrictId,
                                ThanaId = model.ThanaId,
                                DonorInformationId = data.Id,

                            };
                        
                        
                        //Update Present Address
                        await AddressInfoService.SaveAddressInfo(presentAddress);
                        return RedirectToAction(nameof(Index));

                    }
                   
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            bool response = false;
            try
            {
                response = await donorInformationService.DeleteDonorInformationById(id);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return Json(response);
        }

        #endregion


        #region UserDashBoard
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDonorInfo([FromForm] UserDashBoardViewModel model)
        {

            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var donor = await donorInformationService.GetDonorInformationByUserId(user.Id);
            try
            {
                string ImageUrl = "DefaultImage/NoImage.jpg";
                string fileName;
                string message = FileSave.SaveDonorImage(out fileName, model.DonorImage);
                if (message == "success")
                {
                    ImageUrl = "";
                    ImageUrl = fileName;
                }
                else
                {
                    return View(model);
                }

                if (user.Id != null)
                {

                    DonorInformation data = new DonorInformation
                    {
                        Id = donor != null ? donor.Id : model.DonorInformationId,
                        imageUrl = ImageUrl,
                        userId = user.Id,
                        designation = model.designation,
                        age = model.age,
                        nationalId = model.nationalId,
                        SocialOrganizationId = model.SocialOrganizationId,
                        ProfessionId = model.ProfessionId,
                        ReligionId = model.ReligionId,
                        ApplicationUserID = user.Id,
                    };

                    await donorInformationService.SaveDonorInformation(data);

                    return Redirect("/DashBoard/UserDashboard");
                }

            }
            catch (Exception)
            {
                throw;
            }
            return Redirect("/DashBoard/UserDashboard");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDonorPermanentAddress([FromForm] UserDashBoardViewModel model)
        {
            AddressInfo permanentAddress = new AddressInfo();
            ApplicationUser user = new ApplicationUser();

            if (User.Identity.Name == null || User.Identity.Name == "")
            {

                return RedirectToAction("Login", "Account", new { area = "Auth" });
            }
            else
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }


            if (user.Id == null || user.Id == "")
            {
                return Redirect("/Auth/Account/Login");
            }
            else
            {
                
                try
                {
                    var donor = await donorInformationService.GetDonorInformationByUserId(user.Id);
                    if (donor != null)
                    {

                        var permAddress = await AddressInfoService.GetDonorPermanentAddress(donor.Id);

                        if (permAddress == null)
                        {
                            //save donor address information

                            permanentAddress = new AddressInfo
                            {
                                Id = 0,
                                userId = user.Id,
                                careOf = model.careOf,
                                houseNo = model.houseNo,
                                villageName = model.villageName,
                                postOffice = model.postOffice,
                                addressType = 0,  //0=Permanent,1=present
                                                  //fk
                                CountryId = model.CountryId,
                                DistrictId = model.DistrictId,
                                ThanaId = model.ThanaId,
                                DonorInformationId = donor.Id,

                            };
                            //save permanent Address
                            await AddressInfoService.SaveAddressInfo(permanentAddress);
                            return Redirect("/DashBoard/UserDashboard");
                        }
                        else
                        {
                            //update donor address information


                            permanentAddress = new AddressInfo
                            {
                                Id = permAddress.Id,
                                userId = user.Id,
                                careOf = model.careOf,
                                houseNo = model.houseNo,
                                villageName = model.villageName,
                                postOffice = model.postOffice,
                                addressType = 0,  //0=Permanent,1=present
                                                  //fk
                                CountryId = model.CountryId,
                                DistrictId = model.DistrictId,
                                ThanaId = model.ThanaId,
                                DonorInformationId = donor.Id,

                            };
                            //Update permanent Address
                            await AddressInfoService.SaveAddressInfo(permanentAddress);
                            return Redirect("/DashBoard/UserDashboard");

                        }

                    }
                    else
                    {
                        return Redirect("/DashBoard/UserDashboard");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDonorPresentAddress([FromForm] UserDashBoardViewModel model)
        {
            AddressInfo presentAddress = new AddressInfo();
            ApplicationUser user = new ApplicationUser();

            if (User.Identity.Name == null || User.Identity.Name == "")
            {

                return RedirectToAction("Login", "Account", new { area = "Auth" });
            }
            else
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }


            if (user.Id == null || user.Id == "")
            {
                return Redirect("/Auth/Account/Login");
            }
            else
            {
                var donor = await donorInformationService.GetDonorInformationByUserId(user.Id);
                try
                {

                    if (donor != null)
                    {

                        var DonorPresentAddress = await AddressInfoService.GetDonorPresentAddress(donor.Id);

                        if (DonorPresentAddress == null)
                        {
                            //save donor address information

                           

                            presentAddress = new AddressInfo
                            {
                                Id = 0,
                                userId = user.Id,
                                careOf = model.PresentcareOf,
                                houseNo = model.PresenthouseNo,
                                villageName = model.PresentvillageName,
                                postOffice = model.PresentpostOffice,
                                addressType = 1,  //0=Permanent,1=present
                                                  //fk
                                CountryId = model.PresentCountryId,
                                DistrictId = model.PresentDistrictId,
                                ThanaId = model.PresentThanaId,
                                DonorInformationId = donor.Id,

                            };
                            //save permanent Address
                            await AddressInfoService.SaveAddressInfo(presentAddress);
                            return Redirect("/DashBoard/UserDashboard");
                        }
                        else
                        {
                            //update donor address information


                            

                            presentAddress = new AddressInfo
                            {
                                Id = DonorPresentAddress.Id,
                                userId = user.Id,
                                careOf = model.PresentcareOf,
                                houseNo = model.PresenthouseNo,
                                villageName = model.PresentvillageName,
                                postOffice = model.PresentpostOffice,
                                addressType = 1,  //0=Permanent,1=present
                                                  //fk
                                CountryId = model.PresentCountryId,
                                DistrictId = model.PresentDistrictId,
                                ThanaId = model.PresentThanaId,
                                DonorInformationId = donor.Id,

                            };
                            //Update permanent Address
                            await AddressInfoService.SaveAddressInfo(presentAddress);
                            return Redirect("/DashBoard/UserDashboard");

                        }

                    }
                    else
                    {
                        return Redirect("/DashBoard/UserDashboard");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion
    }
}
