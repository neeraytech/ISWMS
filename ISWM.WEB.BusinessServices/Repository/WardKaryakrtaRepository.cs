using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class WardKaryakrtaRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Ward Karyakrta         
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddWard(ward_Karyakrta_master obj)
        {
            int isadd = 0;
            ward_Karyakrta_master updateObj = db.ward_Karyakrta_master.Where(w => w.ward_id == obj.ward_id && w.area_id==w.area_id && w.karyakarta_id==w.karyakarta_id).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.ward_Karyakrta_master.Add(obj);
                db.SaveChanges();

                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update ward Karyakrta details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ModifyWard(ward_Karyakrta_master obj)
        {
            bool isupdate = false;
            ward_Karyakrta_master updateObj = db.ward_Karyakrta_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.ward_id = obj.ward_id;
                updateObj.area_id = obj.area_id;
                updateObj.karyakarta_id = obj.karyakarta_id;
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.ward_Karyakrta_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete ward Karyakrta (only status we change)
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteWard(ward_Karyakrta_master obj)
        {
            bool isupdate = false;
            ward_Karyakrta_master updateObj = db.ward_Karyakrta_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.ward_Karyakrta_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            //Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get ward details by using ward Karyakrta id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ward_Karyakrta_master GetWardByID(int id)
        {
            ward_Karyakrta_master updateObj = db.ward_Karyakrta_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get ward Karyakrta list
        /// </summary>
        /// <returns></returns>
        public List<ward_Karyakrta_master> GetWardList()
        {
            List<ward_Karyakrta_master> objlist = db.ward_Karyakrta_master.ToList();
            return objlist;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
