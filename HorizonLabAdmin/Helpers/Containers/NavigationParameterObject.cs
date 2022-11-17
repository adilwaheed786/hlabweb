using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class NavigationParameterObject
    {
        public List<SelectListItem> selectPackageList { get; set; }
        public List<SelectListItem> sortByList { get; set; }
        public List<SelectListItem> sortOptionList { get; set; }
        public string sortByString { get; set; }
        public string sortByOption { get; set; } //asc or desc
        public int rec_start { get; set; }
        public int rec_end { get; set; }
        public int rec_count { get; set; }
    }
}
