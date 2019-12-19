using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class RouteRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Route         
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddRoute(route_master obj)
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

            return isadd;

        }

        /// <summary>
        /// This method used for update Route details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ModifyRoute(route_master obj)
        {
            bool isupdate = false;
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
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete Route (only status we change)
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteRoute(route_master obj)
        {
            bool isupdate = false;
            route_master updateObj = db.route_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.route_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Route details by using Route id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public route_master GetRouteByID(int id)
        {
            route_master updateObj = db.route_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Route list
        /// </summary>
        /// <returns></returns>
        public List<route_master> GetRouteList()
        {
            List<route_master> objlist = db.route_master.ToList();
            return objlist;
        }

    }
}
