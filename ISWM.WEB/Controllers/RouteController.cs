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
    public class RouteController : Controller
    {
        /// <summary>
        /// This control used for managing Route realted all functionality
        /// Coder: Pranali Patil
        /// </summary>
        ILog log = log4net.LogManager.GetLogger(typeof(WardController));
        RouteRepository rr = new RouteRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: Route
        /// <summary>
        /// This method used to show Route list
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                var list = rr.GetRouteList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                
            }
            
        }

        // GET: Route/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Route/Create
        /// <summary>
        /// This method used to create Route
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.PageHeader = "Add Route";
            try
            {
                
                List<SelectListItem> list = new List<SelectListItem>();            
                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                
            }

            return View();
        }

        // POST: Route/Create
        /// <summary>
        /// This method used to save Route
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Create(route_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = rr.AddRoute(obj);
                if (isadd == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    List<SelectListItem> list = new List<SelectListItem>();
                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View();
                }

            }
            catch(Exception ex)
            {
                log.Error("Error: " + ex.Message);
                List<SelectListItem> list = new List<SelectListItem>();
                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }

        // GET: Route/Edit/5
        /// <summary>
        /// This method used to edit Route
        /// /// coder : Pranali Patil
        /// </summary>
        public ActionResult Edit(int id)
        {
            try
            {
               
                List<SelectListItem> list = new List<SelectListItem>();
               //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                var obj = rr.GetRouteByID(id);
                return View(obj);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
               
            }

            return View();
        }

        // POST: Route/Edit/5
        /// <summary>
        /// This method used to update route
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Edit(int id, route_master obj)
        {
            try
            {
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = rr.ModifyRoute(obj);
                if (isUpdate)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    List<SelectListItem> list = new List<SelectListItem>();
                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View(obj);
                   
                }
                   
            }
            catch(Exception ex)
            {
                log.Error("Error: " + ex.Message);
                List<SelectListItem> list = new List<SelectListItem>();
                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }

        // GET: Route/Delete/5
        /// <summary>
        /// This method used to delete Route
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Delete(int id)
        {
            try
            {
                route_master obj = new route_master();
                obj.id = id;
                obj.status = 2;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = rr.DeleteRoute(obj);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                return RedirectToAction("Index");
            }

        }

        // POST: Route/Delete/5
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
