using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
    public class TruckTrackingHistoryModule
    {
        public int srno { get; set; }
        public int id { get; set; }
        public string truck_number { get; set; }
        public string status { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string speed { get; set; }
        public string Ignition { get; set; }
        public string packetdatetime { get; set; }
        public string color_class { get; set; }
    }
}
