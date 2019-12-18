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
            ward_master updateObj = db.ward_master.Where(w=>w.ward_number.ToLower()==obj.ward_number.ToLower()).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.ward_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
               
            return isadd;

        }

        /// <summary>
        /// This method used for update ward details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ModifyWard(ward_master obj)
        {
            bool isupdate = false;
            ward_master updateObj = db.ward_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.ward_number = obj.ward_number;
                updateObj.ward_description = obj.ward_description;
                updateObj.status = obj.status;
                updateObj.karyakarta_id = obj.karyakarta_id;
                updateObj.modified_by = obj.modified_by;                
                updateObj.modified_datetime = obj.modified_datetime;
                db.ward_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete ward (only status we change)
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteWard(ward_master obj)
        {
            bool isupdate = false;
            ward_master updateObj = db.ward_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;               
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.ward_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

       /// <summary>
       /// This Method Used to get ward details by using ward id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public ward_master GetWardByID(int  id)
        {
            ward_master updateObj = db.ward_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get ward list
        /// </summary>
        /// <returns></returns>
        public List<ward_master> GetWardList()
        {
            List<ward_master> objlist = db.ward_master.ToList();
            return objlist;
        }



    }
}
