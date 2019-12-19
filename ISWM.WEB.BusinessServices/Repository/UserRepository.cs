using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class UserRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method Used to Add User in System with password SHA 2 Cryptography 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int AddUser(user_master obj)
        {
            int isAdd = 0;
            var isFind = db.user_master.Where(w => w.user_name.ToLower() == obj.user_name.ToLower()).FirstOrDefault();
            if(isFind==null)
            {
                db.user_master.Add(obj);
                db.SaveChanges();
                isAdd = 1;
            }
            else
            {
                isAdd = -1;
            }
                      
            return isAdd;
        }

        /// <summary>
        /// This Method Used to Modify User info
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ModifyUser(user_master obj)
        {
            bool isupdate = false;
            user_master updateObj = db.user_master.Find(obj.user_id);
            if(updateObj != null)
            {
                updateObj.name = obj.name;
                updateObj.user_type = obj.user_type;
                updateObj.isActive = obj.isActive;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.user_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }    
            return isupdate;
        }

        /// <summary>
        /// This method used to update user password
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ResetUserPassword(user_master obj)
        {
            bool isupdate = false;
            user_master updateObj = db.user_master.Find(obj.user_id);
            if (updateObj != null)
            {
                updateObj.password = obj.password;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.user_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;
        }

        /// <summary>
        /// This Method Used to Delete User (Soft Delete only change isActive status)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool DeleteUser(user_master obj)
        {
            bool isupdate = false;
            user_master updateObj = db.user_master.Find(obj.user_id);
            if (updateObj != null)
            {
                updateObj.isActive = obj.isActive;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.user_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            return isupdate;
        }

        /// <summary>
        /// This Method used  Get All User Types or Roles
        /// </summary>
        /// <returns></returns>
        public List<userType_master> GetUserType()
        {
            List<userType_master> objlist = new List<userType_master>();
            return objlist = db.userType_master.ToList();           
           

        }

        /// <summary>
        /// This Method Used Get All User List
        /// </summary>
        /// <returns></returns>
        public List<user_master> GetUserList(bool? isActive)
        {

            List<user_master> objlist = new List<user_master>();
            if(isActive==null)
            {
                objlist = db.user_master.Include(u => u.userType_master).ToList();
            }
            else
            {
                objlist = db.user_master.Include(u => u.userType_master).Where(w=>w.isActive==isActive).ToList();
            }
            return objlist;
           
        }

        /// <summary>
        /// This Method used to get user details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public user_master GetUserByID(int id)
        {
            user_master user_master = db.user_master.Find(id);           
            return user_master;
        }

        /// <summary>
        /// This Method Used to Verify login
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public user_master VerifyLogin(LoginModel obj)
        {
            user_master user_master = db.user_master.Where(w=>w.user_name.ToLower()==obj.user_name.ToLower() && w.password==obj.password).FirstOrDefault();
            return user_master;
        }


        /// <summary>
        /// This Method Used Get All User List by type
        /// </summary>
        /// <returns></returns>
        public List<user_master> GetUserListByType(int? UserTypeID)
        {

            List<user_master> objlist = new List<user_master>();
            if (UserTypeID == 0)
            {
                objlist = db.user_master.Include(u => u.userType_master).ToList();
            }
            else
            {
                objlist = db.user_master.Include(u => u.userType_master).Where(w => w.user_type == UserTypeID).ToList();
            }
            return objlist;

        }


        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            Dispose(disposing);
        }
    }
}
