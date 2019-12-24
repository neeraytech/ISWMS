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
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add RFID household details      
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddRHD(RFID_household_details obj)
        {
            int isadd = 0;
            RFID_household_details updateObj = db.RFID_household_details.Where(w => w.RFID.ToLower() == obj.RFID.ToLower()).FirstOrDefault();
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
        public bool ModifyRHD(RFID_household_details obj)
        {
            bool isupdate = false;
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
                isupdate = true;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete RFID household details  (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteRHD(RFID_household_details obj)
        {
            bool isupdate = false;
            RFID_household_details updateObj = db.RFID_household_details.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.RFID_household_details.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
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
        public RFID_household_details GetRHDByID(int id)
        {
            RFID_household_details updateObj = db.RFID_household_details.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get RFID household details  list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public List<RFID_household_details> GetRHDList()
        {
            List<RFID_household_details> objlist = db.RFID_household_details.ToList();
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
