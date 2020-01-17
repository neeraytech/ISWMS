using ISWM.WEB.BusinessServices.Repository;
using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;

namespace ISWM.WEB.AutoScheduler.ISWM_Job
{
    class Job_AddRFIDPunchingData : IJob
    {

        public async void Execute(IJobExecutionContext context)
        {
            try
            {
                AddRFIDPunchingData();
            }
            catch (Exception er)
            {

                //throw;
            }

        }
        public async void AddRFIDPunchingData()
        {
            GCommon gcm = new GCommon();
            if(gcm.CheckForInternetConnection())
            {
                // string starttime = DateTime.Now.AddDays(-6).ToString("dd MMM yyyy hh:mm:ss");

                int addMin =Convert.ToInt32( ConfigurationManager.AppSettings["StartTimeMinDiff"] );
                string starttime = DateTime.Now.AddMinutes(addMin).ToString("dd MMM yyyy hh:mm:ss");              
                string endtime = DateTime.Now.AddDays(1).ToString("dd MMM yyyy")+"";
                string MethodRequestType = "get";
                string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
                string MethodName = ConfigurationManager.AppSettings["RFIDPunchingMethod"]+"?username="+ ConfigurationManager.AppSettings["username_RPM"] + "&accesskey=" + ConfigurationManager.AppSettings["accesskey_RPM"] + "&startdate=" + starttime + "&enddate=" + endtime;
                string BodyParamter = "";

                string str = await gcm.WCFRESTServiceCall(MethodRequestType, MethodName, BodyParamter, BaseUrl);
                if (str.Trim() == "")
                {

                }
                else
                {
                    if (!str.Contains("<body>"))
                    {
                        List<RFIDScannerPunchingModel> ObjList = new List<RFIDScannerPunchingModel>();
                        ObjList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RFIDScannerPunchingModel>>(str);
                        if (ObjList.Count > 0)
                        {
                            RFIDScannerPunchingHistoryRepository rr = new RFIDScannerPunchingHistoryRepository();
                            bool isAdded = await rr.AddRFIDScannerPunchingList(ObjList);
                        }
                    }
                }
            }
        }
    }
}