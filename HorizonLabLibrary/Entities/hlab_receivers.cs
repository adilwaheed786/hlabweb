using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_receivers
    {
        [Required, Key]
        public int id { get; set; }
        public string receiver { get; set; }
    }
}
