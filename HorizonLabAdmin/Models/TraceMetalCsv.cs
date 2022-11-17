using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonLabAdmin.Models
{
    public class TraceMetalCsv
    {
        [Index(0)]
        public string sample_id { get; set; }

        [Index(1)]
        public string acquisition_date { get; set; }

        [Index(2)]
        public string qc_status { get; set; }

        [Index(3)]
        public string Be9 { get; set; }

        [Index(4)]
        public string B10 { get; set; }

        [Index(5)]
        public string Al_27 { get; set; }

        [Index(6)]
        public string Ti_47 { get; set; }

        [Index(7)]
        public string V_1_51 { get; set; }

        [Index(8)]
        public string V_51 { get; set; }

        [Index(9)]
        public string Sc_45 { get; set; }

        [Index(10)]
        public string Cr_52 { get; set; }

        [Index(11)]
        public string Mn_55 { get; set; }

        [Index(12)]
        public string Fe_1_57 { get; set; }

        [Index(13)]
        public string Fe_2_57 { get; set; }

        [Index(14)]
        public string Co_59 { get; set; }

        [Index(15)]
        public string Ni_60 { get; set; }

        [Index(16)]
        public string Cu_65 { get; set; }

        [Index(17)]
        public string Zn_66 { get; set; }

        [Index(18)]
        public string Ge_72 { get; set; }

        [Index(19)]
        public string As_75 { get; set; }

        [Index(20)]
        public string As_1_75 { get; set; }

        [Index(21)]
        public string Se_82 { get; set; }

        [Index(22)]
        public string Sr_87 { get; set; }

        [Index(23)]
        public string Mo_98 { get; set; }

        [Index(24)]
        public string Rh_103 { get; set; }

        [Index(25)]
        public string Ag_107 { get; set; }

        [Index(26)]
        public string Cd_111 { get; set; }

        [Index(27)]
        public string In_115 { get; set; }

        [Index(28)]
        public string Sb_121 { get; set; }

        [Index(29)]
        public string Sn_118 { get; set; }

        [Index(30)]
        public string Ba_137 { get; set; }

        [Index(31)]
        public string Tb_159 { get; set; }

        [Index(32)]
        public string Tl_205 { get; set; }

        [Index(33)]
        public string Pb_208 { get; set; }

        [Index(34)]
        public string Th_232 { get; set; }

        [Index(35)]
        public string U_238 { get; set; }

        [Index(36)]
        public string Na_23 { get; set; }

        [Index(37)]
        public string Mg_24 { get; set; }

        [Index(38)]
        public string K_39 { get; set; }

        [Index(39)]
        public string Ca_44 { get; set; }

        [Index(40)]
        public string Sc_145 { get; set; }

        [Index(41)]
        public string CI_35 { get; set; }
    }
}
