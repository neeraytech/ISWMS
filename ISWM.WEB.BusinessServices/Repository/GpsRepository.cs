using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
   public class GpsRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Gps         
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddGps(GPS_master obj)
        {
            int isadd = 0;
            GPS_master updateObj = db.GPS_master.Where(w => w.GPS_id.ToLower() == obj.GPS_id.ToLower()).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.GPS_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }

            return isadd;

        }

        /// <summary>
        /// This method used for update Gps details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ModifyGps(GPS_master obj)
        {
            bool isupdate = false;
            GPS_master updateObj = db.GPS_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.GPS_id = obj.GPS_id;
                updateObj.api_url = obj.api_url;
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.GPS_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete Gps (only status we change)
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteGps(GPS_master obj)
        {
            bool isupdate = false;
            GPS_master updateObj = db.GPS_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.GPS_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Gps details by using Gps id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GPS_master GetGpsByID(int id)
        {
            GPS_master updateObj = db.GPS_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Gps list
        /// </summary>
        /// <returns></returns>
        public List<GPS_master> GetGpsList()
        {
            List<GPS_master> objlist = db.GPS_master.ToList();
            return objlist;
        }



    }
}


