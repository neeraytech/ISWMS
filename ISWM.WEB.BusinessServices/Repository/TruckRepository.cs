using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class TruckRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Truck
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddTruck(truck_master obj)
        {
            int isadd = 0;
            truck_master updateObj = db.truck_master.Where(w => w.truck_no.ToLower() == obj.truck_no.ToLower() || w.GPS_id==obj.GPS_id).FirstOrDefault();
            if (updateObj != null)
            {                
                isadd = -1;
            }
            else
            {
                //Console.WriteLine(obj.truck_no.Replace(" ",""));
                obj.truck_no = Regex.Replace(obj.truck_no, @"\s", "").ToUpper();
                db.truck_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update Truck details
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyTruck(truck_master obj)
        {
            int update = 0;
            bool isupdate = false;

            truck_master FindObj = db.truck_master.Where(w => w.truck_no.ToLower() == obj.truck_no.ToLower() || w.GPS_id == obj.GPS_id).FirstOrDefault();
            if (FindObj != null)
            {
                if (FindObj.id == obj.id)
                {
                    //Same Truck so allow to update
                    isupdate = true;
                }
                else
                {
                    //already exists so we can't allow to update
                    update = -1;
                }
            }
          
            if (isupdate)
            {
                truck_master updateObj = db.truck_master.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.truck_no = obj.truck_no;
                    updateObj.GPS_id = obj.GPS_id;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    updateObj.truck_no = Regex.Replace(updateObj.truck_no, @"\s", "").ToUpper();
                    db.truck_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                   update = 1;
                }
            }
           
            Dispose(true);
            return update;

        }

        /// <summary>
        /// This Method Used for delete Truck (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteTruck(truck_master obj)
        {
            int isupdate = 0;
            truck_master updateObj = db.truck_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.truck_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Truck details by using Truck id
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<truck_master> GetTruckByID(int id)
        {
            truck_master updateObj = db.truck_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Truck list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<truck_master>> GetTruckList(int? statusid)
        {
            List<truck_master> objlist = db.truck_master.ToList();
            if (statusid>0)
            {
                objlist = objlist.Where(t => t.status == statusid).ToList();
            }
            return objlist;
        }

        /// <summary>
        /// This Method used to get userType list for view model
        ///  coder:Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<TruckModule>> GetViewTruckTypeList(string sort, int userid, int usertypeid)
        {
            List<TruckModule> objlist = new List<TruckModule>();

            List<truck_master> list = db.truck_master.ToList();
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
                    TruckModule obj = new TruckModule();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.truck_no = item.truck_no;
                    obj.GPS_id = item.GPS_id;
                    obj.GPS = item.GPS_master.GPS_id;
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
