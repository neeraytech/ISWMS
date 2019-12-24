using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class RFIDScannerRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add RFIDScanner
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddRFIDScanner(RFID_scanner_master obj)
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
        public bool ModifyRFIDScanner(RFID_scanner_master obj)
        {
            bool isupdate = false;
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
                isupdate = true;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete RFIDScanner (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteRFIDScanner(RFID_scanner_master obj)
        {
            bool isupdate = false;
            RFID_scanner_master updateObj = db.RFID_scanner_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.RFID_scanner_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
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
        public RFID_scanner_master GetRFIDScannerByID(int id)
        {
            RFID_scanner_master updateObj = db.RFID_scanner_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get RFIDScanner list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public List<RFID_scanner_master> GetRFIDScannerList()
        {
            List<RFID_scanner_master> objlist = db.RFID_scanner_master.ToList();
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
