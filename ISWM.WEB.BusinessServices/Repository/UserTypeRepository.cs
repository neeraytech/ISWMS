using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class UserTypeRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();
        /// <summary>
        /// This Method used to add User type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddUserType(userType_master obj)
        {
            int isadd = 0;
            userType_master updateObj = db.userType_master.Where(w => w.user_type.ToLower() == obj.user_type.ToLower()).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.userType_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }

            return isadd;

        }

        /// <summary>
        /// This Method use for update user type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ModifyUserType(userType_master obj)
        {
            bool isupdate = false;
            userType_master updateObj = db.userType_master.Find(obj.user_type_id);
            if (updateObj != null)
            {
                updateObj.user_type = obj.user_type;
                updateObj.user_type_desc = obj.user_type_desc;
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.userType_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }


        /// <summary>
        /// This Method Used for delete UserType (only status we change)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteUserType(userType_master obj) 
        {
            bool isupdate = false;
            userType_master updateObj = db.userType_master.Find(obj.user_type_id);
            if (updateObj != null)
            {
                updateObj.isActivie = obj.isActivie;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.userType_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get ward details by using userType id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public userType_master GetUserTypeByID(int user_type_id)
        { 
            userType_master updateObj = db.userType_master.Find(user_type_id);
            return updateObj;
        }


        /// <summary>
        /// This Method used to get userType list
        /// </summary>
        /// <returns></returns>
        public List<userType_master> GetuserTypeList()
        {
            List<userType_master> objlist = db.userType_master.ToList();
            return objlist;
        }

    }
}
