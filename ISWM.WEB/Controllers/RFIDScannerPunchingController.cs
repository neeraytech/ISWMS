using ISWM.WEB.BusinessServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ISWM.WEB.Controllers
{
    public class RFIDScannerPunchingController : Controller
    {
       
        RFIDScannerPunchingHistoryRepository rr = new RFIDScannerPunchingHistoryRepository();
        // GET: RFIDScannerPunching
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
            var list = await rr.GetRFIDPunchingtrackingList("desc");
            ViewBag.RFIDScannerPunchingList = list;
            return View();
        }
    }
}