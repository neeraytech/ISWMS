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
using System.Threading.Tasks;

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
        public async Task<ActionResult> Index()
        {
            try
            {
                if (Session["User_id"] != null)
                {
                    if (Session["User_id"].ToString() == "0")
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    else if ((Convert.ToInt32(Session["UserTypeID"]) != 1 && Convert.ToInt32(Session["UserTypeID"]) != 6 && Convert.ToInt32(Session["UserTypeID"]) != 3))
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

                var list =await rhd.GetViewRFIDHouseholdList("desc", Convert.ToInt32(Session["User_id"]), Convert.ToInt32(Session["UserTypeID"]));
                ViewBag.RFID_household_detailsList = list;
                if (TempData["MessageCode"] != null)
                {
                    ViewBag.MessageCode = TempData["MessageCode"];
                    if (ViewBag.MessageCode == 1)
                    {
                        ViewBag.MessageTxt = "Data updated successfully.";
                    }
                    else if (ViewBag.MessageCode == -1)
                    {
                        ViewBag.MessageTxt = "Data already available.";
                    }
                    else
                    {
                        ViewBag.MessageTxt = "Some error occurred while updating data.";
                    }
                    TempData["MessageCode"] = null;
                }
                else
                {
                    if (TempData["DeleteMessageCode"] != null)
                    {
                        ViewBag.MessageCode = TempData["DeleteMessageCode"];
                        if (ViewBag.MessageCode == 1)
                        {
                            ViewBag.MessageTxt = "Data Activate Successfully.";
                        }
                        else if (ViewBag.MessageCode == 2)
                        {
                            ViewBag.MessageCode = 1;
                            ViewBag.MessageTxt = "Data Inactivate Successfully.";
                        }
                        else
                        {
                            ViewBag.MessageTxt = "Some error occurred while deleting data.";
                        }
                        TempData["DeleteMessageCode"] = null; ;
                    }
                    else
                    {
                        ViewBag.MessageCode = null;
                    }
                }
                return View();
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);             
                return View();               
            }
           
        }
        /// <summary>
        /// This method used to perform Insert Updte Delete operations is 0 it performs insert operation else perform modify operation
        /// coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Index(RFID_household_details obj)
        {
            try
            {

                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                if (obj.id > 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate =await rhd.ModifyRHD(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;
                    int isadd =await rhd.AddRHD(obj);
                    TempData["MessageCode"] = isadd;
                    return View();
                }
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                //  throw;
            }

        }
        /// <summary>
        /// This method is used to Add Update with Partial view.
        /// coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddEditModel(int id)
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();
                //for Status dropdown              
                list =await cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                //for household dropdown
                list =await cm.GetHouseholdDDL(1);
                ViewBag.DDLhousehold = list;
                var obj =await rhd.GetRHDByID(id);
                return PartialView("AddEditModel", obj);

            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
        }
       
        /// <summary>
        /// This method is use to delete RFID households
        /// coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: RFIDhouseholdDetails/Delete/5
        public async Task<ActionResult> Delete(int id,int status)
        {
            try
            {
                RFID_household_details obj = new RFID_household_details();
                obj.id = id;
                obj.status = status;
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                int isdeleted =await rhd.DeleteRHD(obj);
                TempData["DeleteMessageCode"] = isdeleted;
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
