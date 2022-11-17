using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HorizonLabLibrary.Entities
{
    public class hlab_trace_metal_results
    {
        [Required, Key]
        public int id { get; set; }
        public int trans_id { get; set; }
        public bool qc_stat { get; set; }
        public string description { get; set; }

        public string Be9 { get; set; }
        public string B10 { get; set; }
        public string Al_27 { get; set; }
        public string Ti_47 { get; set; }
        public string V_1_51 { get; set; }
        public string V_51 { get; set; }
        public string Sc_45 { get; set; }
        public string Cr_52 { get; set; }
        public string Mn_55 { get; set; }
        public string Fe_1_57 { get; set; }
        public string Fe_2_57 { get; set; }
        public string Co_59 { get; set; }
        public string Ni_60 { get; set; }
        public string Cu_65 { get; set; }
        public string Zn_66 { get; set; }
        public string Ge_72 { get; set; }
        public string As_75 { get; set; }
        public string As_1_75 { get; set; }
        public string Se_82 { get; set; }
        public string Sr_87 { get; set; }
        public string Mo_98 { get; set; }
        public string Rh_103 { get; set; }
        public string Ag_107 { get; set; }
        public string Cd_111 { get; set; }
        public string In_115 { get; set; }
        public string Sb_121 { get; set; }
        public string Sn_118 { get; set; }
        public string Ba_137 { get; set; }
        public string Tb_159 { get; set; }
        public string Tl_205 { get; set; }
        public string Pb_208 { get; set; }
        public string Th_232 { get; set; }
        public string U_238 { get; set; }
        public string Na_23 { get; set; }
        public string Mg_24 { get; set; }
        public string K_39 { get; set; }
        public string Ca_44 { get; set; }
        public string Sc_145 { get; set; }
        public string CI_35 { get; set; }
        public DateTime acquisition_date { get; set; }        
    }
}
