using System;
using System.Collections.Generic;
using System.Text;

namespace AppIsphereTravelers.Models
{
    public class tbl_emergency
    {
        public long id_emergency { get; set; }
        public string name_emergency { get; set; }
        public string kinship_emergency { get; set; }
        public string phone_emergency { get; set; }
        public string email_emergency { get; set; }
        public string id_emergency_id_profile { get; set; }
        public string pass { get; set; }
        public string idprofile_bw { get; set; }
    }
}
