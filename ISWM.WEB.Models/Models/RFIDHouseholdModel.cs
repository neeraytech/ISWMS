using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// model for RFID Household Details
/// coder:Smruti Wagh
/// </summary>
namespace ISWM.WEB.Models.Models
{
   public class RFIDHouseholdModel
    {
        public int srno { get; set; }
        public int id { get; set; }
        public string RFID { get; set; }
        public string RFID_description { get; set; }
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
