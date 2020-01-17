using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
   public class WardRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Ward
        /// Coder:Dhananjay Powar
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddWard(ward_master obj)
        {
            int isadd = 0;
            ward_master updateObj = db.ward_master.Where(w => w.ward_number.ToLower() == obj.ward_number.ToLower()).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                obj.ward_number.ToUpper();
                db.ward_master.Add(obj);
                db.SaveChanges();

                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update ward details
        /// Coder:Dhananjay Powar
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyWard(ward_master obj)
        {
            bool isupdate = false;
            int isadd = 0;
            ward_master findobj = db.ward_master.Where(w => w.ward_number.ToLower() == obj.ward_number.ToLower()).FirstOrDefault();
            if (findobj != null)
            {
                if (findobj.id == obj.id)
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
            if (isupdate)
            {
                ward_master updateObj = db.ward_master.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.ward_number = obj.ward_number;
                    updateObj.ward_description = obj.ward_description;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    updateObj.ward_number.ToUpper();
                    db.ward_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }
            db.Dispose();
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete ward (only status we change)
        /// Coder:Dhananjay Powar
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteWard(ward_master obj)
        {
            int isupdate = 0;
            ward_master updateObj = db.ward_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.ward_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            //Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get ward details by using ward id
        /// Coder:Dhananjay Powar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ward_master> GetWardByID(int  id)
        {
            ward_master updateObj = db.ward_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get ward list
        /// Coder:Dhananjay Powar
        /// </summary>
        /// <returns></returns>
        public async Task<List<ward_master>> GetWardList(int? statusid)
        {
            List<ward_master> objlist = db.ward_master.ToList();
            if (statusid>0)
            {
                objlist = objlist.Where(w => w.status == statusid).ToList();
            }
            
            return objlist;
        }

        /// <summary>
        /// This Method used to get View ward list
        /// Coder:Dhananjay Powar
        /// </summary>
        /// <returns></returns>
        public async Task<List<WardModule>> GetViewWardList(string sort, int userid,int usertypeid)
        {
            
            List<WardModule> objlist  = new List<WardModule>();
            List<ward_master> list = db.ward_master.ToList();
            if(usertypeid!=1)
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
                    WardModule obj = new WardModule();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.ward_number = item.ward_number;
                    obj.ward_description = item.ward_description;
                    obj.status_id = item.status;
                    obj = gcm.GetStatusDetails(obj);
                    objlist.Add(obj);
                    i++;
                }
            }

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
