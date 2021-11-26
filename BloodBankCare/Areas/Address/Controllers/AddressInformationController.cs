using BloodBankCare.Areas.Address.Models;
using BloodBankCare.Data.Entity.Address;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Services.AddressInfoService.Interfaces;
using BloodBankCare.Services.MasterDataService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankCare.Areas.Address.Controllers
{
   [Area("Address")]
    
    public class AddressInformationController : Controller
    {
        public readonly IAddressInfoService AddressInfoService;
        public readonly ICountryService countryService;
        public readonly IDistrictService districtService;
        public readonly IThanaService thanaService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AddressInformationController(IAddressInfoService _AddressInfoService, ICountryService _countryService, IDistrictService _districtService, IThanaService _thanaService, UserManager<ApplicationUser> userManager)
        {
            AddressInfoService = _AddressInfoService;
            countryService = _countryService;
            districtService = _districtService;
            thanaService = _thanaService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AddressInfoViewModel model = new AddressInfoViewModel
            {
                addressInfos = await AddressInfoService.GetAllAddressInfo(),
                countries = await countryService.GetAllCountry(),
                
            };
            return View(model);
        }



        #region Api

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AddressInfoViewModel model)
        {

            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user.Id!=null)
                {
                    AddressInfo data = new AddressInfo
                    {
                        Id = model.AddressInfoId,
                        userId = user.Id,
                        careOf = model.careOf,
                        houseNo = model.houseNo,
                        villageName = model.villageName,
                        postOffice = model.postOffice,
                        addressType = model.addressType,  //present Or Permanent
                                                          //fk
                        CountryId = model.CountryId,
                        DistrictId = model.DistrictId,
                        ThanaId = model.ThanaId,
                        DonorInformationId = model.DonorInformationId,

                    };

                    await AddressInfoService.SaveAddressInfo(data);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Redirect("/Auth/Account/Login");
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
                response = await AddressInfoService.DeleteAddressInfoById(id);
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return Json(response);
        }
        #endregion

    }
}
