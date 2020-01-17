using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class RFIDScannerRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add RFIDScanner
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddRFIDScanner(RFID_scanner_master obj)
        {
            int isadd = 0;
            RFID_scanner_master updateObj = db.RFID_scanner_master.Where(w => w.scanner_id.ToLower() == obj.scanner_id.ToLower()).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.RFID_scanner_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update RFIDScanner details
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyRFIDScanner(RFID_scanner_master obj)
        {
            int update = 0;
            bool isupdate = false;

            RFID_scanner_master FindObj = db.RFID_scanner_master.Where(w => w.scanner_id.ToLower() == obj.scanner_id.ToLower()).FirstOrDefault();
            if (FindObj != null)
            {
                if (FindObj.id == obj.id)
                {
                    //Same RFID Scanner name so allow to update
                    isupdate = true;
                }
                else {
                    //already exists so we can't allow to update
                    update = -1;
                }
            }
            else
            {
                //This is different RFID Scanner name so allow to update
                isupdate = true;
            }

            if(isupdate)
            {
                RFID_scanner_master updateObj = db.RFID_scanner_master.Find(obj.id);
                if (updateObj != null)
                {

                    updateObj.scanner_id = obj.scanner_id;
                    updateObj.user_id = obj.user_id;
                    updateObj.password = obj.password;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.RFID_scanner_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    update = 1;
                }
            }

          
            Dispose(true);
            return update;

        }

        /// <summary>
        /// This Method Used for delete RFIDScanner (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteRFIDScanner(RFID_scanner_master obj)
        {
            int isupdate = 0;
            RFID_scanner_master updateObj = db.RFID_scanner_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.RFID_scanner_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate =  obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get RFIDScanner details by using RFIDScanner id
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RFID_scanner_master> GetRFIDScannerByID(int id)
        {
            RFID_scanner_master updateObj = db.RFID_scanner_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get RFIDScanner list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<RFID_scanner_master>> GetRFIDScannerList(int? statusid)
        {
            List<RFID_scanner_master> objlist = db.RFID_scanner_master.ToList();
            if (statusid > 0)
            {
                objlist = objlist.Where(s => s.status == statusid).ToList();
            }
            return objlist;
        }

        /// <summary>
        /// This Method used to get RFID Scanner list for view model
        ///  coder:Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<RFIDScannerModule>> GetViewRFIDScannerList(string sort, int userid, int usertypeid)
        {
            List<RFIDScannerModule> objlist = new List<RFIDScannerModule>();

            List<RFID_scanner_master> list = db.RFID_scanner_master.ToList();
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
                    RFIDScannerModule obj = new RFIDScannerModule();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.scanner_id = item.scanner_id;
                    obj.user_id = item.user_id;
                    obj.password = item.password;
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
