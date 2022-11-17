using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabLibrary.Entities
{
    public class hlab_service_details
    {
        [Required, Key]
        public int id { get; set; }

        [Required]
        public string service_detail { get; set; }

        public string description { get; set; }

        [Required]
        public int service_id { get; set; }

    }
}
