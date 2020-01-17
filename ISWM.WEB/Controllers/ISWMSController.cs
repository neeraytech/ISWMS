using ISWM.WEB.BusinessServices;
using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.BusinessServices.SingletonCS;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.CommonCode;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ISWM.WEB.Controllers
{
    public class ISWMSController : Controller
    {
        // GET: ISWMS
        ILog log = log4net.LogManager.GetLogger(typeof(WardController));
        ISWMSRepository ir = new ISWMSRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();

        /// <summary>
        /// To show Data table for ISWMS
        /// coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        // GET: ISWMS
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

                var list =await ir.GetViewISWMSList("desc", Convert.ToInt32(Session["User_id"]), Convert.ToInt32(Session["UserTypeID"]));
                ViewBag.ISWMSList = list;
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
                ViewBag.ISWMSList = null;
                return View();
                //  throw;
            }
        }
        /// <summary>
        ///  This method used to perform Insert Updte Delete operations is 0 it performs insert operation else perform modify operation
        /// coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Index(ISWMS_master obj)
        {
            try
            {

                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                if (obj.id > 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate =await ir.ModifyISWMS(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;
                    int isadd =await ir.AddISWMS(obj);
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
        /// This method is used to perform Add Update with partial view.
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
                //get route dropdown
                list =await cm.GetRouteDDL(1);
                ViewBag.DDLRoute = list;
                //get driver dropdown
                list =await cm.GetDriverDDL(1);
                ViewBag.DDLDriver = list;
                //get truck dropdown
                list =await cm.GetTruckDDL(1);
                ViewBag.DDLTruck = list;
                //get Scanner dropdown 
                list =await cm.GetScannerDDL(1);
                ViewBag.DDLScanner = list;

                var obj =await ir.GetISWMSByID(id);
                if (obj != null)
                {
                    if(obj.id>0)
                    {
                        ViewBag.sDate = Convert.ToDateTime(obj.date).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        ViewBag.sDate = "";
                    }
                    
                }
                return PartialView("AddEditModel", obj);

            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// this method is use for delete ISWMS(status change to inactive) 
        /// coder : Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Ward/Delete/5
        public async Task<ActionResult> Delete(int id,int status)
        {

            try
            {
                ISWMS_master obj = new ISWMS_master();
                obj.id = id;
                obj.status = status;
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                int isdeleted =await ir.DeleteISWMS(obj);
                TempData["DeleteMessageCode"] = isdeleted;
                return RedirectToAction("Index");
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                //throw;
            }

            return View();
        }

    }
}
