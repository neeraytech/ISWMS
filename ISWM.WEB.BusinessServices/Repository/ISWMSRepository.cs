using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class ISWMSRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add ISWMS
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddISWMS(ISWMS_master obj)
        {
            int isadd = 0;
            ISWMS_master updateObj = db.ISWMS_master.Where(w =>w.route_id==obj.route_id && w.driver_id == obj.driver_id && w.truck_id == obj.truck_id && w.scanner_id == obj.scanner_id && w.date == obj.date && w.expected_end_time < obj.expected_start_time).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.ISWMS_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update ISWMS details
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ModifyISWMS(ISWMS_master obj)
        {
            bool isupdate = false;
            ISWMS_master updateObj = db.ISWMS_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.route_id = obj.route_id;
                updateObj.driver_id = obj.driver_id;
                updateObj.truck_id = obj.truck_id;
                updateObj.scanner_id = obj.scanner_id;
                updateObj.date = obj.date;
                updateObj.expected_start_time = obj.expected_start_time;
                updateObj.expected_end_time = obj.expected_end_time;
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.ISWMS_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete ISWMS (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteISWMS(ISWMS_master obj)
        {
            bool isupdate = false;
            ISWMS_master updateObj = db.ISWMS_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.ISWMS_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get ISWMS details by using ISWMS id
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ISWMS_master GetISWMSByID(int id)
        {
            ISWMS_master updateObj = db.ISWMS_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get ISWMS list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public List<ISWMS_master> GetISWMSList()
        {
            List<ISWMS_master> objlist = db.ISWMS_master.ToList();
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
