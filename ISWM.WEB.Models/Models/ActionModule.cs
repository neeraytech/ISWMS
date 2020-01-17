using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
   public class ActionModule
    {
        public int srno { get; set; }
        public int id { get; set; }
        public string module_action_name { get; set; }
        public string module_action_desc{ get; set;}
        public string status { get; set; }
        public int status_id { get; set; }
        public string color_class { get; set; }
    }
}
