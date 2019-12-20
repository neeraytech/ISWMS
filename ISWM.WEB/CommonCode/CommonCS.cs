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
        UserRepository ur = new UserRepository();
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
    }
}