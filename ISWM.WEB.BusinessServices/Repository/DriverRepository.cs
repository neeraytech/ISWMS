using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
     public class DriverRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Driver  
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddDriver(driver_master obj)
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
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update Driver details
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyDriver(driver_master obj)
        {
            int update = 0;
            bool isupdate = false;

            driver_master FindObj = db.driver_master.Where(w => w.contact_no == obj.contact_no).FirstOrDefault();
            if (FindObj != null)
            {
                if (FindObj.id == obj.id)
                {
                    //Same Driver so allow to update
                    isupdate = true;
                }
                else
                {
                    //already exists so we can't allow to update
                    update = -1;
                }
            }
            else
            {
                //This is different Driver so allow to update
                isupdate = true;
            }


            if(isupdate)
            {
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
                    update = 1;
                }
                Dispose(true);
            }

           
            return update;

        }

        /// <summary>
        /// This Method Used for delete Driver (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteDriver(driver_master obj)
        {
            int isupdate = 0;
            driver_master updateObj = db.driver_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.driver_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Driver details by using Driver id
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<driver_master> GetDriverByID(int id)
        {
            driver_master updateObj = db.driver_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Driver list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<driver_master>> GetDriverList(int? statusid)
        {
            List<driver_master> objlist = db.driver_master.ToList();
            if (statusid > 0)
            {
                objlist = objlist.Where(d => d.status == statusid).ToList();
            }
            return objlist;
        }

        /// <summary>
        /// This Method used to get driver list for view model
        ///  coder:pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<DriverModule>> GetViewDriverList(string sort, int userid, int usertypeid)
        {
            List<DriverModule> objlist = new List<DriverModule>();

            List<driver_master> list = db.driver_master.ToList();
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
                    DriverModule obj = new DriverModule();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.name = item.name;
                    obj.contact_no = item.contact_no;
                    obj.status_id = item.status;
                    obj = gcm.GetStatusDetails(obj);
                    objlist.Add(obj);
                    i++;
                }
            }

            return objlist;
        }

        /// <summary>
        /// This Method used to dealocate memory
        ///  coder:Pranali Patil
        /// </summary>
        /// <returns></returns>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

    }
}
