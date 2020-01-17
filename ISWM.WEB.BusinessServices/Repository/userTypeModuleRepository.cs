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
    /// Repository is for User Type Module
    /// coder: Smruti Wagh
    /// </summary>
    public class userTypeModuleRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add User Type modules   
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AdduserType_modules(userType_modules obj)
        {
            int isadd = 0;
            userType_modules updateObj = db.userType_modules.Where(w => w.user_type_id == obj.user_type_id && w.module_id == obj.module_id).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.userType_modules.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            db.Dispose();
            return isadd;

        }

        /// <summary>
        /// This method used for update User Type modules details
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyuserType_modules(userType_modules obj)
        {
            bool isupdate = false;

            int isadd = 0;
            userType_modules FindObj = db.userType_modules.Where(w => w.user_type_id == obj.user_type_id && w.module_id == obj.module_id).FirstOrDefault();
            if (FindObj != null)
            {
                if (FindObj.id == obj.id)
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
                userType_modules updateObj = db.userType_modules.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.user_type_id = obj.user_type_id;
                    updateObj.module_id = obj.module_id;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.userType_modules.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }

           
            db.Dispose();
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete User Type Module (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserTypeModules(userType_modules obj)
        {
            int isupdate = 0;
            userType_modules updateObj = db.userType_modules.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.userType_modules.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get User Type modules details by using id
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<userType_modules> GetuserType_modulesByID(int id)
        {
            userType_modules updateObj = db.userType_modules.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get User Type modules list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<userType_modules>> GetuserType_modulesList()
        {
            List<userType_modules> objlist = db.userType_modules.ToList();
            return objlist;
        }
        /// <summary>
        /// This Method used to get View user type module list
        /// Coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserTypeModuleModel>> GetViewUserTypeModuleList(string sort)
        {

            List<UserTypeModuleModel> objlist = new List<UserTypeModuleModel>();
            List<userType_modules> list = db.userType_modules.ToList();
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.modified_datetime).ToList();
                }

                int i = 1;
                foreach (var item in list)
                {
                    UserTypeModuleModel obj = new UserTypeModuleModel();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.user_type_id = item.user_type_id;
                    obj.module_id = item.module_id;
                    obj.module = item.module_master.module_name;
                    obj.user_type = item.userType_master.user_type;
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
