using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
   public class ISWMSModel
    {
        public int srno { get; set; }
        public int id { get; set; }
        public int route_id { get; set; }
        public int driver_id { get; set; }
        public int truck_id { get; set; }
        public int scanner_id { get; set; }
        public string route { get; set; }
        public string driver { get; set; }
        public string contact_no { get; set; }
        public string truck { get; set; }
        public string scanner { get; set; }
        public string date { get; set; }
        public string expected_start_time { get; set; }
        public string expected_end_time { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string color_class { get; set; }
    }
}
