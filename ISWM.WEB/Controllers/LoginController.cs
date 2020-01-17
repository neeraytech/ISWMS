using ISWM.WEB.BusinessServices;
using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.BusinessServices.SingletonCS;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ISWM.WEB.Controllers
{
    public class LoginController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(LoginController));
        UserRepository ur = new UserRepository();
        GCommon gcm = new GCommon();
        // GET: Login
        public async Task<ActionResult> Index()
        {
            ClearAllSession();
            ViewBag.MessageCode = null;
            ViewBag.MessageTxt = "";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginModel obj)
        {
            try
            {
                obj.password = gcm.ComputeSha256Hash(obj.password);
                user_master ob =await ur.VerifyLogin(obj);
                if (ob != null)
                {
                    if(ob.status == 1)
                    {
                        Session["User_id"] = ob.user_id;
                        Session["Name"] = ob.name;
                        Session["UserName"] = ob.user_name;
                        Session["UserType"] = ob.userType_master.user_type;
                        Session["UserTypeID"] = ob.user_type;
                        List<UserSecurityAccessModel> obList = new List<UserSecurityAccessModel>();
                        foreach (var item in ob.user_security_access_details)
                        {
                            UserSecurityAccessModel add = new UserSecurityAccessModel();
                            add.module_id = item.module_id;
                            add.action_id = item.action_id;
                            add.module_name = item.module_master.module_name;
                            add.action_name = item.actions_master.module_action_name;
                            add.status_id = item.status;
                            obList.Add(add);
                        }
                        Session["USA_List"] = obList;
                        ViewBag.MessageCode = null;
                        ViewBag.MessageTxt = "";
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ViewBag.MessageCode = 0;
                        ViewBag.MessageTxt = "You are not a active user, please contact to administrator.";
                    }
                   
                }
                else
                {
                    ViewBag.MessageCode =0;
                    ViewBag.MessageTxt = "Invalid User Name or Password.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.MessageCode = -1;
                ViewBag.MessageTxt = "Opps...! Something Error has been occured.";
                log.Error("Error: "+ex.Message);
                //throw;
            }
            
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            try
            {
                ClearAllSession();
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                return RedirectToAction("Index", "Login");
                //throw;
            }

           
        }
        public async void ClearAllSession()
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);              
                //throw;
            }

        }
    }
}