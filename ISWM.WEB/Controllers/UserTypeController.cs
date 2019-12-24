using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.BusinessServices.SingletonCS;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.CommonCode;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISWM.WEB.BusinessServices;

namespace ISWM.WEB.Controllers
{/// <summary>
/// this controller is used for data type create, edit, index view
/// </summary>
    public class UserTypeController : Controller
    {
       
        ILog log = log4net.LogManager.GetLogger(typeof(UserTypeController));
        UserRepository ur = new UserRepository();
        UserTypeRepository ut = new UserTypeRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        /// <summary>
        /// This method use to show list  of User Type
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        // GET: UserType
        public ActionResult Index()
        {
            try
            {
                var list = ut.GetuserTypeList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                //  throw;
            }
           
        }


        /// <summary>
        /// This method used to create user type
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>

        // GET: UserType/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
            }
        }
        /// <summary>
        /// To create User Type
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: UserType/Create
        [HttpPost]
        public ActionResult Create(userType_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = ut.AddUserType(obj);
                if (isadd == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

             }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
            }
        }
        /// <summary>
        /// This method used to edit user type
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserType/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                
                var obj = ut.GetUserTypeByID(id);
                if(obj!=null)
                {
                    return View(obj);
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
            }
        }

        // POST: UserType/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, userType_master obj)
        {
            try
            {
                // TODO: Add update logic here
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = ut.ModifyUserType(obj);
                if (isUpdate)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(obj);
                }
                
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View(obj);
            }
        }
        /// <summary>
        /// This method used to delete user type
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserType/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                userType_master obj = new userType_master();
                obj.user_type_id = id;
                obj.isActivie = false;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = ut.DeleteUserType(obj);
                return RedirectToAction("Index");
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
           
        }

       
    }
}
