using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
   public class WardRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Ward 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddWard(ward_master obj)
        {
            int isadd = 0;
            db.ward_master.Add(obj);
            db.SaveChanges();
            isadd = 1;
            return isadd;

        }
    }
}
