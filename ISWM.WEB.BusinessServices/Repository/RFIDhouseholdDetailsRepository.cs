using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    /// <summary>
    /// Repository is for RFID Household Details
    /// coder: Smruti Wagh
    /// </summary>
    public class RFIDhouseholdDetailsRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add RFID household details      
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddRHD(RFID_household_details obj)
        {
            int isadd = 0;
            RFID_household_details updateObj = db.RFID_household_details.Where(w => w.RFID.ToLower() == obj.RFID.ToLower() || w.household_id==obj.household_id).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.RFID_household_details.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            db.Dispose();
            return isadd;

        }

        /// <summary>
        /// This method used for update RFID household details 
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyRHD(RFID_household_details obj)
        {
            bool isupdate = false;
            int isadd = 0;
            RFID_household_details FindObj = db.RFID_household_details.Where(w => w.RFID.ToLower() == obj.RFID.ToLower() || w.household_id == obj.household_id).FirstOrDefault();

            if (FindObj != null)
            {
                if (FindObj.id == obj.id)
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
                RFID_household_details updateObj = db.RFID_household_details.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.RFID = obj.RFID;
                    updateObj.RFID_description = obj.RFID_description;
                    updateObj.household_id = obj.household_id;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.RFID_household_details.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }

           
            db.Dispose();
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete RFID household details  (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteRHD(RFID_household_details obj)
        {
            int isupdate = 0;
            RFID_household_details updateObj = db.RFID_household_details.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.RFID_household_details.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate =  obj.status;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get RFID household details  details by using RFID id
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RFID_household_details> GetRHDByID(int id)
        {
            RFID_household_details updateObj = db.RFID_household_details.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get RFID household details  list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<RFID_household_details>> GetRHDList()
        {
            List<RFID_household_details> objlist = db.RFID_household_details.ToList();
            return objlist;
        }
        /// <summary>
        /// This Method used to get View RFID Household details list
        /// Coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<RFIDHouseholdModel>> GetViewRFIDHouseholdList(string sort, int userid, int usertypeid)
        {

            List<RFIDHouseholdModel> objlist = new List<RFIDHouseholdModel>();
            List<RFID_household_details> list = db.RFID_household_details.ToList();
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
                    RFIDHouseholdModel obj = new RFIDHouseholdModel();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.RFID = item.RFID;
                    obj.RFID_description = item.RFID_description;
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
