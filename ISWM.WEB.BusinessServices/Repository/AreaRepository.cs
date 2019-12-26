using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class AreaRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Area         
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddArea(area_master obj)
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
        public bool ModifyArea(area_master obj)
        {
            bool isupdate = false;
            area_master updateObj = db.area_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.area_name = obj.area_name;               
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.area_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete Area (only status we change)
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteWard(area_master obj)
        {
            bool isupdate = false;
            area_master updateObj = db.area_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.area_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            //Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Area details by using ward id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public area_master GetAreaByID(int id)
        {
            area_master updateObj = db.area_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Area list
        /// </summary>
        /// <returns></returns>
        public List<area_master> GetWardList()
        {
            List<area_master> objlist = db.area_master.ToList();
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
