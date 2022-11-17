using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class customerparameters
    {
        public hlab_customers hlab_customers { get; set; }
        public List<hlab_customer_phone> phone_list { get; set; }
        public List<hlab_customer_email> email_list { get; set; }
        public horizonlabcustomerview horizonlabcustomerview { get; set; }
        public bool save_proceed { get; set; }
        public int next_customer_id { get; set; }
        public int previous_customer_id { get; set; }

        public List<SelectListItem> PublicSelectList { get; set; }
        public List<SelectListItem> SemiPublicSelectList { get; set; }
        public List<SelectListItem> ApproveFinancingSelectList { get; set; }
        public List<SelectListItem> RealEstateSelectList { get; set; }
        public List<SelectListItem> ProvinceSelectList { get; set; }
        public List<SelectListItem> CitySelectList { get; set; }
    }
}
