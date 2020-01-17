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
    /// Repository is for User Type
    /// coder: Smruti Wagh
    /// </summary>
    public class UserTypeRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();
        /// <summary>
        /// This Method used to add User type
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddUserType(userType_master obj)
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
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This Method use for update user type
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyUserType(userType_master obj)
        {
            bool isupdate = false;
            int isadd = 0;
            userType_master findobj = db.userType_master.Where(w => w.user_type.ToLower() == obj.user_type.ToLower()).FirstOrDefault();
            if (findobj != null)
            {
                if (findobj.user_type_id == obj.user_type_id)
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
                userType_master updateObj = db.userType_master.Find(obj.user_type_id);
                if (updateObj != null)
                {
                    updateObj.user_type = obj.user_type;
                    updateObj.user_type_desc = obj.user_type_desc;
                    //updateObj.isActivie = obj.isActivie;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.userType_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }
           
            Dispose(true);
            return isadd;

        }


        /// <summary>
        /// This Method Used for delete UserType (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteUserType(userType_master obj) 
        {
            int isupdate = 0;
            userType_master updateObj = db.userType_master.Find(obj.user_type_id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.userType_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                isupdate = obj.status;
            }
            Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get User Type details by using userType id 
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<userType_master> GetUserTypeByID(int user_type_id)
        { 
            userType_master updateObj = db.userType_master.Find(user_type_id);
            return updateObj;
        }


        /// <summary>
        /// This Method used to get userType list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task <List<userType_master>> GetuserTypeList()
        {
            List<userType_master> objlist = db.userType_master.ToList();
            return objlist;
        }

        /// <summary>
        /// This Method used to get userType list for view model
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserTypeModule>> GetViewUserTypeList(string sort)
        {
            List<UserTypeModule> objlist = new List<UserTypeModule>();

            List<userType_master> list = db.userType_master.ToList();          
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.modified_datetime).ToList();
                }

                int i = 1;
                foreach (var item in list)
                {
                    UserTypeModule obj = new UserTypeModule();
                    obj.srno = i;
                    obj.id = item.user_type_id;
                    obj.user_type = item.user_type;
                    obj.user_description = item.user_type_desc;
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
