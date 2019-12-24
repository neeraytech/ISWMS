using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISWM.WEB.BusinessServices;
using ISWM.WEB.CommonCode;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.BusinessServices.SingletonCS;

namespace ISWM.WEB.Controllers
{
    /// <summary>
    /// This control used for managing Driver realted all functionality
    /// Coder: Pranali Patil
    /// </summary>
    public class DriverController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(DriverController));
        DriverRepository dr = new DriverRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: Driver
        /// <summary>
        /// This method used to show Driver list
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                var list = dr.GetDriverList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
               
            }
            
        }

        // GET: Driver/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Driver/Create
        /// <summary>
        /// This method used to create driver
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.PageHeader = "Add Driver";
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

        // POST: Driver/Create
        /// <summary>
        /// This method used to save Driver
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Create(driver_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = dr.AddDriver(obj);
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

        // GET: Driver/Edit/5
        /// <summary>
        /// This method used to edit driver
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
                var obj = dr.GetDriverByID(id);
                if(obj!=null)
                {                    
                    return View(obj);
                }
                else
                {
                    return RedirectToAction("Index");
                }
               
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                return View();
            }


            
        }

        // POST: Driver/Edit/5
        /// <summary>
        /// This method used to save updated driver 
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Edit(int id, driver_master obj)
        {
            try
            {
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = dr.ModifyDriver(obj);
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
                log.Error("Error: "+ex.Message);
                List<SelectListItem> list = new List<SelectListItem>();
                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }

        // GET: Driver/Delete/5
        /// <summary>
        /// This method used to Delete Driver
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Delete(int id)
        {
            try
            {
                driver_master obj = new driver_master();
                obj.id = id;
                obj.status = 2;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = dr.DeleteDriver(obj);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                return RedirectToAction("Index");
            }

            
        }

        // POST: Driver/Delete/5
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
