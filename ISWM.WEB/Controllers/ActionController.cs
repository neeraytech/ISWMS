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
    /// This controller is used to Create,Edit and show list of Actions
    /// Coder: Smruti Wagh
    /// </summary>
   
    public class ActionController : Controller

    {
        ILog log = log4net.LogManager.GetLogger(typeof(ActionController));
        ActionRepository ar = new ActionRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        /// <summary>
        ///  This method used to show list of Actions
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        // GET: Action
        public async Task<ActionResult> Index()
        {
            try
            {
                if (Session["User_id"] != null && Session["UserTypeID"] != null)
                {
                    if (Session["User_id"].ToString() == "0" || Session["UserTypeID"].ToString() != "1")
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

                var list = await ar.GetViewActionList("desc");
                ViewBag.ActionList = list;
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
        /// This method used to create Action post method
        /// coder: Kailas Ajabe
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Index(actions_master obj)
        {
            try
            {

                obj.modified_by = Convert.ToInt32( Session["User_id"] );
                obj.modified_datetime = DateTime.Now;
                if (obj.module_action_id > 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate = await ar.Modifyactions_master(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;
                    int isadd = await ar.AddActions_master(obj);
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
        /// This method used to delete Actions
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Action/Delete/5
        public async Task<ActionResult> Delete(int id,int status)
        {
            try
            {
                actions_master obj = new actions_master();
                obj.module_action_id = id;
                obj.status = status;
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                int isdeleted = await ar.DeleteAction(obj);
                TempData["DeleteMessageCode"] = isdeleted;
                return RedirectToAction("Index");
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);                
                return RedirectToAction("Index");
            }
           
        }
        /// <summary>
        /// This method used to create Action AddEdit Partial view
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

                var obj = await ar.GetActionByID(id);
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
