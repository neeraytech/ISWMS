using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
   public class ModuleRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add module         
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Addmodule(module_master obj)
        {
            int isadd = 0;
            module_master updateObj = db.module_master.Where(w => w.module_name.ToLower() == obj.module_name.ToLower()).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.module_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }

            return isadd;

        }

        /// <summary>
        /// This method used for update ward details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Modifymodule(module_master obj)
        {
            bool isupdate = false;
            module_master updateObj = db.module_master.Find(obj.module_id);
            if (updateObj != null)
            {
                updateObj.module_name = obj.module_name;
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.module_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete module (only status we change)
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Deletemodule(module_master obj)
        {
            bool isupdate = false;
            module_master updateObj = db.module_master.Find(obj.module_id);
            if (updateObj != null)
            {
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.module_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get module details by using module id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public module_master GetmoduleByID(int module_id)
        {
            module_master updateObj = db.module_master.Find(module_id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get module list
        /// </summary>
        /// <returns></returns>
        public List<module_master> GetmoduleList()
        {
            List<module_master> objlist = db.module_master.ToList();
            return objlist;
        }

    }
}
