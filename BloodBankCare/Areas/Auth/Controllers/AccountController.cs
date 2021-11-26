using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankCare.Areas.Auth.Models;
using BloodBankCare.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodBankCare.Services.AuthService.Interfaces;
using BloodBankCare.Helpers;
using BloodBankCare.Data.Entity.ApplicationUsers;
using BloodBankCare.Services.MasterDataService.Interfaces;
using BloodBankCare.Models;
using System.ComponentModel.DataAnnotations;

namespace BloodBankCare.Areas.Auth.Controllers
{
    [Authorize]
    [Area("Auth")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserInfoes userInfoes;
        private readonly IBloodGroupService _bloodGroupService;
        private readonly IGenderService _genderService;
        private IPasswordHasher<ApplicationUser> _passwordHasher;
        private IPasswordValidator<ApplicationUser> _passwordValidator;
        private IUserValidator<ApplicationUser> _userValidator;


        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IUserInfoes userInfoes,
            IBloodGroupService bloodGroupService,
            IGenderService genderService,
            IPasswordHasher<ApplicationUser> passwordHash,
            IPasswordValidator<ApplicationUser> passwordVal,
            IUserValidator<ApplicationUser> userValid
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            _bloodGroupService = bloodGroupService;
            _genderService = genderService;
            _passwordHasher = passwordHash;
            _passwordValidator = passwordVal;
            _userValidator = userValid;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var userInfos = await userInfoes.GetUserInfoByUser(model.Name);
                if (userInfos != null)
                {
                    if (userInfos.isActive == 1)
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                            var userAgent = Request.Headers["User-Agent"].ToString();
                            var mechineName = Environment.MachineName;

                            var userRole = await userInfoes.GetUserRoleByUserName(model.Name);
                            if (userRole == "General User") // General User - Database
                            {
                                var user = await userInfoes.GetUserInfoByUser(model.Name);
                                if(user.userType.userTypeName == "Donar")
                                {
                                 
                                    
                                    return RedirectToLocal(returnUrl);
                                }
                                else if(user.userType.userTypeName == "Log User")
                                {
                                    return RedirectToLocal(returnUrl);

                                }
                                else if(user.userType.userTypeName == "Normal user")
                                {
                                    return RedirectToLocal(returnUrl);
                                }
                               
                            }
                      
                            else if (userRole == "Admin") 
                            {
                                return RedirectToLocal(returnUrl);
                            }
                            else{
                                return RedirectToLocal(returnUrl);
                            }
                            //UserLogHistory userLog = new UserLogHistory
                            //{
                            //    userId = model.Name,
                            //    logTime = DateTime.Now,
                            //    status = 1,
                            //    ipAddress = ip,
                            //    pcName = mechineName,
                            //    browserName = userAgent
                            //};

                            //await dbChangeService.SaveUserLogHistory(userLog);


                            return RedirectToLocal(returnUrl);

                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(modelr);
            }
            var model = new RegisterViewModel
            {
                userRoles = lstRole,
                bloodGroups=await _bloodGroupService.GetAllBloodGroup(),
                genders=await _genderService.GetAllGender()
                //Designations = await _designationDepartmentService.GetDesignations(),
                //Ranks = await _rank.GetRanks(),
                //Sections = await _section.GetSections(),
                //Photographs = await _photograph.GetPhotographs(),
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string username = HttpContext.User.Identity.Name;
                //var userinfo = await userInfoes.GetUserInfoByUser(username);
                // var userinfo = await userInfoes.GetSbuIdByEmployeeEmail(model.Email);
                
                var user = new ApplicationUser
                {
                    UserName = model.Name,
                    isActive = 1,
                    PhoneNumber=model.PhoneNumber,
                    PasswordHash=model.Password,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, model.RoleId);
                    //IdentityUser temp = await _userManager.FindByNameAsync(model.Name);
                    //var emp = await personalInfoService.GetEmployeeInfoByCode(model.EmpCode);
                    //emp.ApplicationUserId = temp.Id;
                    //emp.rankId = model.RankName;
                    //emp.sectionId = model.SectionName;
                    //emp.designationsId = model.DesignationName;
                    //await personalInfoService.SaveEmployeeInfo(emp);
                    //return RedirectToLocal("/");
                }

                string fileName;
                string message = FileSave.SaveImage(out fileName, model.ImgeUrl);

                //if (model.EmpId != 0)
                //{
                //    if (message == "success")
                //    {
                //        Photograph data = new Photograph
                //        {
                //            Id = 0,
                //            employeeId = model.EmpId,
                //            url = fileName,
                //            type = "profile"
                //        };
                //        await _photograph.SavePhotograph(data);
                //    }
                //}


                AddErrors(result);
            }

            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(modelr);
            }
            var formData = new RegisterViewModel
            {
                userRoles = lstRole,
                //Designations = await _designationDepartmentService.GetDesignations(),
                //Ranks = await _rank.GetRanks(),
                //Sections = await _section.GetSections(),
                //Photographs = await _photograph.GetPhotographs(),
            };
            return View(formData);

        }

       

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserRoleCreate()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel model = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(model);
            }
            ApplicationRoleViewModel viewModel = new ApplicationRoleViewModel
            {
                roleViewModels = lstRole
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserIdentityRoleCreate([FromForm] ApplicationRoleViewModel model)
        {
            
            if (model.RoleId != null)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                role.Name = model.RoleName;
                IdentityResult result = await _roleManager.UpdateAsync(role);
            }
            else
            {
                var role = new ApplicationRole(model.RoleName);
                IdentityResult result = await _roleManager.CreateAsync(role);
            }
            return RedirectToAction(nameof(UserRoleCreate));
        }
        
      
       [HttpGet]
       public async Task<IActionResult> UserRoleAssign()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(modelr);

            }
            UserListViewModel model = new UserListViewModel
            {
                aspNetUsersViewModels = await userInfoes.GetUserInfoList(),
                userRoles = lstRole,
           
            };
            return View(model);
        }

        public async Task<IActionResult> DeleteRoles(string id)
        {
            try
            {
                await userInfoes.DeleteRoleById(id);
                return Json("Success");
            }
            catch
            {
                return Json("Roles is Already Assigned Someone!!");
            }
        }




        [HttpGet]
        public async Task<IActionResult> UpdateStatus(string Id, int status)
        {
            await userInfoes.UpdateUserStatusByUserIdAndStatus(Id, status);
            return RedirectToAction(nameof(UserList));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            //string userName = HttpContext.User.Identity.Name;
             await userInfoes.DeleteUser(Id);
            return RedirectToAction(nameof(UserList));
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EditRole([FromForm] ApplicationRoleViewModel model)
        {

            ApplicationUser user = await _userManager.FindByNameAsync(model.userName);

            if (model.PreRoleId != null)
            {
                await _userManager.RemoveFromRoleAsync(user, model.PreRoleId);
            }
            await _userManager.AddToRoleAsync(user, model.RoleId);
            return RedirectToAction(nameof(UserList));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            return Redirect("~/Home/Index");
      
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePsswordViewModel model)
        {
            string message = "Fail To Update Password";
            if (ModelState.IsValid)
            {
                var data = await _userManager.ChangePasswordAsync(await _userManager.FindByNameAsync(HttpContext.User.Identity.Name), model.OldPassword, model.Password);
                message = data.ToString();
            }

 
            return RedirectToAction(nameof(HomeController.Index), "Home", new { Message = message });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword()
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel
            {
                

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    ApplicationUser user = await _userManager.FindByNameAsync(model.Name);
                    var result = await _userManager.RemovePasswordAsync(user);
                    var results = await _userManager.AddPasswordAsync(user, model.Password);
                    string filter = model.Name;
                    if (results.Succeeded)
                    {
                        TempData["Success"] = "Password Changed Successfully!";
                    }
                    else
                    {
                        AddErrors(results);
                    }

                }
               

                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                return Json("Try again");
            }
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public async Task<IActionResult> UserProxyByAdmin()
        {
            string userName = HttpContext.User.Identity.Name;
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(modelr);
            }
            UserListViewModel model = new UserListViewModel
            {
                userRoles = lstRole,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SwitchedUser(string userId, string securityCode)
        {
            string userName = HttpContext.User.Identity.Name;
            string returnUrl = "/";
            ApplicationUser user = await _userManager.FindByNameAsync(userId);
            if (user != null && securityCode == "na.nab")
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToLocal(returnUrl);

            }
            else
            {
                return RedirectToAction(nameof(UserProxyByAdmin));
            }
        }
        #region GeneralUser
        [AllowAnonymous]
        [HttpGet]
        public async Task<string> RestrictDuplicateUserName(string uName)
        {
            return await userInfoes.CheckUserName(uName);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<string> RestrictDuplicateEmail(string email)
        {
            return await userInfoes.CheckEmail(email);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<string> RestrictDuplicatePhone(string MobileNum)
        {
            return await userInfoes.CheckPhone(MobileNum);
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneralRegister(RegisterViewModel model,string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Name,
                    isActive = 1,
                    Email = model.Email,
                    PhoneNumber=model.PhoneNumber,
                    userTypeId=1,
                    createdAt = DateTime.Now,
                    GenderId=model.GenderId,
                    BloodGroupId=model.BloodGroupId
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "General User");
                    var login = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: true);
                    return Redirect("~/DashBoard/UserDashboard");
                    
                }
              
                AddErrors(result);
            }
         
            return View();
          

        }




        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRegistrationInfo(UserDashBoardViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = new IdentityResult();
                ApplicationUser user = await _userManager.FindByIdAsync(model.ApplicationUserId);
              
                if (user != null)
                {
                    IdentityResult validEmail = null;
                    if (!string.IsNullOrEmpty(model.Email))
                    {
                        validEmail = await _userValidator.ValidateAsync(_userManager, user);
                        if (validEmail.Succeeded)
                            user.Email = model.Email;
                        else
                            Errors(validEmail);
                    }
                    else
                        ModelState.AddModelError("", "Email cannot be empty");

                    IdentityResult validPass = null;
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        validPass = await _passwordValidator.ValidateAsync(_userManager, user, model.Password);
                            if (validPass.Succeeded)
                                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                            else
                                Errors(validPass);
                       
                        
                    }
                    else
                        ModelState.AddModelError("", "Password cannot be empty");

                    if (validEmail != null && validPass != null && validEmail.Succeeded && validPass.Succeeded)
                    {


                     
                        user.isActive = 1;
                        user.Email = model.Email;
                        user.PhoneNumber = model.PhoneNumber;
                        user.userTypeId = 1;
                        user.createdAt = DateTime.Now;
                        user.GenderId = model.GenderId;
                        user.BloodGroupId = model.BloodGroupId;
                        

                        result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                            return Redirect("/DashBoard/UserDashboard");
                        else
                            Errors(result);
                    }
                }
                else
                    ModelState.AddModelError("", "User Not Found");

                 result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "General User");
                    var login = await _signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, lockoutOnFailure: true);
                    return Redirect("/DashBoard/UserDashboard");

                }

                AddErrors(result);
            }

            return View();


        }

        #endregion

        #region User Management
        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(modelr);
            }

            UserListViewModel model = new UserListViewModel
            {
                aspNetUsersViewModels = await userInfoes.GetUserInfoList(),
                userRoles = lstRole,
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(modelr);
            }
            var model = new RegisterViewModel
            {
                userRoles = lstRole,
              
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // string username = HttpContext.User.Identity.Name;
          
                var user = new ApplicationUser
                {
                    UserName = model.Name,
                    isActive = 1,
                    Email = model.Email,
                    createdAt=DateTime.Now
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleId);
                    if (model.RoleId== "donar")
                    {
                        //await userInfoes.UpdatedonarIdById(model.donarId,user.Id);
                    }
                    else if (model.RoleId== "general user")
                    {
                        //await userInfoes.generalUser(model.id, user.Id);
                    }
                    //IdentityUser temp = await _userManager.FindByNameAsync(model.Name);
                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction(nameof(UserList));
                }
                AddErrors(result);
            }

            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(modelr);
            }
            var formData = new RegisterViewModel
            {
                userRoles = lstRole,
               
            };
            return View(formData);
        }



        //[HttpGet]
        //public async Task<IActionResult> UserAssignPage(string userTypeId)
        //{
        //    try
        //    {
        //        var data = await userInfoes.GetUserMenuAccessByUserType(userTypeId);
        //        return Json(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
        #region ForGotPassword
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            if (!ModelState.IsValid)
                return View(email);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPasswordByToken", "Account", new { token, email = user.Email }, Request.Scheme);

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            if (emailResponse)
                return RedirectToAction("ForgotPasswordConfirmation");
            else
            {
                // log email failed 
            }
            return View(email);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordByToken(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordByToken(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        #endregion 
        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                //return Redirect(returnUrl);
                var userId = HttpContext.User.Identity.Name;

                //return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
                return Redirect("~/DashBoard/UserDashboard");
            }
            else
            {
                var userId = HttpContext.User.Identity.Name;

                //return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
                return Redirect("~/Bloodbank/BloodRequest/Index");
            }
        }




        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion



  

    }
}