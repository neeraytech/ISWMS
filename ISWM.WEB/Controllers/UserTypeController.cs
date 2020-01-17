using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.BusinessServices.SingletonCS;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.CommonCode;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISWM.WEB.BusinessServices;
using ISWM.WEB.Models.Models;
using System.Threading.Tasks;

namespace ISWM.WEB.Controllers
{/// <summary>
/// this controller is used for data type create, edit, index view
/// </summary>
    public class UserTypeController : Controller
    {
       
        ILog log = log4net.LogManager.GetLogger(typeof(UserTypeController));
        UserRepository ur = new UserRepository();
        UserTypeRepository ut = new UserTypeRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        /// <summary>
        /// This method use to show list  of User Type
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        // GET: UserType
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

                var list = await ut.GetViewUserTypeList("desc");
                ViewBag.UserTypeList = list;
                if(TempData["MessageCode"]!=null)
                {
                    ViewBag.MessageCode = TempData["MessageCode"];
                    if(ViewBag.MessageCode==1)
                    {
                        ViewBag.MessageTxt = "Data updated successfully.";
                    }
                    else if(ViewBag.MessageCode == -1)
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

        [HttpPost]
        public async Task<ActionResult> Index(userType_master obj)
        {
            try
            {
               
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                if (obj.user_type_id > 0)
                {
                    // TODO: Update insert logic here
                    int isUpdate = await ut.ModifyUserType(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();                    
                    
                }
                else
                {
                    
                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;
                    int isadd =await ut.AddUserType(obj);
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
        /// This  method used  Create Add Edit PartailView 
        /// Coder: Dhananjay Powar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  async Task<ActionResult> AddEditModel(int id)
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();
                //for Status dropdown              
                list = await cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                userType_master obj = new userType_master();
                obj = await ut.GetUserTypeByID(id);
                return PartialView("AddEditModel", obj);

            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// This method used to delete user type
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserType/Delete/5
        public async Task<ActionResult> Delete(int id,int status)
        {
            try
            {
                userType_master obj = new userType_master();
                obj.user_type_id = id;
                obj.status = status;
                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                int isdeleted =await ut.DeleteUserType(obj);
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
