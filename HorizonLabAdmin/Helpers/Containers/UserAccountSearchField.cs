using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class UserAccountSearchField
    {
        public List<SelectListItem> searchOption { get; set; }
        public List<hlab_users> userList { get; set; }
        public string search_String { get; set; }
        public string search_By { get; set; }
        public string status_label { get; set; }
        public bool search_UserAccountStatus;
    }
}
