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
    /// This controller is used to Create,Edit and show list of Actions
    /// Coder: Smruti Wagh
    /// </summary>
   
    public class ActionController : Controller

    {
        ILog log = log4net.LogManager.GetLogger(typeof(ActionController));
        ActionRepository ar = new ActionRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        /// <summary>
        ///  This method used to show list of Actions
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        // GET: Action
        public ActionResult Index()
        {
            try
            {
                var list = ar.GetActionList();
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
        /// This method used to create view for Actions
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>

        // GET: Action/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch(Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
            }
           
        }
        /// <summary>
        /// This method used save cteated Actions
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: Action/Create
        [HttpPost]
        public ActionResult Create(actions_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = ar.AddActions_master(obj);
                if (isadd == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
                
            }
            catch(Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
            }
        }
        /// <summary>
        /// This method used to Edit view for Actions
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Action/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var obj = ar.GetActionByID(id);
                return View(obj);
            }
            catch(Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
            }
           
        }
        /// <summary>
        /// This method used to save edited Actions
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: Action/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, actions_master obj)
        {
            try
            {
                // TODO: Add update logic here
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = ar.Modifyactions_master(obj);
                if (isUpdate)
                {
                    return RedirectToAction("Index");

                }
                else
                {
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
        /// This method used to delete Actions
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Action/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                actions_master obj = new actions_master();
                obj.module_action_id = id;
                obj.isActivie = false;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = ar.DeleteAction(obj);
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
