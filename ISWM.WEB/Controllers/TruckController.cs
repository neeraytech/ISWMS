using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.BusinessServices;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.CommonCode;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISWM.WEB.BusinessServices.SingletonCS;

namespace ISWM.WEB.Controllers
{
    public class TruckController : Controller
    {
        /// <summary>
        /// This control used for managing Truck realted all functionality
        /// Coder: Pranali Patil
        /// </summary>
        ILog log = log4net.LogManager.GetLogger(typeof(WardController));
        TruckRepository tr = new TruckRepository(); 
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: truck
        /// <summary>
        /// This method used to show Truck list
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                var list = tr.GetTruckList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                
            }
            
        }

        // GET: truck/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: truck/Create
        /// <summary>
        /// This method used to create Truck 
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.PageHeader = "Add truck";
            try
            {
                //for GPS dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetGpsDDL();
                ViewBag.DDLGps = list;

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

        // POST: truck/Create
        /// <summary>
        /// This method used to save Truck 
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Create(truck_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = tr.AddTruck(obj);
                if (isadd == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    //for GPS dropdown
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetGpsDDL();
                    ViewBag.DDLGps = list;

                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View();
                }

            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                //for GPS dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetGpsDDL();
                ViewBag.DDLGps = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();

            }

        }

        // GET: truck/Edit/5
        /// <summary>
        /// This method used to edit Truck 
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Edit(int id)
        {
            try
            {
                //for GPS dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetGpsDDL();
                ViewBag.DDLGps = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;

                var obj = tr.GetTruckByID(id);
                return View(obj);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                
            }


            
        }

        // POST: truck/Edit/5
        /// <summary>
        /// This method used to update Truck 
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Edit(int id, truck_master obj)
        {
            try
            {
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = tr.ModifyTruck(obj);
                if (isUpdate)
                {
                    return RedirectToAction("Index");

                }
                
                // TODO: Add update logic here
                else
                {
                    //for GPS dropdown
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetGpsDDL();
                    ViewBag.DDLGps = list;

                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View(obj);
                    
                }
                
            }
            catch(Exception er)
            {
                log.Error("Error: " + er.Message);
                //for GPS dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetGpsDDL();
                ViewBag.DDLGps = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }

        // GET: truck/Delete/5
        /// <summary>
        /// This method used to delete Truck 
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Delete(int id)
        {
            try
            {
                truck_master obj = new truck_master();
                obj.id = id;
                obj.status = 2;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = tr.DeleteTruck(obj);
                return RedirectToAction("Index");
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
                
            }

        }

        // POST: truck/Delete/5
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
