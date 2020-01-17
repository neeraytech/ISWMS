using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
    public class GPS_API_Module
    {
        public int counts { get; set; }
        public string packetdatetime { get; set; }
        public string vehicleName { get; set; }
        public string STATUS { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string speed { get; set; }
        public string Ignition { get; set; }
    }
}
