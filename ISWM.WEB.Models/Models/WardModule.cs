using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
    public class WardModule
    {
        public int srno { get; set; }
        public int id { get; set; }
        public string ward_number { get; set; }
        public string ward_description { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string color_class { get; set; }
        
    }
}
