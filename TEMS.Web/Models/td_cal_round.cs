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
    
    public partial class td_cal_round
    {
        public td_cal_round()
        {
            this.td_cal = new HashSet<td_cal>();
        }
    
        public int round { get; set; }
        public System.DateTime entry_dt { get; set; }
        public bool active { get; set; }
    
        public virtual ICollection<td_cal> td_cal { get; set; }
    }
}