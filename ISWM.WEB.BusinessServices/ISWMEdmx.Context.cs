﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ISWM.WEB.BusinessServices
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ISWM_BASE_DBEntities : DbContext
    {
        public ISWM_BASE_DBEntities()
            : base("name=ISWM_BASE_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<actions_master> actions_master { get; set; }
        public virtual DbSet<area_master> area_master { get; set; }
        public virtual DbSet<driver_master> driver_master { get; set; }
        public virtual DbSet<GPS_master> GPS_master { get; set; }
        public virtual DbSet<GPS_tracking_history> GPS_tracking_history { get; set; }
        public virtual DbSet<household_master> household_master { get; set; }
        public virtual DbSet<ISWMS_details> ISWMS_details { get; set; }
        public virtual DbSet<ISWMS_master> ISWMS_master { get; set; }
        public virtual DbSet<module_master> module_master { get; set; }
        public virtual DbSet<RFID_household_details> RFID_household_details { get; set; }
        public virtual DbSet<RFID_scanner_master> RFID_scanner_master { get; set; }
        public virtual DbSet<route_household_details> route_household_details { get; set; }
        public virtual DbSet<route_master> route_master { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<truck_master> truck_master { get; set; }
        public virtual DbSet<user_master> user_master { get; set; }
        public virtual DbSet<user_security_access_details> user_security_access_details { get; set; }
        public virtual DbSet<userType_master> userType_master { get; set; }
        public virtual DbSet<userType_modules> userType_modules { get; set; }
        public virtual DbSet<ward_Karyakrta_master> ward_Karyakrta_master { get; set; }
        public virtual DbSet<ward_master> ward_master { get; set; }
    }
}
