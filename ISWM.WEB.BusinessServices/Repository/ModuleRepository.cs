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
    /// Repository is for Module Master
    /// coder: Smruti Wagh
    /// </summary>
    public class ModuleRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add module         
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Addmodule(module_master obj)
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
            Dispose(true);
            return isadd;
           
        }

        /// <summary>
        /// This method used for update module details
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Modifymodule(module_master obj)
        {
            bool isupdate = false;
            int isadd = 0;
            module_master findobj = db.module_master.Where(w => w.module_name.ToLower() == obj.module_name.ToLower()).FirstOrDefault();
            if (findobj != null)
            {
                if (findobj.module_id == obj.module_id)
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

            if(isupdate)
            {
                module_master updateObj = db.module_master.Find(obj.module_id);
                if (updateObj != null)
                {
                    updateObj.module_name = obj.module_name;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.module_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }

           
            Dispose(true);
            return isadd;
         
        }

        /// <summary>
        /// This Method Used for delete module (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Deletemodule(module_master obj)
        {
           int isupdate = 0;
            module_master updateObj = db.module_master.Find(obj.module_id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.module_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate =  obj.status;
            }
            Dispose(true);
            return isupdate;
           
        }

        /// <summary>
        /// This Method Used to get module details by using module id
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<module_master> GetmoduleByID(int module_id)
        {
            module_master updateObj = db.module_master.Find(module_id);
            return updateObj;
         
        }

        /// <summary>
        /// This Method used to get Module list
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<module_master>> GetModuleList(int? statusid)
        {
            List<module_master> objlist = db.module_master.ToList();
            if (statusid > 0)
            {
                objlist = objlist.Where(w => w.status == statusid).ToList();
            }
            return objlist;
        }

        /// <summary>
        /// This Method used to get module list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<Module_Module>> GetViewModuleTypeList(string sort)
        {
            List<Module_Module> objlist = new List<Module_Module>();

            List<module_master> list = db.module_master.ToList();
           
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.modified_datetime).ToList();
                }

                int i = 1;
                foreach (var item in list)
                {
                    Module_Module obj = new Module_Module();
                    obj.srno = i;
                    obj.id = item.module_id;
                    obj.module_name = item.module_name;
                    
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
