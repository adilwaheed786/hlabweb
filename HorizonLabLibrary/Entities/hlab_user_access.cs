using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabLibrary.Entities
{
    public class hlab_user_access
    {
        [Required, Key]
        public int access_id { get; set; }

        [Required]
        public string access_name { get; set; }

        [Required]
        public int access_level { get; set; }
    }
}
