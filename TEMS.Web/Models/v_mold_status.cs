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
    
    public partial class v_mold_status
    {
        public byte plant_id { get; set; }
        public string plant_name { get; set; }
        public string prod_id { get; set; }
        public string prod_name { get; set; }
        public string wc_id { get; set; }
        public string wc_name { get; set; }
        public string item_code { get; set; }
        public Nullable<int> OK_Mold { get; set; }
        public Nullable<int> OK_Prod { get; set; }
        public Nullable<int> NG_Mold { get; set; }
        public Nullable<int> NG_Prod { get; set; }
        public Nullable<int> NG_TnD { get; set; }
        public Nullable<int> NG_Nok { get; set; }
        public Nullable<int> Total { get; set; }
        public int need { get; set; }
        public int circulate { get; set; }
        public int spare { get; set; }
    }
}
