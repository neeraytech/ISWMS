using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
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
    public class  ActionRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Action  
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddActions_master(actions_master obj)
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
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update Action details
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Modifyactions_master(actions_master obj)
        {
            bool isupdate = false;
            int isadd = 0;
            actions_master findobj = db.actions_master.Where(w => w.module_action_name == obj.module_action_name).FirstOrDefault();
            if (findobj != null)
            {
               if(findobj.module_action_id==obj.module_action_id)
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
                actions_master updateObj = db.actions_master.Find(obj.module_action_id);
                if (updateObj != null)
                {
                    updateObj.module_action_name = obj.module_action_name;
                    updateObj.module_action_desc = obj.module_action_desc;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.actions_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }
            
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete Action (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteAction(actions_master obj)
        {
            int isupdate = 0;
            actions_master updateObj = db.actions_master.Find(obj.module_action_id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.actions_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get Action details by using Action id
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="module_action_id"></param>
        /// <returns></returns>
        public async Task<actions_master> GetActionByID(int module_action_id)
        {
            actions_master updateObj = db.actions_master.Find(module_action_id);
            return updateObj;

        }


        /// <summary>
        /// This Method used to get Action list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<actions_master>> GetActionList(int? statusid)
        {
            List<actions_master> objlist = db.actions_master.ToList();
            if (statusid > 0)
            {
                objlist = objlist.Where(w => w.status == statusid).ToList();
            }
            return objlist;
        }

        /// <summary>
        /// This Method used to get View Action list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<ActionModule>> GetViewActionList(string sort)
        {
            List<ActionModule> objlist = new List<ActionModule>();

            List<actions_master> list = db.actions_master.ToList();
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.modified_datetime).ToList();
                }
                int i = 1;
                foreach (var item in list)
                {
                    ActionModule obj = new ActionModule();
                    obj.srno = i;
                    obj.id = item.module_action_id;
                    obj.module_action_name = item.module_action_name;
                    obj.module_action_desc = item.module_action_desc;
                    obj.status_id = item.status;
                    obj = gcm.GetStatusDetails(obj);
                    objlist.Add(obj);
                    i++;
                }
            }

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
