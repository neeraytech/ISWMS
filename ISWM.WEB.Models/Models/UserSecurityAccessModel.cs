using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Model for User Security Access
/// Coder : Pranali Patil
/// </summary>

namespace ISWM.WEB.Models.Models
{
   public class UserSecurityAccessModel
    {
        public int srno { get; set; }
        public int id { get; set; }
        public int user_id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string contact_no { get; set; }
        public int module_id { get; set; }
        public int action_id { get; set; }
        public string module_name { get; set; }
        public string action_name { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string color_class { get; set; }



    }
}
