using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class RouteRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Route  
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddRoute(route_master obj)
        {
            int isadd = 0;
            route_master updateObj = db.route_master.Where(w => w.route_name.ToLower() == obj.route_name.ToLower()).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.route_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update Route details
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyRoute(route_master obj)
        {
            int update = 0;
            bool isupdate = false;

            route_master FindObj = db.route_master.Where(w => w.route_name.ToLower() == obj.route_name.ToLower()).FirstOrDefault();
            if (FindObj != null)
            {
                if (FindObj.id == obj.id)
                {
                    //Same Route name so allow to update
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
                //This is different Route name so allow to update
                isupdate = true;
            }

            if(isupdate)
            {
                route_master updateObj = db.route_master.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.route_name = obj.route_name;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.route_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    update = 1;
                }
            }           
            Dispose(true);
            return update;
        }

        /// <summary>
        /// This Method Used for delete Route (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteRoute(route_master obj)
        {
            int isupdate = 0;
            route_master updateObj = db.route_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.route_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Route details by using Route id
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<route_master> GetRouteByID(int id)
        {
            route_master updateObj = db.route_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Route list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<route_master>> GetRouteList(int? statusid)
        {
            List<route_master> objlist = db.route_master.ToList();
            if (statusid > 0)
            {
                objlist = objlist.Where(w => w.status == statusid).ToList();
            }
            return objlist;
        }

        /// <summary>
        /// This Method used to get Route list for view model
        ///  coder:Pranali Patil
        /// </summary>
        ///<returns></returns>
        public async Task<List<RouteModule>> GetViewRouteTypeList(string sort, int userid, int usertypeid)
        {
            List<RouteModule> objlist = new List<RouteModule>();

            List<route_master> list = db.route_master.ToList();
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
                    RouteModule obj = new RouteModule();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.route_name = item.route_name;
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
