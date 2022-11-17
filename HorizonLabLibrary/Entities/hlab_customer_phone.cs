using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_customer_phone
    {
        [Required, Key]
        public int id { get; set; }
        public int customer_id { get; set; }
        public string phone { get; set; }
    }
}
