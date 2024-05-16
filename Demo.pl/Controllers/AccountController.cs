using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.pl.Controllers;
using Demo.PL.Helper;
using Demo.PL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {

        private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController( UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager )
        {
   			_userManager = userManager;
			_signInManager = signInManager;
		}


        #region Sign Up
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var email = await _userManager.FindByEmailAsync(model.Email);
                if (user is null && email is null)
                {
                    user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        IsAgree = model.IsAgree,
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(SignIn));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                }
                ModelState.AddModelError(string.Empty, "UserName Is already Exits !! ");
            }
            return View(model);
        }
        #endregion

        #region Sign In
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
               var user= await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password); 
                    if(flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user , model.Password , model.RememberMe , false);
                        if (result.Succeeded)
                            return RedirectToAction(nameof(HomeController.Index), controllerName: "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, errorMessage: "Invalid Login !!");
            }
            return View(model); ;
        }

        #endregion

        #region Sign Out

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }


        #endregion

        #region ForgetPassword

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    // create url which send to reset password

                    // generate token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    // generate url
                    var url =  Url.Action("ResetPassword","Account",new {email = model.Email , token = token} , Request.Scheme);
                    // Create Email
                    var email = new Email()
                    {
                        Subject = "Reset Your Password ",
                        Recipient = model.Email,
                        Body = url

                    };
                    // Send Email
                    EmailSettings.Email(email);

                    //Redirect to action
                    return RedirectToAction(nameof(checkYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Invalid Email !");

            }
            return View(nameof(ForgetPassword), model);
        }


        public IActionResult checkYourInbox()

        {
            return View();
        }
        #endregion

        #region  Reset Password

        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"]=email;
            TempData["token"]=token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                if(user is not null)
                {
                   var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(SignIn));
                    
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);   
                    }

                }
            }
            ModelState.AddModelError(string.Empty, "Password Not Match ");
            return View(model);
        }
        #endregion

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
