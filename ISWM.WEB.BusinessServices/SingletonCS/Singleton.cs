using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.SingletonCS
{
    public sealed class Singleton
    {
        private Singleton()
        {
        }
        private static Singleton instance = null;
        public static Singleton userobject
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string name { get; set; }
        public int user_type_id { get; set; }
        public string user_type { get; set; }
        public List<UserSecurityAccessModel> User_Security_Access_List { get; set; }

        public bool IsModuleAccess(int moduleid )
        {
            bool isFind = false;
            if(User_Security_Access_List!=null)
            {
                if (User_Security_Access_List.Count > 0)
                {
                    var find = User_Security_Access_List.Where(w => w.module_id == moduleid).FirstOrDefault();
                    if (find != null)
                    {
                        isFind = true;
                    }
                }

            }           
           
            return isFind;
        }

        public bool IsModuleActionAccess( int moduleid,int actionid)
        {
            bool isFind = false;
            if (User_Security_Access_List != null)
            {
                if (User_Security_Access_List.Count > 0)
                {

                    var find = User_Security_Access_List.Where(w => w.module_id == moduleid && w.action_id == actionid).FirstOrDefault();
                    if (find != null)
                    {
                        isFind = find.hasAccess;
                    }
                }

            }
            
            return isFind;
        }
    }
}
