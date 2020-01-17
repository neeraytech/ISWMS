using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
   public class GpsRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Gps 
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddGps(GPS_master obj)
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
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update Gps details
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyGps(GPS_master obj)
        {
            bool isupdate = false;
            int isadd = 0;
            GPS_master findobj = db.GPS_master.Where(w => w.GPS_id.ToLower() == obj.GPS_id.ToLower()).FirstOrDefault();
            if (findobj != null)
            {
                if (findobj.id == obj.id)
                {
                    isupdate = true;
                }
                else
                {
                    isadd = -1;
                }
            }
            else
            {
                isupdate = true;
            }

            if (isupdate)
            {
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
                    isadd = 1;
                }
            }
            
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete Gps (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteGps(GPS_master obj)
        {
            int isupdate = 0;
            GPS_master updateObj = db.GPS_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.GPS_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Gps details by using Gps id
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GPS_master> GetGpsByID(int id)
        {
            GPS_master updateObj = db.GPS_master.Find(id);
            return updateObj;

        }
        /// <summary>
        /// This Method used to get ward list
        /// Coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<GPS_master>> GetGpsList(int? statusid)
        {
            List<GPS_master> objlist = db.GPS_master.ToList();
            if (statusid > 0)
            {
                objlist = objlist.Where(w => w.status == statusid).ToList();
            }

            return objlist;
        }

        /// <summary>
        /// This Method used to get Gps list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>

        public async Task<List<GPSModule>> GetViewGpsList(string sort, int userid, int usertypeid)
        {
            List<GPSModule> objlist = new List<GPSModule>();

            List<GPS_master> list = db.GPS_master.ToList();
            if (usertypeid != 1)
            {
                if (userid > 0)
                {
                    list = list.Where(w => w.modified_by == userid).ToList();
                }
            }
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.modified_datetime).ToList();
                }

                int i = 1;
                foreach (var item in list)
                {
                    GPSModule obj = new GPSModule();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.GPS_id = item.GPS_id;
                    obj.api_url = item.api_url;
                    obj.status_id = item.status;
                    obj = gcm.GetStatusDetails(obj);
                    objlist.Add(obj);
                    i++;
                }
            }

            return objlist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }


    }
}


