using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class RouteHouseholdDetailRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Route Household Details 
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddRouteHouseholdDetails(route_household_details obj)
        {
            int isadd = 0;
            route_household_details updateObj = db.route_household_details.Where(w => w.route_id == obj.route_id && w.household_id==obj.household_id).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.route_household_details.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update Route Household Details
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ModifyRouteHouseholdDetails(route_household_details obj)
        {
            bool isupdate = false;
            route_household_details updateObj = db.route_household_details.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.route_id = obj.route_id;
                updateObj.household_id = obj.household_id;
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.route_household_details.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete Route Household Details (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteRouteHouseholdDetails(route_household_details obj)
        {
            bool isupdate = false;
            route_household_details updateObj = db.route_household_details.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.route_household_details.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Route Household Details by using Route Household id
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public route_household_details GetRouteHouseholdDetailsByID(int id)
        {
            route_household_details updateObj = db.route_household_details.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Route Household Details list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public List<route_household_details> GetRouteHouseholdDetailsList()
        {
            List<route_household_details> objlist = db.route_household_details.ToList();
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
