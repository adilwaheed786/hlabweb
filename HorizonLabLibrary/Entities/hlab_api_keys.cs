using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_api_keys
    {
        [Required, Key]
        public Guid id { get; set; }

        [Required]
        public string consumer { get; set; }
    }
}
