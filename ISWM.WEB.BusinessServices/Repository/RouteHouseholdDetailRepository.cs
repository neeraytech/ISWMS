using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class RouteHouseholdDetailRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Route Household Details 
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddRouteHouseholdDetails(route_household_details obj)
        {
            int isadd = 0;
            route_household_details updateObj = db.route_household_details.Where(w => w.household_id==obj.household_id).FirstOrDefault();
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
        public async Task<int> ModifyRouteHouseholdDetails(route_household_details obj)
        {
            bool isupdate = false;
            int update = 0;
            route_household_details FindObj = db.route_household_details.Where(w =>  w.household_id == obj.household_id).FirstOrDefault();
            if (FindObj != null)
            {
                if (FindObj.id == obj.id)
                {
                    //Same Route Household so allow to update
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
                //This is different Route Household so allow to update
                isupdate = true;
            }

            if(isupdate)
            {
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
                   update = 1;
                }
                Dispose(true);
            }

           
            return update;

        }

        /// <summary>
        /// This Method Used for delete Route Household Details (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteRouteHouseholdDetails(route_household_details obj)
        {
            int isupdate = 0;
            route_household_details updateObj = db.route_household_details.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.route_household_details.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
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
        public async Task<route_household_details> GetRouteHouseholdDetailsByID(int id)
        {
            route_household_details updateObj = db.route_household_details.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Route Household Details list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<route_household_details>> GetRouteHouseholdDetailsList()
        {
            List<route_household_details> objlist = db.route_household_details.ToList();
            return objlist;
        }

        /// <summary>
        /// This Method used to get userType list for view model
        ///  coder:Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<RouteHouseHoldDModule>> GetViewRouteHouseholdDTypeList(string sort, int userid, int usertypeid)
        {
            List<RouteHouseHoldDModule> objlist = new List<RouteHouseHoldDModule>();

            List<route_household_details> list = db.route_household_details.ToList();
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
                    RouteHouseHoldDModule obj = new RouteHouseHoldDModule();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.route_id = item.route_id;
                    obj.route = item.route_master.route_name;
                    obj.household_id = item.household_id;
                    obj.household = item.household_master.household_name;
                    obj.lat = item.household_master.latitude;
                    obj.lg = item.household_master.longitude;
                    obj.area = item.household_master.area_master.area_name;
                    obj.ward = item.household_master.ward_master.ward_number;
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
