using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class sp_gettestpackagesbycategory
    {
        [Required, Key]
        public int row_id {get; set;}
        public int id { get; set; } //package id
        public string lab_code { get; set; }
        public int category_id { get; set; }
        public string package_ctgry { get; set; }
        public decimal price { get; set; }
        public decimal lab_fee { get; set; }
        public string analysis { get; set; }
        public string sample_container { get; set; }
        public string description { get; set; }
        public int pkg_class_id {get;set;}
        public bool status {get;set;}
        public string gl_accnt_num { get; set; }
    }
}
