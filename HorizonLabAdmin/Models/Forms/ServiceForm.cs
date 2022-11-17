using HorizonLabLibrary.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models.Forms
{
    public class ServiceForm
    {
        public string line_1 { get; set; }
        public string line_2 { get; set; }
        public string line_3 { get; set; }
        public string line_4 { get; set; }
        public string line_5 { get; set; }

        public string id_1 { get; set; }
        public string id_2 { get; set; }
        public string id_3 { get; set; }
        public string id_4 { get; set; }
        public string id_5 { get; set; }

        public List<hlab_web_services_intro> header_list { get; set; }
        public List<Service> service_list { get; set; }
        public List<hlab_service_details> service_object_list { get; set; }
        public string new_service_name { get; set; }
        public IFormFile new_service_icon {get;set;}
    }
}
