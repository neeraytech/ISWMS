using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// model for User Type module
/// coder:Smruti Wagh
/// </summary>
namespace ISWM.WEB.Models.Models
{
   public class UserTypeModuleModel
    {
        public int srno { get; set; }
        public int id { get; set; }
        public int user_type_id { get; set; }
        public int module_id { get; set; }
        public string user_type { get; set; }
        public string module { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string color_class { get; set; }
    }
}
