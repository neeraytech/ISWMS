using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISWM.WEB.CommonCode
{
    public class CommonCS
    {
        HouseholdRepository hr = new HouseholdRepository();
        UserRepository ur = new UserRepository();
        ModuleRepository mr = new ModuleRepository();
        WardRepository wr = new WardRepository();
        GpsRepository gr = new GpsRepository();
        RouteRepository rr = new RouteRepository();
        public List<SelectListItem> GetUserTypeDDL()
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = ur.GetUserType();
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.user_type_id.ToString();
                    ob.Text = item.user_type;
                    objlist.Add(ob);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return objlist;
        }

        public List<SelectListItem> GetUserDDL(int typeid)
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = ur.GetUserListByType(typeid);
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.user_id.ToString();
                    ob.Text = item.name+"-"+item.area+"-"+item.contact_no;
                    objlist.Add(ob);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return objlist;
        }       

        /// <summary>
        /// this method is use to drop down for status
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetStatusDDL()
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {

                string statusJson = ConfigurationManager.AppSettings["Status"];
                var result = JsonConvert.DeserializeObject<RootObject>(statusJson);
                foreach (var item in result.status)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.statusVal;
                    objlist.Add(ob);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return objlist;
        }

        /// <summary>
        /// this method is used to drop down for module details
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetModuleDDL()
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = mr.GetmoduleList();
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.module_id.ToString();
                    ob.Text = item.module_name;
                    objlist.Add(ob);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return objlist;
        }
        /// <summary>
        /// this method is used to drop down list for ward detail
        /// coder : smruti Wagh
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetWardDDL()
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = wr.GetWardList();
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.ward_number + "-" + item.ward_description;
                    objlist.Add(ob);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return objlist;
        }

        /// <summary>
        /// this method is used drop down list for household details 
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetHouseholdDDL()
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = hr.GethouseholdList();
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.household_name + "-" + item.ward_master.ward_number + "-" + item.ward_master.ward_description + "-" + item.area + "-(Lat: " + item.latitude + ", Long:" + item.longitude + ")";
                    objlist.Add(ob);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return objlist;
        }

        /// <summary>
        /// This method used for get GPS dropdown list
        /// </summary>

        public List<SelectListItem> GetGpsDDL()
        {
            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = gr.GetGpsList();
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.GPS_id;
                    objlist.Add(ob);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return objlist;
        }


        /// <summary>
        /// This method used for get Route dropdown list
        /// </summary>
        public List<SelectListItem> GetRouteDDL()
        {
            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = rr.GetRouteList();
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.route_name;
                    objlist.Add(ob);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return objlist;
        }
    }
}