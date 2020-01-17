using ISWM.WEB.Common.CommonServices;
using ISWM.WEB.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    public class WardKaryakrtaRepository
    {
        GCommon gcm = new GCommon();
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add Ward Karyakrta         
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> AddWardKaryakarta(ward_Karyakrta_master obj)
        {
            int isadd = 0;
            ward_Karyakrta_master updateObj = db.ward_Karyakrta_master.Where(w => w.ward_id == obj.ward_id && w.area_id==w.area_id && w.karyakarta_id==w.karyakarta_id).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.ward_Karyakrta_master.Add(obj);
                db.SaveChanges();

                isadd = 1;
            }
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This method used for update ward Karyakrta details
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> ModifyWardkaryakarta(ward_Karyakrta_master obj)
        {
            bool isupdate = false;
            int isadd = 0;
            ward_Karyakrta_master FindObj = db.ward_Karyakrta_master.Where(w => w.ward_id == obj.ward_id && w.area_id == w.area_id && w.karyakarta_id == w.karyakarta_id).FirstOrDefault();

           
            if (FindObj != null)
            {
                if (FindObj.id == obj.id)
                {
                    isupdate = true;
                }
                else
                {
                    isadd = -1;
                }
            }
            else
            {
                isupdate = true;

            }
            if (isupdate)
            {
                ward_Karyakrta_master updateObj = db.ward_Karyakrta_master.Find(obj.id);
                if (updateObj != null)
                {
                    updateObj.ward_id = obj.ward_id;
                    updateObj.area_id = obj.area_id;
                    updateObj.karyakarta_id = obj.karyakarta_id;
                    updateObj.status = obj.status;
                    updateObj.modified_by = obj.modified_by;
                    updateObj.modified_datetime = obj.modified_datetime;
                    db.ward_Karyakrta_master.Attach(updateObj);
                    db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isadd = 1;
                }
            }
               
            Dispose(true);
            return isadd;

        }

        /// <summary>
        /// This Method Used for delete ward Karyakrta (only status we change)
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> DeleteWardkaryakarta(ward_Karyakrta_master obj)
        {
            int isupdate = 0;
            ward_Karyakrta_master updateObj = db.ward_Karyakrta_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.ward_Karyakrta_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = obj.status;
            }
            //Dispose(true);
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get ward details by using ward Karyakrta id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ward_Karyakrta_master> GetWardkaryakartaByID(int id)
        {
            ward_Karyakrta_master updateObj = db.ward_Karyakrta_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get ward Karyakrta list
        /// </summary>
        /// <returns></returns>
        public async Task<List<ward_Karyakrta_master>> GetWardList()
        {
            List<ward_Karyakrta_master> objlist = db.ward_Karyakrta_master.ToList();
            return objlist;
        }
        /// <summary>
        /// This Method used to get View ward karyakarta list
        /// Coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public async Task<List<WardKarykartaModel>> GetViewWardkaryakartaList(string sort, int userid, int usertypeid)
        {

            List<WardKarykartaModel> objlist = new List<WardKarykartaModel>();
            List<ward_Karyakrta_master> list = db.ward_Karyakrta_master.ToList();
            if (usertypeid != 1)
            {
                if (userid > 0)
                {
                    list = list.Where(w => w.modified_by == userid).ToList();
                }
            }
            if (list.Count > 0)
            {
                if (sort.ToLower() == "desc")
                {
                    list = list.OrderByDescending(o => o.modified_datetime).ToList();
                }
                int i = 1;
                foreach (var item in list)
                {
                    WardKarykartaModel obj = new WardKarykartaModel();
                    obj.srno = i;
                    obj.id = item.id;
                    obj.ward_id = item.ward_id;
                    obj.area_id = item.ward_id;
                    obj.karyakarta_id = item.karyakarta_id;
                    obj.ward = item.ward_master.ward_number;
                    obj.area = item.area_master.area_name;
                    obj.karyakarta = item.user_master.name;
                    obj.contact_no = item.user_master.contact_no;
                    obj.status_id = item.status;
                    obj = gcm.GetStatusDetails(obj);
                    objlist.Add(obj);
                    i++;
                }
            }

            return objlist;

        }
        /// <summary>
        /// This method is used to dispose allocated memory
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
