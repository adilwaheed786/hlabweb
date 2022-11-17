using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class sp_CountTodaysRequests
    {
        [Required, Key]
        public int total_count { get; set; }
    }
}
