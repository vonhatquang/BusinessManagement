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
            List<WebApiParameter> parameters = new List<WebApiParameter>();            
            WebApiParameter parameter = new WebApiParameter();
            PostItem item = new PostItem();
            //ListCustom
            client.InitializeClient(WebApiConst.LISTCUSTOM);
            List<PostItem> listCustom = await client.ListFromCustomApi<PostItem>();

            //GetCustom
            client.InitializeClient(WebApiConst.GETCUSTOM);
            parameters = new List<WebApiParameter>();  
            parameters.Add(new WebApiParameter(){Name="id2", Value="0"});
            PostItem getCustom = await client.GetFromCustomApi<PostItem>(parameters);

            //List                       
            client.InitializeClient(WebApiConst.LIST);
            List<PostItem> listReturn = await client.ListFromApi<PostItem>();

            //Get
            client.InitializeClient(WebApiConst.GET);
            parameter = new WebApiParameter();
            parameter.Name="id";
            parameter.Value="0";
            PostItem list = await client.GetFromApi<PostItem>(parameter);

            //Post
            client.InitializeClient(WebApiConst.POST);
            item = new PostItem();
            item.value = "2000";
            PostItem post = await client.PostToApi<PostItem>(item);

            //Put
            client.InitializeClient(WebApiConst.PUT);
            parameter = new WebApiParameter();
            parameter.Name="id";
            parameter.Value="0";
            item = new PostItem();
            item.value = "3000";
            PostItem put = await client.PutToApi<PostItem>(parameter,item);

            //Delete
            client.InitializeClient(WebApiConst.DELETE);
            parameter = new WebApiParameter();
            parameter.Name="id";
            parameter.Value="0";
            string delete = await client.DeleteToApi(parameter);


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
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                        IsPersistent = false,
                        AllowRefresh = false
                    });  
                        
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else{
                     
                        return RedirectToAction("Index", "Home", new {area=""});  
                    } 
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
