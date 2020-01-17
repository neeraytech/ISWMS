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

        /// <summary>
        /// This method used to check model access,if have access return true else return false
        /// </summary>
        /// <param name="moduleid"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  This method used to check model action access,if have access return true else return false
        ///  
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="actionid"></param>
        /// <returns></returns>
        public int IsModuleActionAccess( int moduleid,int actionid)
        {
            int isFind = 0;
            if (User_Security_Access_List != null)
            {
                if (User_Security_Access_List.Count > 0)
                {

                    var find = User_Security_Access_List.Where(w => w.module_id == moduleid && w.action_id == actionid).FirstOrDefault();
                    if (find != null)
                    {
                        isFind = find.status_id;
                    }
                }

            }
            
            return isFind;
        }
    }
}
