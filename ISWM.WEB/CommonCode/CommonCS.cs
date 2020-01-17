using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
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
        AreaRepository ar = new AreaRepository();
        DriverRepository dr = new DriverRepository();
        TruckRepository tr = new TruckRepository();
        RFIDScannerRepository rsr = new RFIDScannerRepository();
        ActionRepository acr = new ActionRepository();
        public async Task<List<SelectListItem>> GetUserTypeDDL(int? statusid)
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await ur.GetUserType(statusid);
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

        public async Task<List<SelectListItem>> GetUserDDL(int? statusid)
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await ur.GetUserListByType(statusid);
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.user_id.ToString();
                    ob.Text = item.name + "-" + item.area_master.area_name + "-" + item.contact_no;
                    objlist.Add(ob);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //throw;
            }

            return objlist;
        }

        /// <summary>
        /// this method is use to drop down for status
        /// </summary>
        /// <returns></returns>.
        public async Task<List<SelectListItem>> GetStatusDDL()
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
        public async Task<List<SelectListItem>> GetModuleDDL(int? statusid)
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await mr.GetModuleList(statusid);
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
        public async Task<List<SelectListItem>> GetWardDDL(int? statusid)
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await wr.GetWardList(statusid);
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.ward_number;
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

        public async Task<List<SelectListItem>> GetAreaDDL(int? statusid)
        {
            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await ar.GetAreaList(statusid);
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.area_name;
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
        public async Task<List<SelectListItem>> GetHouseholdDDL(int? statusid)
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl =await hr.GethouseholdList(statusid);
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.household_name + "-" + item.ward_master.ward_number + "-" + item.area_master.area_name + "-(Lat: " + item.latitude + ", Long:" + item.longitude + ")";
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

        public async Task<List<SelectListItem>> GetGpsDDL(int? statusid)
        {
            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl =await gr.GetGpsList(statusid);
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
        public async Task<List<SelectListItem>> GetRouteDDL(int? statusid)
        {
            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await rr.GetRouteList(statusid);
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

        /// <summary>
        /// This method used for get Driver dropdown list
        /// coder:Smruti Wagh
        /// </summary>
        public async Task<List<SelectListItem>> GetDriverDDL(int? statusid)
        {
            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await dr.GetDriverList(statusid);
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.name + "-" + item.contact_no;
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
        /// This method used for get Truck dropdown list
        /// coder:Smruti Wagh
        /// </summary>
        public async Task<List<SelectListItem>> GetTruckDDL(int? statusid)
        {
            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await tr.GetTruckList(statusid);
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.truck_no;
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
        /// This method used for get Scanner dropdown list
        /// coder:Smruti Wagh
        /// </summary>
        public async Task<List<SelectListItem>> GetScannerDDL(int? statusid)
        {
            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await rsr.GetRFIDScannerList(statusid);
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.id.ToString();
                    ob.Text = item.scanner_id;
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
        /// this method is used to drop down for action 
        /// coder: Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectListItem>> GetActionDDL(int? statusid)
        {

            List<SelectListItem> objlist = new List<SelectListItem>();
            try
            {
                var obl = await acr.GetActionList(statusid);
                foreach (var item in obl)
                {
                    SelectListItem ob = new SelectListItem();
                    ob.Value = item.module_action_id.ToString();
                    ob.Text = item.module_action_name;
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