using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
   public class UserSecurityAccessRepository
    {
        /// <summary>
        /// this repository is used to add, update, delete, listById and list for User Security Access
        ///  coder:Smruti Wagh
        /// </summary>
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add user security access details   
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddUserSecAc(user_security_access_details obj)
        {
            int isadd = 0;
            user_security_access_details updateObj = db.user_security_access_details.Where(w => w.user_id == obj.user_id && w.module_id == obj.module_id && w.action_id == obj.action_id).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.user_security_access_details.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            db.Dispose();
            return isadd;

        }

        /// <summary>
        /// This method used for update user security access details  details
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyUserSecAc(user_security_access_details obj)
        {
            bool isupdate = false;
            int isadd = 0;
            user_security_access_details findobj = db.user_security_access_details.Where(w => w.user_id == obj.user_id && w.module_id == obj.module_id && w.action_id == obj.action_id).FirstOrDefault();
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

            if(isupdate)
            {
                user_security_access_details updateObj = db.user_security_access_details.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.user_id = obj.user_id;
                    updateObj.module_id = obj.module_id;
                    updateObj.action_id = obj.action_id;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.user_security_access_details.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }

           
            db.Dispose();
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete user security access details  (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserSecAc(user_security_access_details obj)
        {
            int isupdate = 0;
            user_security_access_details updateObj = db.user_security_access_details.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.user_security_access_details.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get user security access details  by using their id
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<user_security_access_details> GetUSADByID(int id)
        {
            user_security_access_details updateObj = db.user_security_access_details.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get user security access details list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<user_security_access_details>> GetUSADList()
        {
            List<user_security_access_details> objlist = db.user_security_access_details.ToList();
            return objlist;
        }

        /// <summary>
        /// This Method used to get driver list for view model
        ///  coder:pranali Patil
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserSecurityAccessModel>> GetViewUserSecurityList(string sort)
        {
            List<UserSecurityAccessModel> objlist = new List<UserSecurityAccessModel>();

            List<user_security_access_details> list = db.user_security_access_details.ToList();
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.modified_datetime).ToList();
                }

                int i = 1;
                foreach (var item in list)
                {
                    UserSecurityAccessModel obj = new UserSecurityAccessModel();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.user_id = item.user_id;
                    obj.username = item.user_master.user_name;
                    obj.name = item.user_master.name;
                    obj.contact_no = item.user_master.contact_no;
                    obj.module_id = item.module_id;
                    obj.module_name = item.module_master.module_name;
                    obj.action_id = item.action_id;
                    obj.action_name = item.actions_master.module_action_name;
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
