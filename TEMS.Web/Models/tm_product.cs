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
    
    public partial class tm_product
    {
        public tm_product()
        {
            this.tm_wc = new HashSet<tm_wc>();
        }
    
        public string prod_id { get; set; }
        public string prod_name { get; set; }
        public byte plant_id { get; set; }
        public string update_user { get; set; }
        public System.DateTime update_dt { get; set; }
        public bool active { get; set; }
    
        public virtual tm_plant tm_plant { get; set; }
        public virtual ICollection<tm_wc> tm_wc { get; set; }
    }
}
