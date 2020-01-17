using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ISWM.WEB.AutoScheduler.ISWM_Job
{
    class Job_AddTruckCurrentLocation : IJob
    {
       
        public async void Execute(IJobExecutionContext context)
        {
            try
            {
                AddTruckCurrentLocation();
            }
            catch (Exception er)
            {

                //throw;
            }
           
        }
        public async void AddTruckCurrentLocation()
        {
            GCommon gcm = new GCommon();
            if (gcm.CheckForInternetConnection())
            {
                string MethodRequestType = "get";
                string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
                string MethodName = ConfigurationManager.AppSettings["TruckTrackingMethod"] + "?username=" + ConfigurationManager.AppSettings["username_TTM"] + "&accesskey=" + ConfigurationManager.AppSettings["accesskey_TTM"];
                string BodyParamter = "";
               
                string str = await gcm.WCFRESTServiceCall(MethodRequestType, MethodName, BodyParamter, BaseUrl);
                if (str.Trim() == ""){ }
                else
                {
                    List<GPS_API_Module> ObjList = new List<GPS_API_Module>();
                    ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GPS_API_Module>>(str);
                    if (ObjList.Count > 0)
                    {
                        TruckTrackingHistoryRepository tr = new TruckTrackingHistoryRepository();
                        bool isAdded = await tr.AddTruckTrackingHistoryList(ObjList);
                    }

                }
            }
        }
    }
}
