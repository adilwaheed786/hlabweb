using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Helpers.Containers
{
    public class ProjectRequestPageObject
    {
        public int selected_project_id { get; set; }
        public int selected_payment_type_id { get; set; }
        public int selected_testpkg_id { get; set; }
        public int selected_project_form_id { get; set; }
        public int row_count { get; set; }
        public int form_id { get; set; }    
        public int proj_form_id { get; set; }    
        public string Message { get; set; }
        public string RequestDate { get; set; }
        public string DateCreated { get; set; }
        public string ReceivedBy { get; set; }
        public string IncubationTemp { get; set; }
        public string SelectedFilter { get; set; }
        public bool isRush{ get; set; }
        public bool isConditionMet{ get; set; }
        public DateTime? IncubationIn { get; set; }
        public DateTime? IncubationOut { get; set; }        
        public DateTime? InputDateRequest { get; set; }        
        public DateTime? _RequestDate { get; set; }        
        public List<temporaryprojectrequestsview> ProjectRequestRecords { get; set; }        
        public projectrequestsformview ProjectRequestFormInfo { get; set; }        
        public List<projectrequestsformview> ProjectRequestForms { get; set; }        
        public List<projectrequestsupplyview> ProjectRequestFormsSettings { get; set; }        
        public List<hlab_temp_requests> temporary_request_list { get; set; }
        public List<hlab_supplies> TestPackageSupplyList { get; set; }
        public List<int> request_delete_list { get; set; }
        public List<SelectListItem> ProjectSelectList { get; set; }
        public List<SelectListItem> FilterOptions { get; set; }
        public List<SelectListItem> IsConditionMetList { get; set; }
        public List<SelectListItem> RushList { get; set; }
        public List<SelectListItem> FormSelectList { get; set; }
        public List<SelectListItem> TestPackageSelectList { get; set; }
        public List<SelectListItem> PaymentSelectList { get; set; }      
        public List<SelectListItem> ReceiverSelectList { get; set; }      
    }
}
