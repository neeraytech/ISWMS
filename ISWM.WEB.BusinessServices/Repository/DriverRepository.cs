using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
     public class DriverRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Driver         
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddDriver(driver_master obj)
        {
            int isadd = 0;
            driver_master updateObj = db.driver_master.Where(w => w.contact_no == obj.contact_no).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.driver_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }

            return isadd;

        }

        /// <summary>
        /// This method used for update Driver details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ModifyDriver(driver_master obj)
        {
            bool isupdate = false;
            driver_master updateObj = db.driver_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.name = obj.name;
                updateObj.contact_no = obj.contact_no;
                updateObj.status = obj.status;    
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.driver_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete Driver (only status we change)
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteDriver(driver_master obj)
        {
            bool isupdate = false;
            driver_master updateObj = db.driver_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.driver_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Driver details by using Driver id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public driver_master GetDriverByID(int id)
        {
            driver_master updateObj = db.driver_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Driver list
        /// </summary>
        /// <returns></returns>
        public List<driver_master> GetDriverList()
        {
            List<driver_master> objlist = db.driver_master.ToList();
            return objlist;
        }

    }
}
