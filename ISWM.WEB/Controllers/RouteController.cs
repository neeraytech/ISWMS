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
        public async Task<ActionResult> Index()
        {
            try
            {
                if (Session["User_id"] != null && Session["UserTypeID"] != null)
                {
                    if (Session["User_id"].ToString() == "0" )
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    else if ((Session["UserTypeID"].ToString() != "1" && Session["UserTypeID"].ToString() != "7"))
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }


                var list =await rr.GetViewRouteTypeList("desc", Convert.ToInt32( Session["User_id"]), Convert.ToInt32(Session["UserTypeID"]));
                ViewBag.RouteList = list;
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
        public async Task<ActionResult> Index(route_master obj)
        {
            try
            {
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                if (obj.id > 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate =await rr.ModifyRoute(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;
                    int isadd =await rr.AddRoute(obj);
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
        /// This method used to AddEdit Route
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

                var obj =await rr.GetRouteByID(id);
                return PartialView("AddEditModel", obj);

            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
        }


        // GET: Route/Delete/5
        /// <summary>
        /// This method used to delete Route
        /// coder : Pranali Patil
        /// </summary>
        public async Task<ActionResult> Delete(int id,int status)
        {
            try
            {
                route_master obj = new route_master();
                obj.id = id;
                obj.status = status;
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                int isdeleted =await rr.DeleteRoute(obj);
                TempData["DeleteMessageCode"] = isdeleted;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                return RedirectToAction("Index");
            }

        }

       
    }
}
