using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// model for Ward karyakarta
/// coder: Smruti Wagh
/// </summary>
namespace ISWM.WEB.Models.Models
{
   public class WardKarykartaModel
    {
        public int srno { get; set; }
        public int id { get; set; }
        public int ward_id { get; set; }
        public int area_id { get; set; }
        public int karyakarta_id { get; set; }
        public string ward { get; set; }
        public string area { get; set; }
        public string karyakarta { get; set; }
        public string contact_no { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public string color_class { get; set; }
    }
}
