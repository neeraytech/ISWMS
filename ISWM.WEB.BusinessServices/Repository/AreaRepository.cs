using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class AreaRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Area         
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddArea(area_master obj)
        {
            int isadd = 0;
            area_master updateObj = db.area_master.Where(w => w.area_name.ToLower() == obj.area_name.ToLower()).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.area_master.Add(obj);
                db.SaveChanges();

                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update Area details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyArea(area_master obj)
        {
            bool isupdate = false;
            int isadd = 0;
            area_master findobj = db.area_master.Where(w => w.area_name.ToLower() == obj.area_name.ToLower()).FirstOrDefault();
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
                area_master updateObj = db.area_master.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.area_name = obj.area_name;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.area_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            } 
         
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete Area (only status we change)
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteArea(area_master obj)
        {
            int isupdate = 0;
            area_master updateObj = db.area_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.area_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Area details by using ward id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<area_master> GetAreaByID(int id)
        {
            area_master updateObj = db.area_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Area list
        /// </summary>
        /// <returns></returns>
        public async Task<List<area_master>> GetAreaList(int? statusid)
        {
            List<area_master> objlist = db.area_master.ToList();
            if (statusid > 0)
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
        public async Task<List<AreaModule>> GetViewAreaList(string sort)
        {
            List<AreaModule> objlist = new List<AreaModule>();
            List<area_master> list = db.area_master.ToList();
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.modified_datetime).ToList();
                }

                int i = 1;
                foreach (var item in list)
                {
                    AreaModule obj = new AreaModule();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.area_name = item.area_name;                    
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
