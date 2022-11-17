using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_test_payment_types
    {
        [Required, Key]
        public int id { get; set; }
        public string payment { get; set; }
    }
}
