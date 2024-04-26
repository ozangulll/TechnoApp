using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.WebUI.Extension;
using ShopApp.WebUI.Identity;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private IEmailSender _emailSender;
        private ICartService _cartService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IEmailSender emailSender,ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender=emailSender;
            _cartService=cartService;
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });

                // send email
                await _emailSender.SendEmailAsync(model.Email,"Confirm Your Mail.",$"Please  <a href='http://localhost:5110{callbackUrl}'> click the link </a> to confirm your mail adress.");
                    TempData.Put("message",new ResultMessage{
                    Title="Confirm Mail",
                    Message="We sent a confirmation mail to you. Please confirm your email.",
                    Css="warning"
                }); 
                return RedirectToAction("Login", "Account");
            }


            ModelState.AddModelError("", "There is a unkown error, please try later.");
            return View(model);
        }


        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "There is no account which has this username.");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Please confirm your account with Email.");
                return View(model);
            }


            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }

            ModelState.AddModelError("", "Username or password incorrect.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
                TempData.Put("message",new ResultMessage{
                    Title="Log Out",
                    Message="Your accounts have been terminated securely.",
                    Css="warning"
                });
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId==null || token ==null)
            {
                 TempData.Put("message",new ResultMessage{
                    Title="Confirm",
                    Message="Your informations are wrong to confirm account.",
                    Css="danger"
                });
                return Redirect("~/");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user!=null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    //create cart object
                    _cartService.InitializeCart(user.Id);
                     TempData.Put("message",new ResultMessage{
                    Title="Confirm",
                    Message="Your account confirmed succesfully",
                    Css="success"
                });
                    return RedirectToAction("Login");
                }
            }

            TempData.Put("message",new ResultMessage{
                    Title="Confirm",
                    Message="Your accounts was not confirmed",
                    Css="warning"
                });
            return View();
        }
        public IActionResult ForgotPassword(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email){
            if(string.IsNullOrEmpty(Email)){
                 TempData.Put("message",new ResultMessage{
                    Title="Forgot Password",
                    Message="Your information are incorrect",
                    Css="warning"
                });
                return View();
            }
            var user= await _userManager.FindByEmailAsync(Email);
            if(user==null){
                 TempData.Put("message",new ResultMessage{
                    Title="Forgot Password",
                    Message="An account could not be found with this email address.",
                    Css="warning"
                });
                return View();
            }
            var token= await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new
                {
                    
                    token = token
                });

                // send email
                await _emailSender.SendEmailAsync(Email,"Reset Your Password",$"Please  <a href='http://localhost:5110{callbackUrl}'> click the link </a> to reset your password"); 
         TempData.Put("message",new ResultMessage{
                    Title="Reset Password",
                    Message="We sent a mail you to reset your password",
                    Css="success"
                });
                return RedirectToAction("Login", "Account");    
        }
        public IActionResult ResetPassword(string token){
            if(token==null){
                return RedirectToAction("Home","Index");
            }
            var model=new ResetPasswordModel{
                Token=token
            };

            return View(model);

        }
        [HttpPost]
         public async Task<IActionResult> ResetPassword(ResetPasswordModel model){
                if(!ModelState.IsValid){
                    return View(model);
                }
                var user=await _userManager.FindByEmailAsync(model.Email);
                if(user==null){
                    return RedirectToAction("Home","Index");
                }
                var result= await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
                if(result.Succeeded){
                    return RedirectToAction("Login","Account");
                }
            return View();

        }
        
        public IActionResult AccessDenied(){
            return View();
        }

        }


    }

