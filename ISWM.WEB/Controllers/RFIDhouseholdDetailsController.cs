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
    /// This controller is used to Create, Edit and show List of RFID and Household Details
    /// coder:Smruti Wagh
    /// </summary>
    public class RFIDhouseholdDetailsController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(RFIDhouseholdDetailsController));
        RFIDhouseholdDetailsRepository rhd = new RFIDhouseholdDetailsRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        /// <summary>
        /// this method is use to show list of RFID allocated households
        /// coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        // GET: RFIDhouseholdDetails
        public ActionResult Index()
        {
            try
            {
                var list = rhd.GetRHDList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
               
            }
           
        }

        /// <summary>
        /// This method is use to create RFID households
        /// coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>

        // GET: RFIDhouseholdDetails/Create
        public ActionResult Create()
        {

            ViewBag.PageHeader = "Add RFID";
            try
            {
                //for household dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetHouseholdDDL();
                ViewBag.DDLhousehold = list;
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
        /// <summary>
        /// This method is use to save createD RFID households
        /// coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: RFIDhouseholdDetails/Create
        [HttpPost]
        public ActionResult Create(RFID_household_details obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = rhd.AddRHD(obj);
                if (isadd==1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    //for household dropdown
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetHouseholdDDL();
                    ViewBag.DDLhousehold = list;
                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View();
                }
                
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                //for household dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetHouseholdDDL();
                ViewBag.DDLhousehold = list;
                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }
        /// <summary>
        /// This method is use to Edit RFID households
        /// coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: RFIDhouseholdDetails/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //for Household dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetHouseholdDDL();
                ViewBag.DDLhousehold = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;

                var obj = rhd.GetRHDByID(id);
                return View(obj);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                //for Household dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetHouseholdDDL();
                ViewBag.DDLhousehold = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
           
        }
        /// <summary>
        /// This method is use to save edited RFID households
        /// coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: RFIDhouseholdDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RFID_household_details obj)
        {
            try
            {
                // TODO: Add update logic here
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = rhd.ModifyRHD(obj);
                if (isUpdate)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetHouseholdDDL();
                    ViewBag.DDLhousehold = list;

                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View(obj);
                }
               
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                //for Household dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetHouseholdDDL();
                ViewBag.DDLhousehold = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }
        /// <summary>
        /// This method is use to delete RFID households
        /// coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: RFIDhouseholdDetails/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                RFID_household_details obj = new RFID_household_details();
                obj.id = id;
                obj.status = 2;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = rhd.DeleteRHD(obj);
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
