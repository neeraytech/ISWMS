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
    public class WardController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(WardController));
        UserRepository ur = new UserRepository();
        WardRepository wr = new WardRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: Ward
        public ActionResult Index()
        {
            try
            {
                var list = wr.GetWardList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                //  throw;
            }
            return View();
        }

        // GET: Ward/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ward/Create
        public ActionResult Create()
        {
            ViewBag.PageHeader = "Add Ward";
            try
            {
                //for Karykarta dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetUserDDL(6);
                ViewBag.DDLUser = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
               // throw;
            }
           

            return View();
        }

        // POST: Ward/Create
        [HttpPost]
        public ActionResult Create(ward_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = wr.AddWard(obj);
                if(isadd==1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Ward/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //for Karykarta dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetUserDDL(6);
                ViewBag.DDLUser = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;

                var obj = wr.GetWardByID(id);
                return View(obj);
            }
            catch (Exception)
            {

                throw;
            }
         

            return View();
        }

        // POST: Ward/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ward_master obj)
        {
            try
            {
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = wr.ModifyWard(obj);
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ward/Delete/5
        public ActionResult Delete(int id)
        {
            
            try
            {
                ward_master obj = new ward_master();
                obj.id = id;
                obj.status = 2;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = wr.DeleteWard(obj);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
            
            return View();
        }

        // POST: Ward/Delete/5
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
