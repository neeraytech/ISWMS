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
    public class UserController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(UserController));
        UserRepository ur = new UserRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: User
        public async Task<ActionResult> Index()
        {
            try
            {  
                if (Session["User_id"] != null && Session["UserTypeID"] != null)
                {
                    if (Session["User_id"].ToString() == "0")
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    else if ((Session["UserTypeID"].ToString() != "1" && Session["UserTypeID"].ToString() != "3"))
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }


                var list =await ur.GetViewUserList("desc", Convert.ToInt32(Session["User_id"]), Convert.ToInt32(Session["UserTypeID"]));
                ViewBag.UserList = list;
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
            catch (Exception ex)
            {
                log.Error("Index: " + ex.Message);
                return View();
                //throw;
            }
           
        }

        [HttpPost]
        public async Task<ActionResult> Index(user_master obj)
        {
            try
            {

                obj.modified_by = Convert.ToInt32(Session["User_id"]);
                obj.modified_datetime = DateTime.Now;
                if (obj.user_id > 0)
                {
                    int isUpdate =await ur.ModifyUser(obj);
                    TempData["MessageCode"] = isUpdate;
                    return View();
                }
                else
                {

                    // TODO: Add insert logic here
                    obj.created_by = Convert.ToInt32(Session["User_id"]);
                    obj.created_datetime = DateTime.Now;                    
                    obj.password = gcm.ComputeSha256Hash(obj.password);
                    int isadd =await ur.AddUser(obj);
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


        public async Task<ActionResult> AddEditModel(int id)
        {
            try
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list = await cm.GetUserTypeDDL(1);
                ViewBag.DDLUserType = list;
                if(Convert.ToInt32(Session["UserTypeID"]) != 1)
                {
                    ViewBag.DDLUserType = list.Where(w=>w.Value=="6").ToList();
                }

                list = await cm.GetStatusDDL();
                ViewBag.DDLStatus = list;

                //for Area Dropdown
                list = await cm.GetAreaDDL(1);
                ViewBag.DDLArea = list;

                var obj =await ur.GetUserByID(id);
                ViewBag.User_id = obj.user_id;
                return PartialView("AddEditModel", obj);

            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(int id,int status)
        {
            try
            {
                if(id>0)
                {
                    user_master obj = new user_master();
                    obj.user_id = id;
                    obj.status = status;
                    obj.modified_by = Convert.ToInt32(Session["User_id"]);
                    obj.modified_datetime = DateTime.Now;
                    int isdeleted =await ur.DeleteUser(obj);
                    TempData["DeleteMessageCode"] = isdeleted;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception er)
            {

                log.Error("Error: " + er.Message);
                return RedirectToAction("Index");
            }
            return View();
        }

        
    }
}
