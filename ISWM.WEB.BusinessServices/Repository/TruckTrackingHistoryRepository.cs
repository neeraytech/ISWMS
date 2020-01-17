using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class TruckTrackingHistoryRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();
        /// <summary>
        /// This Method Used To Add  into Truck racking History using Auto Scheduler Truck tracking API
        ///  /// coder: Dhananjay Powar
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> AddTruckTrackingHistory(truck_tracking_history obj)
        {
            bool isadd = false;
            if(obj!=null)
            {
                db.truck_tracking_history.Add(obj);
                db.SaveChanges();
                isadd = true;
            }
            Dispose(true);
            return isadd;
        }

        /// <summary>
        /// This Method Used To Add list into Truck racking History using Auto Scheduler Truck tracking API
        ///  /// coder: Dhananjay Powar
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public async Task<bool> AddTruckTrackingHistoryList(List<GPS_API_Module> objList)
        {
            bool isadd = false;
            if (objList.Count>0)
            {
                isadd = true;
                foreach (var item in objList)
                {
                    try
                    {
                        truck_tracking_history obj = new truck_tracking_history();
                        obj.truck_number = item.vehicleName;
                        obj.latitude = item.latitude;
                        obj.longitude = item.longitude;
                        obj.speed = item.speed;
                        obj.status = item.STATUS;
                        obj.packetdatetime = item.packetdatetime;
                        obj.Ignition = item.Ignition;
                        obj.created_datetime = DateTime.Now;
                        db.truck_tracking_history.Add(obj);
                        db.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        isadd = false;
                        // throw;
                    }
                    
                }               
               
            }
            Dispose(true);
            return isadd;
        }
        /// <summary>
        /// This Method Used get truck tracking history
        /// </summary>
        /// <returns></returns>
        public async Task<List<TruckTrackingHistoryModule>> GetTruckTrackingHistoryList(string sort)
        {
            List<TruckTrackingHistoryModule> objlist = new List<TruckTrackingHistoryModule>();
           
            List<truck_tracking_history> list = db.truck_tracking_history.ToList();
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.created_datetime).ToList();
                }
                int i = 1;
                foreach (var item in list)
                {
                    TruckTrackingHistoryModule obj = new TruckTrackingHistoryModule();
                    obj.srno = i;
                    obj.id = item.ID;
                    obj.truck_number = item.truck_number;
                    obj.speed = item.speed;
                    obj.packetdatetime =Convert.ToDateTime(item.packetdatetime).ToString("dd/MM/yyy hh:mm:ss");
                    obj.status = item.status;
                    if(obj.status.ToLower()=="active")
                    {
                        obj.color_class = "badge-success";
                    }
                    else
                    {
                        obj.color_class = "badge-danger";
                    }
                    obj.Ignition = item.Ignition;
                    obj.longitude = item.longitude;
                    obj.latitude = item.latitude;                    
                    objlist.Add(obj);
                    i++;
                }
            }
            Dispose(true);
            return objlist;
            
        }
        /// <summary>
        /// this method is used to deallocate used memory
        /// coder: Dhananjay Powar
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
