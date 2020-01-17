using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
    public class RouteHouseHoldDModule
    {
        public int srno { get; set; }
        public int id { get; set; }
        public int route_id { get; set; }
        public string route { get; set; }
        public int household_id { get; set; }
        public string household { get; set; }
        public string ward { get; set; }
        public string area { get; set; }
        public string lat { get; set; }
        public string lg { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string color_class { get; set; }
    }
}
