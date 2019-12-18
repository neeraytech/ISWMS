using ISWM.WEB.BusinessServices;
using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.BusinessServices.SingletonCS;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel obj)
        {
            obj.password = gcm.ComputeSha256Hash(obj.password);
            user_master ob = ur.VerifyLogin(obj);
            if(ob!=null)
            {
                Singleton.userobject.user_id = ob.user_id;
                Singleton.userobject.name = ob.name;
                Singleton.userobject.user_name = ob.user_name;
                Singleton.userobject.user_type = ob.userType_master.user_type;
                Singleton.userobject.user_type_id = ob.user_id;
                List<UserSecurityAccessModel> obList = new List<UserSecurityAccessModel>();
                foreach (var item in ob.user_security_access_details)
                {
                    UserSecurityAccessModel add = new UserSecurityAccessModel();
                    add.module_id = item.module_id;
                    add.action_id = item.action_id;
                    add.module_name = item.module_master.module_name;
                    add.action_name = item.actions_master.module_action_name;
                    add.hasAccess = item.hasAccess;
                    obList.Add(add);
                }
                Singleton.userobject.User_Security_Access_List = obList;

                return RedirectToAction("Index","User");

            }
            return View();
        }

        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Singleton.userobject.user_id = 0;
                Singleton.userobject.user_type_id = 0;
                Singleton.userobject.user_type = null;
                Singleton.userobject.name = null;
                Singleton.userobject.user_name = null;
                Singleton.userobject.User_Security_Access_List = null;

            }
            catch (Exception)
            {

                //throw;
            }

            return View();
        }
    }
}