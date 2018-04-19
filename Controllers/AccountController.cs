using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;  
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using WebApp.Models;
using WebApp.Helpers.WebApi;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        /*public IActionResult Index(string ReturnUrl)
        {
            //ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }*/
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> UserLogin(LoginUserModel model)  
        {    
            string userName = model.UserName;
            WebApiClient client = new WebApiClient(this._configuration);
            client.InitializeClient(WebApiConst.CALCULATE_NUMBER);
            int cal = await client.GetFromApi<int>();
            if (ModelState.IsValid)  
            {  
                //string LoginStatus = objUser.ValidateLogin(user);  
  
                /*if (UserID.Equals("1") && Password.Equals(1))  
                { */ 
                    var claims = new List<Claim>  
                    {  
                        /*new Claim(ClaimTypes.Name, UserID)  */
                        new Claim(ClaimTypes.Name, "1")  
                    };  
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");  
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);  
  
                    await HttpContext.SignInAsync(principal,new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddSeconds(5),
                        IsPersistent = false,
                        AllowRefresh = false
                    });  
                    return RedirectToAction("Index", "Home", new {area=""});  
                /*}  
                else  
                {  
                    TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";  
                    return View();  
                }  */
            }  
            else  
                return View();  
  
        }  
        public async Task<IActionResult> UserLogout(/*string UserID, string Password*/)  
        {    
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new {area=""});  

        }  
    }
}
