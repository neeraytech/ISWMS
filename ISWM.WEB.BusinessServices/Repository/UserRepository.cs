using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class UserRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();
        GCommon gcm = new GCommon();
        /// <summary>
        /// This Method Used to Add User in System with password SHA 2 Cryptography 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddUser(user_master obj)
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
        public async Task<int> ModifyUser(user_master obj)
        {
            bool isupdate = false;
            int isadd = 0;
            var findobj = db.user_master.Where(w => w.user_name.ToLower() == obj.user_name.ToLower()).FirstOrDefault();
            if (findobj != null)
            {
                if (findobj.user_id == obj.user_id)
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
                user_master updateObj = db.user_master.Find(obj.user_id);
                if (updateObj != null)
                {
                    updateObj.name = obj.name;
                    updateObj.user_type = obj.user_type;
                    updateObj.address1 = obj.address1;
                    updateObj.address2 = obj.address2;
                    updateObj.area_id = obj.area_id;
                    updateObj.email_id = obj.email_id;
                    updateObj.contact_no = obj.contact_no;
                    updateObj.city = obj.city;
                    updateObj.state = obj.state;
                    updateObj.pin_code = obj.pin_code;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.user_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }

           
            return isadd;
        }

        /// <summary>
        /// This method used to update user password
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> ResetUserPassword(user_master obj)
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
        public async Task<int> DeleteUser(user_master obj)
        {
            int isupdate = 0;
            user_master updateObj = db.user_master.Find(obj.user_id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.user_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;
        }

        /// <summary>
        /// This Method used  Get All User Types or Roles
        /// </summary>
        /// <returns></returns>
        public async Task<List<userType_master>> GetUserType(int? statusid)
        {
            List<userType_master> objlist = new List<userType_master>();
            if (statusid > 0)
            {
                objlist = objlist.Where(w => w.status == statusid).ToList();
            }
            return objlist = db.userType_master.ToList();           
           

        }

        /// <summary>
        /// This Method Used Get All User List
        /// </summary>
        /// <returns></returns>
        public async Task<List<user_master>> GetUserList(int? status)
        {

            
            if(status>0)
            {
                List<user_master> objlist = db.user_master.Include(u => u.userType_master).ToList(); //db.user_master.ToList();
                return objlist;
            }
            else
            {
                List<user_master> objlist = db.user_master.Include(u => u.userType_master).Where(w=>w.status==status).ToList();
                return objlist;
            }
           
           
        }

        /// <summary>
        /// This Method Used Get View All User List
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserModule>> GetViewUserList(string sort,int usertype,int userid)
        {
            List<UserModule> objlist = new List<UserModule>();

            List<user_master> list = db.user_master.ToList(); //db.user_master.ToList();
            if(usertype!=1)
            {
                list = list.Where(w => w.modified_by == userid).ToList();
            }
            if (list.Count > 0)
            {
                
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.modified_datetime).ToList();
                }


                int i = 1;
                foreach (var item in list)
                {
                    UserModule obj = new UserModule();
                    obj.srno = i;
                    obj.id = item.user_id;
                    obj.address1 = item.address1;
                    obj.address2 = item.address2;
                    obj.area_id = item.area_id;
                    obj.area = item.area_master.area_name;
                    obj.pin_code = item.pin_code+"";
                    obj.city = item.city;
                    obj.state = item.state;                   
                    obj.name = item.name;
                    obj.user_name = item.user_name;
                    obj.email = item.email_id;
                    obj.contact_no = item.contact_no;                    
                    obj.status_id = item.status;
                    obj = gcm.GetStatusDetails(obj);
                    objlist.Add(obj);
                    i++;
                }
            }

            return objlist;

        }


        /// <summary>
        /// This Method used to get user details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<user_master> GetUserByID(int id)
        {
            user_master updateObj = db.user_master.Find(id);
            if (id == 0)
            {
                updateObj = new user_master();
                updateObj.city = ConfigurationManager.AppSettings["City"];
                updateObj.state = ConfigurationManager.AppSettings["State"];
            }
            return updateObj;
        }

        /// <summary>
        /// This Method Used to Verify login
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<user_master> VerifyLogin(LoginModel obj)
        {
            user_master user_master = db.user_master.Where(w=>w.user_name.ToLower()==obj.user_name.ToLower() && w.password==obj.password).FirstOrDefault();
            return user_master;
        }


        /// <summary>
        /// This Method Used Get All User List by type
        /// </summary>
        /// <returns></returns>
        public async Task<List<user_master>> GetUserListByType(int? UserTypeID)
        {

            List<user_master> objlist = new List<user_master>();
            if (UserTypeID == 0)
            {
                objlist = db.user_master.ToList();
            }
            else
            {
                objlist = db.user_master.Where(w => w.user_type == UserTypeID).ToList();
            }
            return objlist;

        }


        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            
        }
    }
}
