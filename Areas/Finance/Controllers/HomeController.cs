using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;  
using Microsoft.AspNetCore.Authorization; 
using BusinessManagement.Models;

namespace BusinessManagement.Areas.Finance.Controllers
{
    [Area("Finance")]
    public class HomeController : Controller
    {
        [Authorize]  
        public IActionResult Index()
        {
            return View();
        }
    }
}
