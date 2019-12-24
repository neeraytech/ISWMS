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
    /// This control used for managing RFID Scanner realted all functionality
    /// Coder: Pranali Patil
    /// </summary>
    public class RFIDScannerController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(WardController));
        RFIDScannerRepository rs = new RFIDScannerRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: RFID
        /// <summary>
        /// This method used to show RFID Scanner list
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                var list = rs.GetRFIDScannerList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
               
            }
           
        }

        // GET: RFID/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RFID/Create
        /// <summary>
        /// This method used to create RFID Scanner
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.PageHeader = "Add RFID Scanner";
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

        // POST: RFID/Create
        /// <summary>
        /// This method used to save RFID Scanner
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Create(RFID_scanner_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                obj.password = obj.password; //gcm.ComputeSha256Hash(obj.password);
                int isadd = rs.AddRFIDScanner(obj);
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

        // GET: RFID/Edit/5
        /// <summary>
        /// This method used to edit RFID Scanner
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

                var obj = rs.GetRFIDScannerByID(id);
                return View(obj);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                
            }

            return View();
        }

        // POST: RFID/Edit/5
        /// <summary>
        /// This method used to update RFID Scanner
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Edit(int id, RFID_scanner_master obj)
        {
            try
            {
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = rs.ModifyRFIDScanner(obj);
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

        // GET: RFID/Delete/5
        /// <summary>
        /// This method used to Delete RFID Scanner
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Delete(int id)
        {
            try
            {
                RFID_scanner_master obj = new RFID_scanner_master();
                obj.id = id;
                obj.status = 2;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = rs.DeleteRFIDScanner(obj);
                return RedirectToAction("Index");
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
               
            }
  
        }

        // POST: RFID/Delete/5
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
