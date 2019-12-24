using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.CommonCode;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISWM.WEB.BusinessServices;
using ISWM.WEB.BusinessServices.SingletonCS;

namespace ISWM.WEB.Controllers
{
    /// <summary>
    /// This Controller is used to Create, Edit and show list for User Type module
    /// Coder: Smruti Wagh
    /// </summary>
    public class UserTypeModuleController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(userTypeModuleRepository));
        UserTypeRepository ur = new UserTypeRepository();
        userTypeModuleRepository utm = new userTypeModuleRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        /// <summary>
        /// This method used to show list of User Type Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        // GET: UserTypeModule
        public ActionResult Index()
        {
            try
            {
                var list = utm.GetuserType_modulesList();
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
        ///  This method used to create User Type Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        // GET: UserTypeModule/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.PageHeader = "Add Ward";
                //for module dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetModuleDDL();
                ViewBag.DDLModule = list;

                //for userType dropdown              
                list = cm.GetUserTypeDDL();
                ViewBag.DDLUserType = list;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                // throw;
            }

            return View();
        }
        /// <summary>
        ///  This method used to save created value of User Type Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: UserTypeModule/Create
        [HttpPost]
        public ActionResult Create(userType_modules obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = utm.AdduserType_modules(obj);
                if (isadd == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetModuleDDL();
                    ViewBag.DDLModule = list;

                    //for userType dropdown              
                    list = cm.GetUserTypeDDL();
                    ViewBag.DDLUserType = list;
                    return View();
                }
                
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetModuleDDL();
                ViewBag.DDLModule = list;

                //for userType dropdown              
                list = cm.GetUserTypeDDL();
                ViewBag.DDLUserType = list;
                return View();
            }
        }
        /// <summary>
        ///  This method used to edit User Type Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserTypeModule/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //for module dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetModuleDDL();
                ViewBag.DDLModule = list;

                //for module dropdown              
                list = cm.GetUserTypeDDL();
                ViewBag.DDLUserType = list;

                var obj = utm.GetuserType_modulesByID(id);
               
                return View(obj);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                //for module dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetModuleDDL();
                ViewBag.DDLModule = list;

                //for module dropdown              
                list = cm.GetUserTypeDDL();
                ViewBag.DDLUserType = list;
                return View();
            }
            
        }
        /// <summary>
        ///  This method used Save edited value of User Type Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: UserTypeModule/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, userType_modules obj)
        {
            try
            {
                // TODO: Add update logic here
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = utm.ModifyuserType_modules(obj);
                if (isUpdate )
                {
                    return RedirectToAction("Index");
                   
                }
                else
                {
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetModuleDDL();
                    ViewBag.DDLModule = list;

                    //for module dropdown              
                    list = cm.GetUserTypeDDL();
                    ViewBag.DDLUserType = list;
                    return View(obj);
                }
                
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetModuleDDL();
                ViewBag.DDLModule = list;

                //for module dropdown              
                list = cm.GetUserTypeDDL();
                ViewBag.DDLUserType = list;
                return View(obj);
            }
        }
        /// <summary>
        ///  This method used to delete User Type Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserTypeModule/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                userType_modules obj = new userType_modules();
                obj.id = id;
                obj.isActivie = false;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = utm.DeleteUserTypeModules(obj);
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
