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
    public class UserSecurityAccessController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(UserSecurityAccessController));
        UserSecurityAccessRepository usa = new UserSecurityAccessRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: UserSecurityAccess
        /// <summary>
        /// This method used to show User Security Access list
        /// coder : Pranali Patil
        /// </summary>
        public async Task<ActionResult> Index()
        {
            try
            {
                if (Session["User_id"] != null && Session["UserTypeID"] != null)
                {
                    if (Session["User_id"].ToString() == "0" ||Session["UserTypeID"].ToString() != "1")
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

                var list = await usa.GetViewUserSecurityList("desc");
                ViewBag.UserSecurityList = list;
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
        /// This method used to perform Insert Update Delete operations if 0 its perform insert operation else perform modify operation
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Index(user_security_access_details obj)
        {
            try
            {

                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                if (obj.id > 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate =await usa.ModifyUserSecAc(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;
                    int isadd =await usa.AddUserSecAc(obj);
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
        /// This method used to AddEdit User Security Access 
        /// coder : Pranali Patil
        /// </summary>
        public async Task<ActionResult> AddEditModel(int id)
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();
                //for User dropdown              
                list = await cm.GetUserDDL(0);
                ViewBag.DDLUser = list;

                //for Module Type dropdown              
                list = await cm.GetModuleDDL(1);
                ViewBag.DDLModule = list;

                //for Action Type dropdown              
                list = await cm.GetActionDDL(1);
                ViewBag.DDLAction = list;

                //for Status dropdown              
                list = await cm.GetStatusDDL();
                ViewBag.DDLStatus = list;

                var obj =await usa.GetUSADByID(id);
                return PartialView("AddEditModel", obj);

            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
        }

        // GET: UserSecurityAccess/Delete/5
        // GET: Driver/Delete/5
        /// <summary>
        /// This method used to Delete User Security
        /// coder : Pranali Patil
        /// </summary>
        public async Task<ActionResult> Delete(int id, int status)
        {
            try
            {
                user_security_access_details obj = new user_security_access_details();
                obj.id = id;
                obj.status = status;
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                int isdeleted =await usa.DeleteUserSecAc(obj);
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

