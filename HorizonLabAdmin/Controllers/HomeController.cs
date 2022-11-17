using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HorizonLabAdmin.Helpers.Containers;
using HorizonLabAdmin.Helpers.Utilities;

namespace HorizonLabAdmin.Controllers
{
    public class HomeController : Controller
    {
        private HorizonLabMenu _hlabMenu = new HorizonLabMenu();

        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Index", "Login");//back to login page
            }
            else
            {
                //load Web Admin Page
                _hlabMenu = FunctionLibrary.setActiveMenu(_hlabMenu, "home");
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewBag.menu = _hlabMenu;
                return View();
            }
        }

        public IActionResult Logout()
        {
            //HttpContext.Session.Remove("UserName");
            HttpContext.Session.SetString("UserName", "");
            return View();
        }

        [HttpPost, NonAction]
        ActionResult InsertNewService()
        {            
            return View("Index");
        }

    }
}