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
    /// This control used for managing GPS realted all functionality
    /// Coder: Pranali Patil
    /// </summary>
    public class GPSController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(GPSController));
        GpsRepository gr = new GpsRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: GPS
        /// <returns></returns>
        /// This method used to show GPS list
        /// coder : Pranali Patil
        public ActionResult Index()
        {
            try
            {
                var list = gr.GetGpsList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                
            }
            
        }

        // GET: GPS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GPS/Create
        /// <summary>
        /// This method used to create GPS
        /// coder : Pranali Patil
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.PageHeader = "Add GPS";
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

        // POST: GPS/Create
        /// <summary>
        /// This method used save the GPS
        /// coder : Pranali Patil
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(GPS_master obj)
        {
            try
            {
                // TODO: Add insert logic here

                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = gr.AddGps(obj);
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
            catch
            {
                List<SelectListItem> list = new List<SelectListItem>();
                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }

        // GET: GPS/Edit/5
        /// <summary>
        /// This method used to Edit GPS
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Edit(int id)
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();
                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;

                var obj = gr.GetGpsByID(id);
                return View(obj);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                return View();
            }

            
        }

        // POST: GPS/Edit/5
        /// <summary>
        /// This method used to Save updated GPS
        ///coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Edit(int id, GPS_master obj)
        {
            try
            {
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = gr.ModifyGps(obj);
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
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                List<SelectListItem> list = new List<SelectListItem>();
                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }

        // GET: GPS/Delete/5
        /// <summary>
        /// This method used to Delete GPS
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Delete(int id)
        {
            try
            {
                GPS_master obj = new GPS_master();
                obj.id = id;
                obj.status = 2;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = gr.DeleteGps(obj);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                return RedirectToAction("Index");
                
            }
            
        }

        // POST: GPS/Delete/5
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
