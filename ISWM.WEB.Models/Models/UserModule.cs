using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
   public class UserModule
    {
        public int srno { get; set; }
        public int id { get; set; }
        public string user_name { get; set; }
        public int area_id { get; set; }
        public string area { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }       
        public string state { get; set; }
        public string email { get; set; }
        public string contact_no { get; set; }
        public string pin_code { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string color_class { get; set; }
    }
}
