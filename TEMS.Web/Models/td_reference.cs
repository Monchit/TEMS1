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
    
    public partial class td_reference
    {
        public int id { get; set; }
        public string mold_code { get; set; }
        public double mold_no { get; set; }
        public byte ref_id { get; set; }
        public string ref_no { get; set; }
        public bool active { get; set; }
        public Nullable<byte> prob_id { get; set; }
        public System.DateTime entry_dt { get; set; }
        public string entry_user { get; set; }
    
        public virtual td_mold td_mold { get; set; }
        public virtual tm_problem tm_problem { get; set; }
        public virtual tm_reference tm_reference { get; set; }
    }
}