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
    
    public partial class tm_wc
    {
        public tm_wc()
        {
            this.tm_item = new HashSet<tm_item>();
        }
    
        public string wc_id { get; set; }
        public string wc_name { get; set; }
        public string prod_id { get; set; }
        public string update_user { get; set; }
        public System.DateTime update_dt { get; set; }
        public bool active { get; set; }
    
        public virtual ICollection<tm_item> tm_item { get; set; }
        public virtual tm_product tm_product { get; set; }
    }
}
