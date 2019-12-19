using ISWM.WEB.BusinessServices.Repository;
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
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: Ward
        public ActionResult Index()
        {
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
                list = cm.GetStatusDDL(6);
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ward/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ward/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
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
