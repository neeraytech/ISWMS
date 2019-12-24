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

        public ActionResult Index()
        {
            try
            {
                var list = hr.GethouseholdList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                //  throw;
            }
            
        }

        /// <summary>
        /// this method is use for create View for household 
        /// coder : Smruti Wagh
        /// </summary>
        /// <returns></returns>

        // GET: HouseHold/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.PageHeader = "Add Ward";
                //for Ward dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetWardDDL();
                ViewBag.DDLWard = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);                
            }
            return View();
        }
        /// <summary>
        /// this method is use for save created household 
        /// coder : Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: HouseHold/Create
        [HttpPost]
        public ActionResult Create(household_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = hr.Addhousehold(obj);
                if (isadd == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetWardDDL();
                    ViewBag.DDLWard = list;

                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View();
                }
                
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetWardDDL();
                ViewBag.DDLWard = list;
                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }
        /// <summary>
        /// this method is use for edit View for household 
        /// coder : Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: HouseHold/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //for  dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetWardDDL();
                ViewBag.DDLWard = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;

                var obj = hr.GethouseholdByID(id);
                return View(obj);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                //throw;
            }
            return View();
        }
        /// <summary>
        /// this method is use for save edited household 
        /// coder : Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: HouseHold/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, household_master obj)
        {
            try
            {
                // TODO: Add update logic here
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = hr.Modifyhousehold(obj);
                if (isUpdate)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetWardDDL();
                    ViewBag.DDLWard = list;

                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View(obj);
                }
                   
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View(obj);
            }
        }
        /// <summary>
        /// this method is use for delete household(status change to inactive) 
        /// coder : Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: HouseHold/Delete/5
        public ActionResult Delete(int id)
        {

            try
            {
                household_master obj = new household_master();
                obj.id = id;
                obj.status = 2;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = hr.Deletehousehold(obj);
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
