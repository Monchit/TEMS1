using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using TEMS.Web.Models;
using WebCommonFunction;

namespace TEMS.Web.Controllers
{
    public class AdminController : Controller
    {
        private TEMS1Entities dbTEMS = new TEMS1Entities();
        private TNC_ADMINEntities dbTNC = new TNC_ADMINEntities();

        
        public ActionResult Users()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UserList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_user;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.emp_code,
                        s.utype_id,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateUser()
        {
            try
            {
                var formData = Request.Form["emp_code"];
                var dbData = dbTEMS.tm_user.Where(w => w.emp_code == formData).FirstOrDefault();
                if (dbData == null)
                {
                    tm_user data = new tm_user();
                    data.emp_code = formData;
                    data.utype_id = byte.Parse(Request.Form["utype_id"]);
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_user.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_user.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateUser()
        {
            try
            {
                var emp_code = Request.Form["emp_code"];
                var data = dbTEMS.tm_user.Find(emp_code);
                data.utype_id = byte.Parse(Request.Form["utype_id"]);
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteUser()
        {
            try
            {
                var emp_code = Request.Form["emp_code"];
                var data = dbTEMS.tm_user.Find(emp_code);
                dbTEMS.tm_user.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult UserType()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UTypeList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_utype;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.utype_id,
                        s.utype_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateUType()
        {
            try
            {
                var param = byte.Parse(Request.Form["utype_id"]);
                var dbData = dbTEMS.tm_utype.Where(w => w.utype_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_utype data = new tm_utype();
                    data.utype_id = param;
                    data.utype_name = Request.Form["utype_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_utype.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_utype.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateUType()
        {
            try
            {
                var param = byte.Parse(Request.Form["utype_id"]);
                var data = dbTEMS.tm_utype.Find(param);
                data.utype_name = Request.Form["utype_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteUType()
        {
            try
            {
                var param = byte.Parse(Request.Form["utype_id"]);
                var data = dbTEMS.tm_utype.Find(param);
                dbTEMS.tm_utype.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult UserLevel()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ULvList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_ulv;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.ulv_id,
                        s.ulv_name,
                        //s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateULv()
        {
            try
            {
                var param = byte.Parse(Request.Form["ulv_id"]);
                var dbData = dbTEMS.tm_ulv.Where(w => w.ulv_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_ulv data = new tm_ulv();
                    data.ulv_id = param;
                    data.ulv_name = Request.Form["ulv_name"];
                    //data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_ulv.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_ulv.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateULv()
        {
            try
            {
                var param = byte.Parse(Request.Form["ulv_id"]);
                var data = dbTEMS.tm_ulv.Find(param);
                data.ulv_name = Request.Form["ulv_name"];
                //data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteULv()
        {
            try
            {
                var param = byte.Parse(Request.Form["ulv_id"]);
                var data = dbTEMS.tm_ulv.Find(param);
                dbTEMS.tm_ulv.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Action()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ActionList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_action;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.act_id,
                        s.act_name,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateAction()
        {
            try
            {
                var param = byte.Parse(Request.Form["act_id"]);
                var dbData = dbTEMS.tm_action.Where(w => w.act_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_action data = new tm_action();
                    data.act_id = param;
                    data.act_name = Request.Form["act_name"];
                    //data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_action.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_action.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateAction()
        {
            try
            {
                var param = byte.Parse(Request.Form["act_id"]);
                var data = dbTEMS.tm_action.Find(param);
                data.act_name = Request.Form["act_name"];
                //data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteAction()
        {
            try
            {
                var param = byte.Parse(Request.Form["act_id"]);
                var data = dbTEMS.tm_action.Find(param);
                dbTEMS.tm_action.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Customer()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CustomerList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_customer;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.cust_id,
                        s.cust_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateCustomer()
        {
            try
            {
                var param = Request.Form["cust_id"];
                var dbData = dbTEMS.tm_customer.Where(w => w.cust_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_customer data = new tm_customer();
                    data.cust_id = param;
                    data.cust_name = Request.Form["cust_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_customer.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_customer.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateCustomer()
        {
            try
            {
                var param = Request.Form["cust_id"];
                var data = dbTEMS.tm_customer.Find(param);
                data.cust_name = Request.Form["cust_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteCustomer()
        {
            try
            {
                var param = Request.Form["cust_id"];
                var data = dbTEMS.tm_customer.Find(param);
                dbTEMS.tm_customer.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Group()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GroupList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_group;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.gtype_id,
                        s.group_id,
                        //s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateGroup()
        {
            try
            {
                var param = int.Parse(Request.Form["group_id"]);
                var dbData = dbTEMS.tm_group.Where(w => w.group_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_group data = new tm_group();
                    data.group_id = param;
                    data.gtype_id = byte.Parse(Request.Form["gtype_id"]);
                    //data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_group.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_group.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateGroup()
        {
            try
            {
                var param = int.Parse(Request.Form["group_id"]);
                var data = dbTEMS.tm_group.Find(param);
                data.gtype_id = byte.Parse(Request.Form["gtype_id"]);
                //data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteGroup()
        {
            try
            {
                var param = int.Parse(Request.Form["group_id"]);
                var data = dbTEMS.tm_group.Find(param);
                dbTEMS.tm_group.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult GroupType()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GTypeList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_gtype;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.gtype_id,
                        s.gtype_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateGType()
        {
            try
            {
                var param = byte.Parse(Request.Form["gtype_id"]);
                var dbData = dbTEMS.tm_gtype.Where(w => w.gtype_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_gtype data = new tm_gtype();
                    data.gtype_id = param;
                    data.gtype_name = Request.Form["gtype_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_gtype.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_gtype.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateGType()
        {
            try
            {
                var param = byte.Parse(Request.Form["gtype_id"]);
                var data = dbTEMS.tm_gtype.Find(param);
                data.gtype_name = Request.Form["gtype_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteGType()
        {
            try
            {
                var param = byte.Parse(Request.Form["gtype_id"]);
                var data = dbTEMS.tm_gtype.Find(param);
                dbTEMS.tm_gtype.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Judgement()
        {
            return View();
        }

        [HttpPost]
        public JsonResult JudgementList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_judgement;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.judge_id,
                        s.judge_name,
                        //s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateJudgement()
        {
            try
            {
                var param = Request.Form["judge_id"];
                var dbData = dbTEMS.tm_judgement.Where(w => w.judge_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_judgement data = new tm_judgement();
                    data.judge_id = param;
                    data.judge_name = Request.Form["judge_name"];
                    //data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_judgement.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_judgement.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateJudgement()
        {
            try
            {
                var param = Request.Form["judge_id"];
                var data = dbTEMS.tm_judgement.Find(param);
                data.judge_name = Request.Form["judge_name"];
                //data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteJudgement()
        {
            try
            {
                var param = Request.Form["judge_id"];
                var data = dbTEMS.tm_judgement.Find(param);
                dbTEMS.tm_judgement.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Location()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LocationList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_location;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.location_id,
                        s.location_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateLocation()
        {
            try
            {
                var param = byte.Parse(Request.Form["location_id"]);
                var dbData = dbTEMS.tm_location.Where(w => w.location_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_location data = new tm_location();
                    data.location_id = param;
                    data.location_name = Request.Form["location_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_location.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_location.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateLocation()
        {
            try
            {
                var param = byte.Parse(Request.Form["location_id"]);
                var data = dbTEMS.tm_location.Find(param);
                data.location_name = Request.Form["location_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteLocation()
        {
            try
            {
                var param = byte.Parse(Request.Form["location_id"]);
                var data = dbTEMS.tm_location.Find(param);
                dbTEMS.tm_location.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Param()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ParamList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_param;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.param_id,
                        s.param_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateParam()
        {
            try
            {
                var param = byte.Parse(Request.Form["param_id"]);
                var dbData = dbTEMS.tm_param.Where(w => w.param_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_param data = new tm_param();
                    data.param_id = param;
                    data.param_name = Request.Form["param_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_param.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_param.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateParam()
        {
            try
            {
                var param = byte.Parse(Request.Form["param_id"]);
                var data = dbTEMS.tm_param.Find(param);
                data.param_name = Request.Form["param_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteParam()
        {
            try
            {
                var param = byte.Parse(Request.Form["param_id"]);
                var data = dbTEMS.tm_param.Find(param);
                dbTEMS.tm_param.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Problem()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ProblemList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_problem;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.prob_id,
                        s.prob_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateProblem()
        {
            try
            {
                var param = byte.Parse(Request.Form["prob_id"]);
                var dbData = dbTEMS.tm_problem.Where(w => w.prob_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_problem data = new tm_problem();
                    //data.prob_id = param;
                    data.prob_name = Request.Form["prob_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_problem.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_problem.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateProblem()
        {
            try
            {
                var param = byte.Parse(Request.Form["prob_id"]);
                var data = dbTEMS.tm_problem.Find(param);
                data.prob_name = Request.Form["prob_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteProblem()
        {
            try
            {
                var param = byte.Parse(Request.Form["prob_id"]);
                var data = dbTEMS.tm_problem.Find(param);
                dbTEMS.tm_problem.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Reference()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RefList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_reference;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.ref_id,
                        s.ref_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateRef()
        {
            try
            {
                var param = byte.Parse(Request.Form["ref_id"]);
                var dbData = dbTEMS.tm_reference.Where(w => w.ref_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_reference data = new tm_reference();
                    //data.ref_id = param;
                    data.ref_name = Request.Form["ref_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_reference.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_reference.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateRef()
        {
            try
            {
                var param = byte.Parse(Request.Form["ref_id"]);
                var data = dbTEMS.tm_reference.Find(param);
                data.ref_name = Request.Form["ref_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteRef()
        {
            try
            {
                var param = byte.Parse(Request.Form["ref_id"]);
                var data = dbTEMS.tm_reference.Find(param);
                dbTEMS.tm_reference.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Status()
        {
            return View();
        }

        [HttpPost]
        public JsonResult StatusList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_status;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.status_id,
                        s.status_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateStatus()
        {
            try
            {
                var param = int.Parse(Request.Form["status_id"]);
                var dbData = dbTEMS.tm_status.Where(w => w.status_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_status data = new tm_status();
                    data.status_id = param;
                    data.status_name = Request.Form["status_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_status.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_status.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateStatus()
        {
            try
            {
                var param = int.Parse(Request.Form["status_id"]);
                var data = dbTEMS.tm_status.Find(param);
                data.status_name = Request.Form["status_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteStatus()
        {
            try
            {
                var param = int.Parse(Request.Form["status_id"]);
                var data = dbTEMS.tm_status.Find(param);
                dbTEMS.tm_status.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Plant()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PlantList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_plant;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.plant_id,
                        s.plant_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreatePlant()
        {
            try
            {
                var param = byte.Parse(Request.Form["plant_id"]);
                var dbData = dbTEMS.tm_plant.Where(w => w.plant_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_plant data = new tm_plant();
                    //data.plant_id = param;
                    data.plant_name = Request.Form["plant_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_plant.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_plant.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdatePlant()
        {
            try
            {
                var param = byte.Parse(Request.Form["plant_id"]);
                var data = dbTEMS.tm_plant.Find(param);
                data.plant_name = Request.Form["plant_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeletePlant()
        {
            try
            {
                var param = byte.Parse(Request.Form["plant_id"]);
                var data = dbTEMS.tm_plant.Find(param);
                dbTEMS.tm_plant.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Product()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ProductList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_product;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.prod_id,
                        s.prod_name,
                        s.plant_id,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateProduct()
        {
            try
            {
                var param = Request.Form["prod_id"];
                var dbData = dbTEMS.tm_product.Where(w => w.prod_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_product data = new tm_product();
                    data.prod_id = param;
                    data.prod_name = Request.Form["prod_name"];
                    data.plant_id = byte.Parse(Request.Form["plant_id"]);
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_product.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_product.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateProduct()
        {
            try
            {
                var param = Request.Form["prod_id"];
                var data = dbTEMS.tm_product.Find(param);
                data.prod_name = Request.Form["prod_name"];
                data.plant_id = byte.Parse(Request.Form["plant_id"]);
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteProduct()
        {
            try
            {
                var param = Request.Form["prod_id"];
                var data = dbTEMS.tm_product.Find(param);
                dbTEMS.tm_product.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult WC()
        {
            return View();
        }

        [HttpPost]
        public JsonResult WCList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_wc;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.wc_id,
                        s.wc_name,
                        s.prod_id,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateWC()
        {
            try
            {
                var param = Request.Form["wc_id"];
                var dbData = dbTEMS.tm_wc.Where(w => w.prod_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_wc data = new tm_wc();
                    data.wc_id = param;
                    data.wc_name = Request.Form["wc_name"];
                    data.prod_id = Request.Form["prod_id"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_wc.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_wc.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateWC()
        {
            try
            {
                var param = Request.Form["wc_id"];
                var data = dbTEMS.tm_wc.Find(param);
                data.wc_name = Request.Form["wc_name"];
                data.prod_id = Request.Form["prod_id"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteWC()
        {
            try
            {
                var param = Request.Form["wc_id"];
                var data = dbTEMS.tm_wc.Find(param);
                dbTEMS.tm_wc.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Item()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ItemList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_item;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.item_code,
                        s.cust_id,
                        s.wc_id,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateItem()
        {
            try
            {
                var param = Request.Form["item_code"];
                var dbData = dbTEMS.tm_item.Where(w => w.item_code == param).FirstOrDefault();
                if (dbData == null)
                {
                    tm_item data = new tm_item();
                    data.item_code = param;
                    data.cust_id = Request.Form["cust_id"];
                    data.wc_id = Request.Form["wc_id"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_item.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_item.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateItem()
        {
            try
            {
                var param = Request.Form["item_code"];
                var data = dbTEMS.tm_item.Find(param);
                data.cust_id = Request.Form["cust_id"];
                data.wc_id = Request.Form["wc_id"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteItem()
        {
            try
            {
                var param = Request.Form["item_code"];
                var data = dbTEMS.tm_item.Find(param);
                dbTEMS.tm_item.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public void ItemExport()
        {
            TNCUtility util = new TNCUtility();
            var query = from a in dbTEMS.tm_item
                        select a;

            var output = query.ToList()
                    .Select(s => new
                    {
                        ItemCode = s.item_code,
                        Need = "",
                        Circulate = "",
                        Spare = ""
                    });

            util.CreateExcel(output.ToList(), "Item_export");
        }

        public ActionResult ItemManualCal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCal(HttpPostedFileBase file_cal)
        {
            try
            {
                TNCFileDirectory dir = new TNCFileDirectory();
                TNCUtility util = new TNCUtility();
                var savedDir = dir.SaveFile(file_cal, "Temp/" + DateTime.Now.ToString("yyyyMM", new CultureInfo("en-US")));
                var reader = util.ReadExcel(savedDir);

                int cal_round = AddCalRound();

                foreach (var item in reader)
                {
                    td_cal cal = new td_cal();
                    cal.round = cal_round;
                    cal.item_code = item[0].ToString();
                    cal.need = int.Parse(item[1].ToString());
                    cal.circulate = int.Parse(item[2].ToString());
                    cal.spare = int.Parse(item[3].ToString());
                    dbTEMS.td_cal.Add(cal);
                }

                dbTEMS.SaveChanges();

                return RedirectToAction("ItemManualCal", "Admin");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int AddCalRound()
        {
            using (var local = new TEMS1Entities())
            {
                var last_round = local.td_cal_round.OrderByDescending(o => o.round).Select(s => s.round).First();

                td_cal_round round = new td_cal_round();
                round.entry_dt = DateTime.Now;
                round.active = true;
                local.td_cal_round.Add(round);

                var upd_prev_round = local.td_cal_round.Find(last_round);
                upd_prev_round.active = false;

                local.SaveChanges();

                return round.round;
            }
        }

        //-----------------------------------//

        public ActionResult Unit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UnitList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.tm_unit;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.unit_id,
                        s.unit_name,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateUnit()
        {
            try
            {
                //var param = byte.Parse(Request.Form["unit_id"]);
                //var dbData = dbTEMS.tm_unit.Where(w => w.unit_id == param).FirstOrDefault();
                //if (dbData == null)
                //{
                    tm_unit data = new tm_unit();
                    //data.unit_id = param;
                    data.unit_name = Request.Form["unit_name"];
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.tm_unit.Add(data);
                //}

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.tm_unit.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateUnit()
        {
            try
            {
                var param = byte.Parse(Request.Form["unit_id"]);
                var data = dbTEMS.tm_unit.Find(param);
                data.unit_name = Request.Form["unit_name"];
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteUnit()
        {
            try
            {
                var param = byte.Parse(Request.Form["unit_id"]);
                var data = dbTEMS.tm_unit.Find(param);
                dbTEMS.tm_unit.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult Cal()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CalList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.td_cal.Where(w => w.td_cal_round.active == true);

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.round,
                        s.item_code,
                        s.need,
                        s.circulate,
                        s.spare
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateCal()
        {
            try
            {
                var get_round = dbTEMS.td_cal_round.Where(w => w.active == true).Select(s => s.round).First();
                var param = Request.Form["item_code"];
                var data = dbTEMS.td_cal.Find(get_round, param);
                data.need = int.Parse(Request.Form["need"]);
                data.circulate = int.Parse(Request.Form["circulate"]);
                data.spare = int.Parse(Request.Form["spare"]);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult CalRound()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CalRoundList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.td_cal_round;

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.round,
                        s.entry_dt,
                        s.active
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateCalRound()
        {
            try
            {
                var param = int.Parse(Request.Form["round"]);
                var data = dbTEMS.td_cal_round.Find(param);
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult SetParamA()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ParamAList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var query = dbTEMS.td_param.Where(w => w.param_id < 5);

                //Get data from database
                int TotalRecord = query.Count();

                // Paging
                var output = query
                    .Select(s => new
                    {
                        s.item_code,
                        s.param_id,
                        s.value,
                        s.active,
                        s.update_dt
                    }).OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize);

                //Return result to jTable
                return Json(new { Result = "OK", Records = output, TotalRecordCount = TotalRecord });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateParamA()
        {
            try
            {
                var item = Request.Form["item_code"];
                var param = byte.Parse(Request.Form["param_id"]);
                var dbData = dbTEMS.td_param.Where(w => w.item_code == item && w.param_id == param).FirstOrDefault();
                if (dbData == null)
                {
                    td_param data = new td_param();
                    data.item_code = item;
                    data.param_id = param;
                    data.value = double.Parse(Request.Form["value"]);
                    data.active = true;
                    data.update_user = Session["TEMS_Auth"].ToString();
                    data.update_dt = DateTime.Now;

                    dbTEMS.td_param.Add(data);
                }

                dbTEMS.SaveChanges();

                return Json(new { Result = "OK", Record = dbTEMS.td_param.OrderByDescending(o => o.update_dt).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateParamA()
        {
            try
            {
                var item = Request.Form["item_code"];
                var param = byte.Parse(Request.Form["param_id"]);
                var data = dbTEMS.td_param.Find(item,param);
                data.value = double.Parse(Request.Form["value"]);
                data.active = Request.Form["active"] != null ? bool.Parse(Request.Form["active"]) : false;
                data.update_user = Session["TEMS_Auth"].ToString();
                data.update_dt = DateTime.Now;
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteParamA()
        {
            try
            {
                var item = Request.Form["item_code"];
                var param = byte.Parse(Request.Form["param_id"]);
                var data = dbTEMS.td_param.Find(item, param);
                dbTEMS.td_param.Remove(data);
                dbTEMS.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //-----------------------------------//

        public ActionResult SetParamB()
        {
            return View();
        }

        //-----------------------------------//

        public ActionResult SetParamC()
        {
            return View();
        }

        //-----------------------------------//

        [HttpPost]
        public JsonResult GetTNCUserList()
        {
            try
            {
                var result = dbTNC.tnc_user.Where(w => w.emp_status == "A").OrderBy(o => o.emp_fname).ThenBy(o => o.emp_lname)
                    .Select(r => new { DisplayText = r.emp_fname + " " + r.emp_lname, Value = r.emp_code });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetUTypeList()
        {
            try
            {
                var result = dbTEMS.tm_utype
                    .Select(r => new { DisplayText = r.utype_name, Value = r.utype_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetTNCGroupList()
        {
            try
            {
                var result = dbTNC.tnc_group_master.OrderBy(o => o.group_name)
                    .Select(r => new { DisplayText = r.group_name, Value = r.id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetGTypeList()
        {
            try
            {
                var result = dbTEMS.tm_gtype
                    .Select(r => new { DisplayText = r.gtype_name, Value = r.gtype_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetPlantList()
        {
            try
            {
                var result = dbTEMS.tm_plant
                    .Select(r => new { DisplayText = r.plant_name, Value = r.plant_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetProductList()
        {
            try
            {
                var result = dbTEMS.tm_product
                    .Select(r => new { DisplayText = r.prod_id + " - " + r.prod_name, Value = r.prod_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetWCList()
        {
            try
            {
                var result = dbTEMS.tm_wc
                    .Select(r => new { DisplayText = r.wc_id + " - " + r.wc_name, Value = r.wc_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetItemList()
        {
            try
            {
                var result = dbTEMS.tm_item
                    .Select(r => new { DisplayText = r.item_code, Value = r.item_code });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetCustomerList()
        {
            try
            {
                var result = dbTEMS.tm_customer
                    .Select(r => new { DisplayText = r.cust_id + " - " + r.cust_name, Value = r.cust_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetParamList()
        {
            try
            {
                var result = dbTEMS.tm_param
                    .Select(r => new { DisplayText = r.param_name, Value = r.param_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetParamAList()
        {
            try
            {
                var result = dbTEMS.tm_param.Where(w => w.param_id < 5)
                    .Select(r => new { DisplayText = r.param_name, Value = r.param_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetParamBList()
        {
            try
            {
                var result = dbTEMS.tm_param.Where(w => w.param_id > 4 && w.param_id < 7)
                    .Select(r => new { DisplayText = r.param_name, Value = r.param_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetParamCList()
        {
            try
            {
                var result = dbTEMS.tm_param.Where(w => w.param_id > 6 && w.param_id < 10)
                    .Select(r => new { DisplayText = r.param_name, Value = r.param_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetActionList()
        {
            try
            {
                var result = dbTEMS.tm_action
                    .Select(r => new { DisplayText = r.act_name, Value = r.act_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetRefList()
        {
            try
            {
                var result = dbTEMS.tm_reference
                    .Select(r => new { DisplayText = r.ref_name, Value = r.ref_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetUnitList()
        {
            try
            {
                var result = dbTEMS.tm_unit
                    .Select(r => new { DisplayText = r.unit_name, Value = r.unit_id });
                return Json(new { Result = "OK", Options = result });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}
