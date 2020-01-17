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
                    else if ((Convert.ToInt32(Session["UserTypeID"]) != 1 && Convert.ToInt32(Session["UserTypeID"]) != 7))
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

                var list =await rs.GetViewRFIDScannerList("desc", Convert.ToInt32(Session["User_id"]), Convert.ToInt32(Session["UserTypeID"]));
                ViewBag.RFIDScannerList = list;
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
                        TempData["DeleteMessageCode"] = null;
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
        /// This method used to perform insert update delete operation if 0 its perform insert operation else perform modify operation
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Index(RFID_scanner_master obj)
        {
            try
            {
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                if (obj.id > 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate =await rs.ModifyRFIDScanner(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;
                    int isadd =await rs.AddRFIDScanner(obj);
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
        /// This method used to AddEdit RFID Scanner 
        /// coder : Pranali Patil
        /// </summary>
        public async Task<ActionResult> AddEditModel(int id)
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();
                //for Status dropdown              
                list =await cm.GetStatusDDL();
                ViewBag.DDLStatus = list;

                var obj =await rs.GetRFIDScannerByID(id);
                return PartialView("AddEditModel", obj);

            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
        }

        // GET: RFID/Delete/5
        /// <summary>
        /// This method used to Delete RFID Scanner
        /// coder : Pranali Patil
        /// </summary>
        public async Task<ActionResult> Delete(int id,int status)
        {
            try
            {
                RFID_scanner_master obj = new RFID_scanner_master();
                obj.id = id;
                obj.status = status;
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                int isdeleted =await rs.DeleteRFIDScanner(obj);
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
