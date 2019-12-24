﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISWM.WEB.BusinessServices.Repository
{
    /// <summary>
    /// Repository is for Household Master
    /// coder: Smruti Wagh
    /// </summary>
    public class HouseholdRepository
    {
        private ISWM_BASE_DBEntities db = new ISWM_BASE_DBEntities();

        /// <summary>
        /// This Method used to add household   
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Addhousehold(household_master obj)
        {
            int isadd = 0;
            household_master updateObj = db.household_master.Where(w => w.latitude == obj.latitude && w.longitude == obj.longitude).FirstOrDefault();
            if (updateObj != null)
            {
                isadd = -1;
            }
            else
            {
                db.household_master.Add(obj);
                db.SaveChanges();
                isadd = 1;
            }
            db.Dispose();
            return isadd;

        }

        /// <summary>
        /// This method used for update household details
        ///  coder:Smruti Wagh
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Modifyhousehold(household_master obj)
        {
            bool isupdate = false;
            household_master updateObj = db.household_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.address1 = obj.address1;
                updateObj.address2 = obj.address2;
                updateObj.area = obj.area;
                updateObj.pin_code = obj.pin_code;
                updateObj.city = obj.city;
                updateObj.state = obj.state;
                updateObj.latitude = obj.latitude;
                updateObj.longitude = obj.longitude;
                updateObj.household_name = obj.household_name;
                updateObj.email = obj.email;
                updateObj.contact_no = obj.contact_no;
                updateObj.ward_id = obj.ward_id;
                updateObj.valid_from = obj.valid_from;
                updateObj.valid_to = obj.valid_to;
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.household_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used for delete household (only status we change)
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Deletehousehold(household_master obj)
        {
            bool isupdate = false;
            household_master updateObj = db.household_master.Find(obj.id);
            if (updateObj != null)
            {
                updateObj.status = obj.status;
                updateObj.modified_by = obj.modified_by;
                updateObj.modified_datetime = obj.modified_datetime;
                db.household_master.Attach(updateObj);
                db.Entry(updateObj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isupdate = true;
            }
            db.Dispose();
            return isupdate;

        }

        /// <summary>
        /// This Method Used to get ward details by using household id
        ///  coder:Smruti Wagh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public household_master GethouseholdByID(int id)
        {
            household_master updateObj = db.household_master.Find(id);
            return updateObj;

        }

        /// <summary>
        /// This Method used to get household list
        ///  coder:Smruti Wagh
        /// </summary>
        /// <returns></returns>
        public List<household_master> GethouseholdList()
        {
            List<household_master> objlist = db.household_master.ToList();
            return objlist;
        }
        /// <summary>
        /// this method is used to deallocate used memory
        /// coder: Smruti Wagh
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
