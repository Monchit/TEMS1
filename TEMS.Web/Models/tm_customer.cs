//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TEMS.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tm_customer
    {
        public tm_customer()
        {
            this.tm_item = new HashSet<tm_item>();
        }
    
        public string cust_id { get; set; }
        public string cust_name { get; set; }
        public string update_user { get; set; }
        public Nullable<System.DateTime> update_dt { get; set; }
        public bool active { get; set; }
    
        public virtual ICollection<tm_item> tm_item { get; set; }
    }
}
