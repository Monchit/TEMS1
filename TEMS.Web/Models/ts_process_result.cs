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
    
    public partial class ts_process_result
    {
        public string transaction_no { get; set; }
        public string main_job_no { get; set; }
        public string sub_job_no { get; set; }
        public Nullable<int> qty { get; set; }
        public string marking_step { get; set; }
        public string process_code { get; set; }
        public string machine_code { get; set; }
        public string update_pgm { get; set; }
        public string update_date { get; set; }
        public string update_time { get; set; }
        public string status { get; set; }
        public string start_date { get; set; }
        public string start_time { get; set; }
        public string start_user { get; set; }
        public string finished_date { get; set; }
        public string finished_time { get; set; }
        public string finished_user { get; set; }
        public Nullable<int> spent_time { get; set; }
        public string remark { get; set; }
        public string transfer_sign { get; set; }
        public string type_est { get; set; }
    }
}