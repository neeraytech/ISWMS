using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    /// <summary>
    /// Repository is for Household Master
    /// coder: Smruti Wagh
    /// </summary>
    public class HouseholdRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();
        GCommon gcm = new GCommon();

        /// <summary>
        /// This Method used to add household   
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Addhousehold(household_master obj)
        {
            int isadd = 0;
            household_master updateObj = db.household_master.Where(w => w.latitude == obj.latitude && w.longitude == obj.longitude).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.household_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update household details
        ///  coder:Smruti Wagh
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Modifyhousehold(household_master obj)
        {
            bool isupdate = false;

            int isadd = 0;
            household_master FindObj = db.household_master.Where(w => w.latitude == obj.latitude && w.longitude == obj.longitude).FirstOrDefault();
            if (FindObj != null)
            {
                if(FindObj.id==obj.id)
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

            if(isupdate)
            {
                household_master updateObj = db.household_master.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.address1 = obj.address1;
                    updateObj.address2 = obj.address2;
                    updateObj.area_id = obj.area_id;
                    updateObj.pin_code = obj.pin_code;
                    updateObj.city = obj.city;
                    updateObj.state = obj.state;
                    updateObj.latitude = obj.latitude;
                    updateObj.longitude = obj.longitude;
                    updateObj.household_name = obj.household_name;
                    updateObj.email = obj.email;
                    updateObj.contact_no = obj.contact_no;
                    updateObj.ward_id = obj.ward_id;
                    updateObj.valid_from = obj.valid_from;
                    updateObj.valid_to = obj.valid_to;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.household_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }
    
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete household (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Deletehousehold(household_master obj)
        {
            int isupdate = 0;
            household_master updateObj = db.household_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.household_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get household details by using household id
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<household_master> GethouseholdByID(int id)
        {
            household_master updateObj = db.household_master.Find(id);
            if(id==0)
            {
                updateObj = new household_master();
                updateObj.city= ConfigurationManager.AppSettings["City"];
                updateObj.state = ConfigurationManager.AppSettings["State"];
            }
            return updateObj;

        }

        /// <summary>
        /// This Method used to get household list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<household_master>> GethouseholdList(int? statusid)
        {
            List<household_master> objlist = db.household_master.ToList();
            if (statusid > 0)
            {
                objlist = objlist.Where(w => w.status == statusid).ToList();
            }

            return objlist;
        }
        /// <summary>
        /// This Method Used to get view household details 
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<HouseholdModel>> GetViewhouseholdList(string sort, int userid, int usertypeid)
        {
            List<HouseholdModel> objlist = new List<HouseholdModel>();

            List<household_master> list = db.household_master.ToList();
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
                    HouseholdModel obj = new HouseholdModel();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.address1 = item.address1;
                    obj.address2 = item.address2;
                    obj.area_id = item.area_id;
                    obj.area = item.area_master.area_name;
                    obj.pin_code = item.pin_code;
                    obj.city = item.city;
                    obj.state = item.state;
                    obj.latitude = item.latitude;
                    obj.longitude = item.longitude;
                    obj.household_name = item.household_name;
                    obj.email = item.email;
                    obj.contact_no = item.contact_no;
                    obj.ward_id = item.ward_id;
                    obj.ward = item.ward_master.ward_number;
                    obj.status_id = item.status;
                    obj = gcm.GetStatusDetails(obj);
                    objlist.Add(obj);
                    i++;
                }
            }
            
            return objlist;
        }
        /// <summary>
        /// this method is used to deallocate used memory
        /// coder: Smruti Wagh
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
