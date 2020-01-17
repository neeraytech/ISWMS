using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.Models.Models
{
  public class RFIDPunchingDetailsModel
    {
        public int srno { get; set; }
        public int id { get; set; }
        public string RFIDName { get; set; }
        public string RfidNo { get; set; }
        public string PunchingDateTime { get; set; }

    }
}
