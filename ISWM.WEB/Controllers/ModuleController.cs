using ISWM.WEB.BusinessServices.SingletonCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISWM.WEB.BusinessServices;
using log4net;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.CommonCode;
using ISWM.WEB.BusinessServices.Repository;

namespace ISWM.WEB.Controllers
{
    /// <summary>
    /// This Controller is used to Create, Edite and show list for Mudules
    /// coder : Smruti Wagh
    /// </summary>
    public class ModuleController : Controller
    {
        ILog log = log4net.LogManager.GetLogger(typeof(ModuleController));
        ModuleRepository mr = new ModuleRepository();
        CommonCS cm = new CommonCS();
        GCommon gcm = new GCommon();
        // GET: Module
        /// <summary>
        /// This method used to create module
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                var list = mr.GetmoduleList();
                return View(list);
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
                
            }            
        }

        /// <summary>
        /// This method used to create Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>

        // GET: Module/Create
        public ActionResult Create()
        {
            try {
                return View();
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
            }
        }
        /// <summary>
        /// This method used to save created Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: Module/Create
        [HttpPost]
        public ActionResult Create(module_master obj)
        {
            try
            {
                // TODO: Add insert logic here
                obj.created_by = Singleton.userobject.user_id;
                obj.created_datetime = DateTime.Now;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                int isadd = mr.Addmodule(obj);
                if (isadd == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
               
            }
            catch( Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
            }
        }
        /// <summary>
        /// This method used to save Edited Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Module/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {

                var obj = mr.GetmoduleByID(id);
                if(obj!=null)
                {
                    return View(obj);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception er)
            {
                log.Error("Error: " + er.Message);
                return View();
            }
           
        }
        /// <summary>
        /// This method used to save Edited Module
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        // POST: Module/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, module_master obj)
        {
            try
            {
                // TODO: Add update logic here
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isUpdate = mr.Modifymodule(obj);
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
                return View();
            }
        }
        /// <summary>
        /// This method used to Delete Module(change only status)
        /// coder: Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Module/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                module_master obj = new module_master();
                obj.module_id = id;
                obj.isActivie = false;
                obj.modified_by = Singleton.userobject.user_id;
                obj.modified_datetime = DateTime.Now;
                bool isdeleted = mr.Deletemodule(obj);
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
