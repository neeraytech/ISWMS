//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ward_master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ward_master()
        {
            this.household_master = new HashSet<household_master>();
        }
    
        public int id { get; set; }
        public string ward_number { get; set; }
        public string ward_description { get; set; }
        public int karyakarta_id { get; set; }
        public int status { get; set; }
        public int created_by { get; set; }
        public System.DateTime created_datetime { get; set; }
        public int modified_by { get; set; }
        public System.DateTime modified_datetime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<household_master> household_master { get; set; }
        public virtual user_master user_master { get; set; }
    }
}
