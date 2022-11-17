using HorizonLabLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonLabLibrary.Parameters
{
    public class BulkRequestInsertParameter
    {
        public int project_id { get; set; }
        public DateTime request_date { get; set; }
        public string received_by { get; set; }
        public List<hlab_temp_requests> temporary_request_list { get; set; }
        public List<int> request_delete_list { get; set; }
    }
}
