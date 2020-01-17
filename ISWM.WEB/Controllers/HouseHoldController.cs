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
/// This controller is used to create, edit and show list 
/// coder: Smruti Wagh
/// </summary>
    public class HouseHoldController : Controller
    {
        /// <summary>
        /// This conroller is used for Create, Edit and show list
        /// coder: Smruti Wagh
        /// </summary>
        ILog log = log4net.LogManager.GetLogger(typeof(HouseHoldController));
        HouseholdRepository hr = new HouseholdRepository();
        WardRepository wr = new WardRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        /// <summary>
        /// this method is use for show list of households 
        /// coder : Smruti Wagh
        /// </summary>
        /// <returns></returns>
        // GET: HouseHold

        public async Task<ActionResult>Index()
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

                var list =await hr.GetViewhouseholdList("desc", Convert.ToInt32(Session["User_id"]), Convert.ToInt32(Session["UserTypeID"]));
                ViewBag.HouseholdList = list;
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
                //  throw;
            }
            
        }
        /// <summary>
        /// This method used to perform Insert Updte Delete operations is 0 it performs insert operation else perform modify operation
        /// coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Index(household_master obj)
        {
            try
            {

                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                if (obj.id > 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate =await hr.Modifyhousehold(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;
                    int isadd =await hr.Addhousehold(obj);
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
                list = await cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                //for ward drpdown               
                list =await cm.GetWardDDL(1);
                ViewBag.DDLWard = list;
                //for Area Dropdown
                list = await cm.GetAreaDDL(1);
                ViewBag.DDLArea = list;

                var obj =await hr.GethouseholdByID(id);
                if (obj != null)
                {
                    if(obj.id>0)
                    {
                        ViewBag.FromDate = Convert.ToDateTime(obj.valid_from).ToString("dd-MMM-yyyy");
                        ViewBag.ToDate = Convert.ToDateTime(obj.valid_to).ToString("dd-MMM-yyyy");
                    }
                    else
                    {

                        ViewBag.FromDate = "";
                        ViewBag.ToDate = "";

                    }
                    
                }
                else
                {
                    ViewBag.FromDate = "";
                    ViewBag.ToDate = "";
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
        /// this method is use for delete household(status change to inactive) 
        /// coder : Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: HouseHold/Delete/5
        public async Task<ActionResult> Delete(int id,int status)
        {

            try
            {
                household_master obj = new household_master();
                obj.id = id;
                obj.status = status;
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                int isdeleted =await hr.Deletehousehold(obj);
                TempData["DeleteMessageCode"] = isdeleted;
                return RedirectToAction("Index");
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
               
            }
            return View();
        }

       
    }
}
