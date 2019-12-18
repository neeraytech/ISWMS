using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
   public class UserSecurityAccessModel
    {
        
        public int module_id { get; set; }
        public int action_id { get; set; }
        public string module_name { get; set; }
        public string action_name { get; set; }
        public bool hasAccess { get; set; }



    }
}
