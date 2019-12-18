using ISWM.WEB.BusinessServices;
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

namespace ISWM.WEB.Controllers
{
    public class UserController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(UserController));
        UserRepository ur = new UserRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: User
        public ActionResult Index()
        {
            try
            {
                ViewBag.PageHeader = "System User List";
                int userid = Singleton.userobject.user_id;
                bool module1 = Singleton.userobject.IsModuleAccess(1);
                bool module1Action1 = Singleton.userobject.IsModuleActionAccess(1,1);

                return View(ur.GetUserList(true));
            }
            catch (Exception ex)
            {
                log.Error("Index: " + ex.Message);
                return View();
                //throw;
            }
           
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
           
            try
            {
                ViewBag.PageHeader = "Add User";
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetUserTypeDDL();
                ViewBag.DDLUserType = list;
            }
            catch (Exception ex)
            {

               // throw;
            }
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(user_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                try
                {
                    ViewBag.PageHeader = "Add User";
                    obj.created_by = 1;
                    obj.created_datetime = DateTime.Now;
                    obj.modified_by = 1;
                    obj.modified_datetime = DateTime.Now;
                    obj.password = gcm.ComputeSha256Hash(obj.password);
                    int isAdd = ur.AddUser(obj);
                    if(isAdd==1)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    return View();
                    // throw;
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {            
            try
            {
                ViewBag.PageHeader = "Modify User";
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetUserTypeDDL();
                ViewBag.DDLUserType = list;

                var finduser = ur.GetUserByID(id);
                if(finduser!=null)
                {
                    return View(finduser);
                }

            }
            catch (Exception)
            {

               // throw;
            }
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,user_master obj)
        {
            try
            {
                // TODO: Edit update logic here
                ViewBag.PageHeader = "Modify User";
                if (ModelState.IsValid)
                {
                    obj.modified_by = 1;
                    obj.modified_datetime = DateTime.Now;
                    bool isupdate = ur.ModifyUser(obj);
                    if (isupdate)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if(id>0)
                {
                    user_master obj = new user_master();
                    obj.user_id = id;
                    obj.modified_by = 1;
                    obj.modified_datetime = DateTime.Now;
                    bool isdelete = ur.DeleteUser(obj);
                    if (isdelete)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
