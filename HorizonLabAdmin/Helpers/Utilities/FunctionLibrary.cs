using HorizonLabAdmin.Helpers.Containers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Utilities
{
    public static class FunctionLibrary
    {
        public static HorizonLabMenu setActiveMenu(HorizonLabMenu menu, string activeMenu)
        {
            if(nameof(menu.home) == activeMenu)
            {
                menu.home = "active";
                menu.web = "";
                menu.reports = "";
                menu.user = "";
                menu.transactions = "";
                menu.order = "";
                menu.settings = "";
                menu.help = "";
                menu.waterbacteria = "";
                menu.supply = "";
            }
            else if (nameof(menu.web) == activeMenu)
            {
                menu.home = "";
                menu.web = "active";
                menu.reports = "";
                menu.user = "";
                menu.transactions = "";
                menu.order = "";
                menu.settings = "";
                menu.help = "";
                menu.waterbacteria = "";
                menu.supply = "";
            }
            else if (nameof(menu.reports) == activeMenu)
            {
                menu.home = "";
                menu.web = "";
                menu.reports = "active";
                menu.user = "";
                menu.transactions = "";
                menu.order = "";
                menu.settings = "";
                menu.help = "";
                menu.waterbacteria = "";
                menu.supply = "";
            }
            else if (nameof(menu.user) == activeMenu)
            {
                menu.home = "";
                menu.web = "";
                menu.reports = "";
                menu.user = "active";
                menu.transactions = "";
                menu.order = "";
                menu.settings = "";
                menu.help = "";
                menu.waterbacteria = "";
                menu.supply = "";
            }
            else if (nameof(menu.transactions) == activeMenu)
            {
                menu.home = "";
                menu.web = "";
                menu.reports = "";
                menu.user = "";
                menu.transactions = "active";
                menu.order = "";
                menu.settings = "";
                menu.help = "";
                menu.waterbacteria = "";
                menu.supply = "";
            }
            else if (nameof(menu.customer) == activeMenu)
            {
                menu.home = "";
                menu.web = "";
                menu.reports = "";
                menu.user = "";
                menu.transactions = "";
                menu.customer = "active";
                menu.order = "";
                menu.settings = "";
                menu.help = "";
                menu.waterbacteria = "";
                menu.supply = "";
            }
            else if (nameof(menu.order) == activeMenu)
            {
                menu.home = "";
                menu.web = "";
                menu.reports = "";
                menu.user = "";
                menu.transactions = "";
                menu.customer = "";
                menu.order = "active";
                menu.settings = "";
                menu.help = "";
                menu.waterbacteria = "";
                menu.supply = "";
            }
            else if (nameof(menu.settings) == activeMenu)
            {
                menu.home = "";
                menu.web = "";
                menu.reports = "";
                menu.user = "";
                menu.transactions = "";
                menu.customer = "";
                menu.order = "";
                menu.settings = "active";
                menu.help = "";
                menu.waterbacteria = "";
                menu.supply = "";
            }
            else if (nameof(menu.help) == activeMenu)
            {
                menu.home = "";
                menu.web = "";
                menu.reports = "";
                menu.user = "";
                menu.transactions = "";
                menu.customer = "";
                menu.order = "";
                menu.settings = "";
                menu.help = "active";
                menu.waterbacteria = "";
                menu.supply = "";
            }
            else if (nameof(menu.waterbacteria) == activeMenu)
            {
                menu.home = "";
                menu.web = "";
                menu.reports = "";
                menu.user = "";
                menu.transactions = "";
                menu.customer = "";
                menu.order = "";
                menu.settings = "";
                menu.help = "";
                menu.waterbacteria = "active";
                menu.supply = "";
            }
            else if (nameof(menu.supply) == activeMenu)
            {
                menu.home = "";
                menu.web = "";
                menu.reports = "";
                menu.user = "";
                menu.transactions = "";
                menu.customer = "";
                menu.order = "";
                menu.settings = "";
                menu.help = "";
                menu.waterbacteria = "";
                menu.supply = "active";
            }

            return menu;
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string IntToMilitaryDate(int Time)
        {
            int Hours = Time / 100;
            int Minutes = Time - Hours * 100;
            DateTime Result = DateTime.MinValue;     

            Result = Result.AddHours(Hours);
            Result = Result.AddMinutes(Minutes);

            return Result.ToString("H:mm");
        }
    }
}
