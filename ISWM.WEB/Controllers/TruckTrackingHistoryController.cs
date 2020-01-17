using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.BusinessServices.SingletonCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ISWM.WEB.Controllers
{
    public class TruckTrackingHistoryController : Controller
    {
        TruckTrackingHistoryRepository tr = new TruckTrackingHistoryRepository();
        // GET: TruckTrackingHistory
        public async Task<ActionResult> Index()
        {
            if (Session["User_id"] != null && Session["UserTypeID"] != null)
            {
                if (Session["User_id"].ToString() == "0")
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            var list = await tr.GetTruckTrackingHistoryList("desc");
            ViewBag.TruckTrackingList = list;
            return View();
        }
    }
}