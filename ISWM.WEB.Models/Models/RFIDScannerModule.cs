using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
    public class RFIDScannerModule
    {

        public int srno { get; set; }
        public int id { get; set; }
        public string scanner_id { get; set; }
        public string user_id { get; set; }
        public string password { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string color_class { get; set; }
    }
}
