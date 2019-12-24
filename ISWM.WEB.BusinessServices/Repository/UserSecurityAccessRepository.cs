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
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add user security access details   
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Adduser_security_access_details(user_security_access_details obj)
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
        public bool Modifyuser_security_access_details(user_security_access_details obj)
        {
            bool isupdate = false;
            user_security_access_details updateObj = db.user_security_access_details.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.user_id = obj.user_id;
                updateObj.module_id = obj.module_id;               
                updateObj.action_id = obj.action_id;
                updateObj.hasAccess = obj.hasAccess;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.user_security_access_details.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete user security access details  (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Deleteuser_security_access_details(user_security_access_details obj)
        {
            bool isupdate = false;
            user_security_access_details updateObj = db.user_security_access_details.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.hasAccess = obj.hasAccess;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.user_security_access_details.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
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
        public user_security_access_details GetUSADByID(int id)
        {
            user_security_access_details updateObj = db.user_security_access_details.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get user security access details list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public List<user_security_access_details> GetUSADList()
        {
            List<user_security_access_details> objlist = db.user_security_access_details.ToList();
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
