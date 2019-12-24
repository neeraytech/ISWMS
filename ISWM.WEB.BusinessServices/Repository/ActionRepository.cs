using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    /// <summary>
    /// Repository is for Action Master
    /// coder: Smruti Wagh
    /// </summary>
    public class ActionRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Action  
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddActions_master(actions_master obj)
        {
            int isadd = 0;
            actions_master updateObj = db.actions_master.Where(w => w.module_action_name == obj.module_action_name).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.actions_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            db.Dispose();
            return isadd;

        }

        /// <summary>
        /// This method used for update Action details
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Modifyactions_master(actions_master obj)
        {
            bool isupdate = false;
            actions_master updateObj = db.actions_master.Find(obj.module_action_id);
            if (updateObj != null)
            {
                updateObj.module_action_name = obj.module_action_name;
                updateObj.module_action_desc = obj.module_action_desc;
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.actions_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete Action (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteAction(actions_master obj)
        {
            bool isupdate = false;
            actions_master updateObj = db.actions_master.Find(obj.module_action_id);
            if (updateObj != null)
            {
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.actions_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Action details by using Action id
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="module_action_id"></param>
        /// <returns></returns>
        public actions_master GetActionByID(int module_action_id)
        {
            actions_master updateObj = db.actions_master.Find(module_action_id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get Action list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public List<actions_master> GetActionList()
        {
            List<actions_master> objlist = db.actions_master.ToList();
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
