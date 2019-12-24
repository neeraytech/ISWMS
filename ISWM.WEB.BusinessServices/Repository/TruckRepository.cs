using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class TruckRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Truck
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddTruck(truck_master obj)
        {
            int isadd = 0;
            truck_master updateObj = db.truck_master.Where(w => w.truck_no.ToLower() == obj.truck_no.ToLower() || w.GPS_id==obj.GPS_id).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
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
        public bool ModifyTruck(truck_master obj)
        {
            bool isupdate = false;
            truck_master updateObj = db.truck_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.truck_no = obj.truck_no;
                updateObj.GPS_id = obj.GPS_id;
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.truck_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete Truck (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteTruck(truck_master obj)
        {
            bool isupdate = false;
            truck_master updateObj = db.truck_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.truck_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
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
        public truck_master GetTruckByID(int id)
        {
            truck_master updateObj = db.truck_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Truck list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public List<truck_master> GetTruckList()
        {
            List<truck_master> objlist = db.truck_master.ToList();
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
