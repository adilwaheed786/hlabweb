using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models.Forms
{
    public class Service
    {
        public hlab_services service { get; set; }
        public List<hlab_service_details> detail_list { get; set; }
    }
}
