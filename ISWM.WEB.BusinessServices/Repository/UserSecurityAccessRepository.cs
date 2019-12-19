using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
   public class UserSecurityAccessRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add user security access details         
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

            return isadd;

        }

        /// <summary>
        /// This method used for update user security access details  details
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
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete user security access details  (only status we change)
        /// 
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
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get user security access details  by using their id
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
        /// </summary>
        /// <returns></returns>
        public List<user_security_access_details> GetUSADList()
        {
            List<user_security_access_details> objlist = db.user_security_access_details.ToList();
            return objlist;
        }

    }
}
