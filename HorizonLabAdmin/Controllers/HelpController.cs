using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HorizonLabAdmin.Controllers
{
    public class HelpController : Controller
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();

        public IActionResult Index()
        {
            _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "help");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName"))) return RedirectToAction("Index", "Login");//back to login page                                
            ViewData["PageTitle"] = "Help Modules";
            ViewBag.menu = _hlabMenu;
            return View();
        }
    }
}