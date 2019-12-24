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
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add User Type modules   
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AdduserType_modules(userType_modules obj)
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
        public bool ModifyuserType_modules(userType_modules obj)
        {
            bool isupdate = false;
            userType_modules updateObj = db.userType_modules.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.user_type_id = obj.user_type_id;
                updateObj.module_id = obj.module_id;
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.userType_modules.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete User Type Module (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteUserTypeModules(userType_modules obj)
        {
            bool isupdate = false;
            userType_modules updateObj = db.userType_modules.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.userType_modules.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
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
        public userType_modules GetuserType_modulesByID(int id)
        {
            userType_modules updateObj = db.userType_modules.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get User Type modules list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public List<userType_modules> GetuserType_modulesList()
        {
            List<userType_modules> objlist = db.userType_modules.ToList();
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
