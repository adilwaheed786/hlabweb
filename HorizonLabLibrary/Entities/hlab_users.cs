using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HorizonLabLibrary.Entities
{
    public class hlab_users
    {
        [Required, Key]
        public int user_id { get; set; }

        [Required]
        public string fname { get; set; }

        [Required]
        public string lname { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public DateTime date_reg { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }        

        [Required]
        public int access_id { get; set; }

        [Required]
        public bool status { get; set; }

        public string signature_img { get; set; }
        public string role { get; set; }

    }
}
