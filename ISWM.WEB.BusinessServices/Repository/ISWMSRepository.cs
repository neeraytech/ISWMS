using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class ISWMSRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add ISWMS
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddISWMS(ISWMS_master obj)
        {
            int isadd = 0;
            bool allowToadd = false;
            List<ISWMS_master> updateObj = db.ISWMS_master.Where(w => w.route_id == obj.route_id && w.driver_id == obj.driver_id && w.truck_id == obj.truck_id && w.scanner_id == obj.scanner_id && w.date == obj.date).ToList();
            if(updateObj.Count>0)
            {
                int isfind = 0;
                foreach (var item in updateObj)
                {
                    if(obj.expected_end_time <= item.expected_start_time || obj.expected_start_time>= item.expected_end_time){ }
                    else{
                        isfind++;
                    }
                }
                if(isfind==0)
                {
                    allowToadd = true;
                }

            }
            else
            {
                allowToadd = true;
            }

            if (allowToadd)
            {
                db.ISWMS_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            else
            {              
                isadd = -1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update ISWMS details
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyISWMS(ISWMS_master obj)
        {
            bool isupdate = false;
            int isadd = 0;
            bool allowToadd = false;
            List<ISWMS_master> list = db.ISWMS_master.Where(w => w.route_id == obj.route_id && w.driver_id == obj.driver_id && w.truck_id == obj.truck_id && w.scanner_id == obj.scanner_id && w.date == obj.date).ToList();
            if (list.Count > 0)
            {
                int isfind = 0;
                foreach (var item in list)
                {
                    if (obj.expected_end_time <= item.expected_start_time || obj.expected_start_time >= item.expected_end_time) { }
                    else
                    {
                        isfind++;
                        if(item.id==obj.id)
                        {
                            isupdate = true;
                        }
                    }
                }

                if (isfind == 0)
                {
                    allowToadd = true;
                }
                else
                {
                    if(isupdate)
                    {
                        allowToadd = true;
                    }
                }

            }
            else
            {
                allowToadd = true;
            }


            if (allowToadd)
            {
                ISWMS_master updateObj = db.ISWMS_master.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.route_id = obj.route_id;
                    updateObj.driver_id = obj.driver_id;
                    updateObj.truck_id = obj.truck_id;
                    updateObj.scanner_id = obj.scanner_id;
                    updateObj.date = obj.date;
                    updateObj.expected_start_time = obj.expected_start_time;
                    updateObj.expected_end_time = obj.expected_end_time;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.ISWMS_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete ISWMS (only status we change)
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteISWMS(ISWMS_master obj)
        {
            int isupdate = 0;
            ISWMS_master updateObj = db.ISWMS_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.ISWMS_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get ISWMS details by using ISWMS id
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ISWMS_master> GetISWMSByID(int id)
        {
            ISWMS_master updateObj = db.ISWMS_master.Find(id);
           
            return updateObj;

        }

        /// <summary>
        /// This Method used to get ISWMS list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<ISWMS_master>> GetISWMSList()
        {
            List<ISWMS_master> objlist = db.ISWMS_master.ToList();
            return objlist;
        }


        /// <summary>
        /// This Method used to get View ward list
        /// Coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<ISWMSModel>> GetViewISWMSList(string sort, int userid, int usertypeid)
        {

            List<ISWMSModel> objlist = new List<ISWMSModel>();
            List<ISWMS_master> list = db.ISWMS_master.ToList();
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
                    ISWMSModel obj = new ISWMSModel();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.route_id = item.route_id;
                    obj.truck_id = item.truck_id;
                    obj.driver_id = item.driver_id;
                    obj.scanner_id = item.scanner_id;
                    obj.route = item.route_master.route_name;
                    obj.truck = item.truck_master.truck_no;
                    obj.driver = item.driver_master.name;
                    obj.contact_no = item.driver_master.contact_no;
                    obj.scanner = item.RFID_scanner_master.scanner_id;
                    obj.date = item.date.ToShortDateString();
                    obj.expected_start_time = item.expected_start_time.ToString();
                    obj.expected_end_time = item.expected_end_time.ToString();
                    obj.status_id = item.status;
                    obj = gcm.GetStatusDetails(obj);
                    objlist.Add(obj);
                    i++;
                }
            }

            return objlist;

        }
        /// <summary>
        /// To Dispose allocated memory
        /// coder:Pranali Patil
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
