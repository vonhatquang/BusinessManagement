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
using Microsoft.Extensions.Options;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly WebApiSetting _webApiSetting;
        public AccountController(IOptions<WebApiSetting> webApiSetting)
        {
            this._webApiSetting = webApiSetting.Value;
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
            WebApiClient client = new WebApiClient(this._webApiSetting);
            client.InitializeClient(WebApiConst.GET);
            List<WebApiParameter> parameters = new List<WebApiParameter>();
            parameters.Add(new WebApiParameter(){Name="a", Value="10"});
            int getReturn = await client.GetFromApi<int>(parameters);

            
            client.InitializeClient(WebApiConst.LIST);
            parameters = new List<WebApiParameter>();
            parameters.Add(new WebApiParameter(){Name="a", Value="10"});
            parameters.Add(new WebApiParameter(){Name="b", Value="20"});
            List<string> listReturn = await client.ListFromApi<string>(parameters);

            client.InitializeClient(WebApiConst.POST);
            parameters = new List<WebApiParameter>();
            parameters.Add(new WebApiParameter(){Name="a", Value="10"});
            int postReturn = await client.PostToApi(parameters);

            client.InitializeClient(WebApiConst.PUT);
            parameters = new List<WebApiParameter>();
            parameters.Add(new WebApiParameter(){Name="a", Value="10"});
            int putReturn = await client.PutToApi(parameters);

            client.InitializeClient(WebApiConst.DELETE);
            parameters = new List<WebApiParameter>();
            parameters.Add(new WebApiParameter(){Name="a", Value="10"});
            int deleteReturn = await client.DeleteToApi(parameters);
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
