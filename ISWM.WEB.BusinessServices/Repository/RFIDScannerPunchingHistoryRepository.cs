using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
   public class RFIDScannerPunchingHistoryRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();
        /// <summary>
        /// This Method Used To Add  into RFID Scanner Punching Historyusing Auto Scheduler RFID Punching tracking API
        ///  /// coder: Dhananjay Powar
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> AddRFIDScannerPunchingData(RFID_scanner_history obj)
        {
            bool isadd = false;
            if (obj != null)
            {
                db.RFID_scanner_history.Add(obj);
                db.SaveChanges();
                isadd = true;
            }
            Dispose(true);
            return isadd;
        }

        /// <summary>
        /// This Method Used To Add list into RFID Scanner Punching using Auto Scheduler  RFID Punching tracking API
        ///  /// coder: Dhananjay Powar
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        public async Task<bool> AddRFIDScannerPunchingList(List<RFIDScannerPunchingModel> objList)
        {
            bool isadd = false;
            if (objList.Count > 0)
            {
                
                foreach (var item in objList)
                {
                    try
                    {
                        DateTime punchdatetime = Convert.ToDateTime(item.PunchingDateTime);
                        var findobj = db.RFID_scanner_history.Where(w => w.RFID_Scanner_Name == item.RFIDName && w.RFID_number == item.RfidNo && w.Punching_date_time == punchdatetime).FirstOrDefault();
                        if(findobj==null)
                        {
                            RFID_scanner_history obj = new RFID_scanner_history();
                            obj.RFID_Scanner_Name = item.RFIDName;
                            obj.RFID_number = item.RfidNo;
                            obj.Punching_date_time = Convert.ToDateTime(item.PunchingDateTime);
                            obj.created_datetime = DateTime.Now;
                            db.RFID_scanner_history.Add(obj);
                            db.SaveChanges();
                            isadd = true;
                        }
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
        /// This Method Used get  RFID Punching tracking History
        /// </summary>
        /// <returns></returns>
        public async Task<List<RFIDPunchingDetailsModel>> GetRFIDPunchingtrackingList(string sort)
        {
            List<RFIDPunchingDetailsModel> objlist = new List<RFIDPunchingDetailsModel>();

            List<RFID_scanner_history> list = db.RFID_scanner_history.ToList();
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.created_datetime).ToList();
                }
                int i = 1;
                foreach (var item in list)
                {
                    RFIDPunchingDetailsModel obj = new RFIDPunchingDetailsModel();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.RFIDName = item.RFID_Scanner_Name;
                    obj.RfidNo = item.RFID_number;
                    obj.PunchingDateTime = Convert.ToDateTime(item.Punching_date_time).ToString("dd/MM/yyy hh:mm:ss");              
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
