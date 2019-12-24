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
    /// This control used for managing Route Household realted all functionality
    /// Coder: Pranali Patil
    /// </summary>
    public class RouteHouseholdDetailController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(WardController));
        RouteHouseholdDetailRepository rd = new RouteHouseholdDetailRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: RouteHouseholdDetail
        /// <summary>
        /// This method used to show Route Household list
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                var list = rd.GetRouteHouseholdDetailsList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                
            }
            
        }

        // GET: RouteHouseholdDetail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RouteHouseholdDetail/Create
        /// <summary>
        /// This method used to create Route Household 
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Create()
        {
            ViewBag.PageHeader = "Add Route household details";
            try
            {
                //for Route dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetRouteDDL();
                ViewBag.DDLRoute = list;

                //for Route dropdown
                list = cm.GetHouseholdDDL();
                ViewBag.DDLRouteHouse = list;

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

        // POST: RouteHouseholdDetail/Create
        /// <summary>
        /// This method used to save Route Household 
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Create(route_household_details obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = rd.AddRouteHouseholdDetails(obj);
                if (isadd == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    //for Route dropdown
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetRouteDDL();
                    ViewBag.DDLRoute = list;

                    //for Route dropdown
                    list = cm.GetHouseholdDDL();
                    ViewBag.DDLRouteHouse = list;

                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View();
                }

            }
            catch(Exception er)
            {
                log.Error("Error: " + er.Message);
                //for Route dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetRouteDDL();
                ViewBag.DDLRoute = list;

                //for Route dropdown
                list = cm.GetHouseholdDDL();
                ViewBag.DDLRouteHouse = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }

        // GET: RouteHouseholdDetail/Edit/5
        /// <summary>
        /// This method used to edit Route Household
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Edit(int id)
        {
            try
            {

                //for Route dropdown
                List<SelectListItem> list = new List<SelectListItem>();

                list = cm.GetRouteDDL();
                ViewBag.DDLRoute = list;

                //for Route Household dropdown
                list = cm.GetHouseholdDDL();
                ViewBag.DDLRouteHouse = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;

                var obj = rd.GetRouteHouseholdDetailsByID(id);
                return View(obj);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                
            }

            return View();
        }

        // POST: RouteHouseholdDetail/Edit/5
        /// <summary>
        /// This method used to update Route Household 
        /// coder : Pranali Patil
        /// </summary>
        [HttpPost]
        public ActionResult Edit(int id, route_household_details obj)
        {
            try
            {
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = rd.ModifyRouteHouseholdDetails(obj);
                if (isUpdate)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    //for Route dropdown
                    List<SelectListItem> list = new List<SelectListItem>();
                    list = cm.GetRouteDDL();
                    ViewBag.DDLRoute = list;

                    //for Route dropdown
                    list = cm.GetHouseholdDDL();
                    ViewBag.DDLRouteHouse = list;

                    //for Status dropdown              
                    list = cm.GetStatusDDL();
                    ViewBag.DDLStatus = list;
                    return View(obj);
                }
                
            }
            catch
            {
                //for Route dropdown
                List<SelectListItem> list = new List<SelectListItem>();
                list = cm.GetRouteDDL();
                ViewBag.DDLRoute = list;

                //for Route dropdown
                list = cm.GetHouseholdDDL();
                ViewBag.DDLRouteHouse = list;

                //for Status dropdown              
                list = cm.GetStatusDDL();
                ViewBag.DDLStatus = list;
                return View();
            }
        }

        // GET: RouteHouseholdDetail/Delete/5
        /// <summary>
        /// This method used to delete Route Household
        /// coder : Pranali Patil
        /// </summary>
        public ActionResult Delete(int id)
        {
            try
            {
                route_household_details obj = new route_household_details();
                obj.id = id;
                obj.status = 2;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = rd.DeleteRouteHouseholdDetails(obj);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                return RedirectToAction("Index");
                
            }
        }

        // POST: RouteHouseholdDetail/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
