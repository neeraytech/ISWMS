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
        public async Task<ActionResult> Index()
        {
            try
            {

                if (Session["User_id"] != null )
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

                var list = await gr.GetViewGpsList("desc", Convert.ToInt32(Session["User_id"]), Convert.ToInt32(Session["UserTypeID"]));
                ViewBag.GPSList = list;
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
        /// This method used to create GPS post method
        /// coder: Kailas Ajabe
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Index(GPS_master obj)
        {
            try
            {

                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                if (obj.id > 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate = await gr.ModifyGps(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;
                    int isadd = await gr.AddGps(obj);
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

        // GET: GPS/Delete/5
        /// <summary>
        /// This method used to Delete GPS
        /// coder : Pranali Patil
        /// </summary>
        public async Task<ActionResult> Delete(int id,int status)
        {
            try
            {
                GPS_master obj = new GPS_master();
                obj.id = id;
                obj.status = status;
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                int isdeleted = await gr.DeleteGps(obj);
                TempData["DeleteMessageCode"] = isdeleted;
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                return RedirectToAction("Index");
                
            }
            
        }

       
        /// <summary>
        /// This method used to create GPS AddEdit Partial view
        /// coder: Kailas Ajabe
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

                var obj = await gr.GetGpsByID(id);
                return PartialView("AddEditModel", obj);

            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
        }

    }
}
