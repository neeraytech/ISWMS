using ISWM.WEB.BusinessServices.SingletonCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISWM.WEB.BusinessServices;
using log4net;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.CommonCode;
using ISWM.WEB.BusinessServices.Repository;
using System.Threading.Tasks;

namespace ISWM.WEB.Controllers
{
    /// <summary>
    /// This Controller is used to Create, Edite and show list for Mudules
    /// coder : Smruti Wagh
    /// </summary>
    public class ModuleController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(ModuleController));
        ModuleRepository mr = new ModuleRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: Module
        /// <summary>
        /// This method used to create module
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
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

                var list =await mr.GetViewModuleTypeList("desc");

                ViewBag.ModuleList = list;
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
        /// This used to create module(post)
        /// Coder: Kailas Ajabe
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Index(module_master obj)
        {
            try
            {

                obj.modified_by = Singleton.userobject.user_id; ;
                obj.modified_datetime = DateTime.Now;
                if (obj.module_id> 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate =await mr.Modifymodule(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Singleton.userobject.user_id; ;
                    obj.created_datetime = DateTime.Now;
                    int isadd =await mr.Addmodule(obj);
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
        /// This method used to Delete Module(change only status)
        /// coder: Kailas Ajabe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Module/Delete/5
        public async Task<ActionResult> Delete(int id, int status)
        {
            try
            {
                module_master obj = new module_master();
                obj.module_id = id;
                obj.status = status;
                obj.modified_by = Singleton.userobject.user_id; ;
                obj.modified_datetime = DateTime.Now;
                int isdeleted =await mr.Deletemodule(obj);
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
        /// This method used to create module AddEdit Partial view
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

                var obj =await mr.GetmoduleByID(id);
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
