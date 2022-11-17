using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_transaction_supplies
    {
        [Required, Key]
        public int id { get; set; }
        public int transaction_id { get; set; }
        public int supply_id { get; set; }
    }
}
