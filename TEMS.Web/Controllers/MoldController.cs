using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TEMS.Web.Models;
using WebCommonFunction;

namespace TEMS.Web.Controllers
{
    public class MoldController : Controller
    {
        private TEMS1Entities dbTEMS = new TEMS1Entities();
        private TNC_ADMINEntities dbTNC = new TNC_ADMINEntities();

        [Chk_Auth]
        public ActionResult Index()
        {
            ViewBag.SelectPlant = dbTEMS.tm_plant;
            return View();
        }

        [Chk_Auth]
        public ActionResult Dashboard()
        {
            return View();
        }

        [Chk_Auth]
        public ActionResult MoldStatus()
        {
            ViewBag.SelectPlant = dbTEMS.tm_plant;
            return View();
        }

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult MoldStatusPivot(byte plant, string product, string wc)
        {
            var query = dbTEMS.td_mold.Where(w => w.tm_item.tm_wc.tm_product.tm_plant.plant_id == plant);

            if (product != "")
            {
                query = query.Where(w => w.tm_item.tm_wc.tm_product.prod_id == product);
            }

            if (wc != "")
            {
                query = query.Where(w => w.tm_item.tm_wc.wc_id == wc);
            }

            //var query = (from a in dbTEMS.td_mold
            //             where a.tm_item.tm_wc.tm_product.tm_plant.plant_id == plant && a.tm_item.tm_wc.tm_product.prod_id == product
            //             select new {
            //                 a.item_code,
            //                 a.mold_code,
            //                 a.mold_no
            //             }).ToArray();
            return Json(query.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult _MoldStatusPivot(byte plant, string product = "", string wc = "")
        {
            var sql = dbTEMS.v_mold_status.Where(w => w.plant_id == plant);

            if (product != "") sql = sql.Where(w => w.prod_id == product);
            if (wc != "") sql = sql.Where(w => w.wc_id == wc);

            return PartialView(sql);
        }

        public void ExportMoldStatus(byte sel_plant, string sel_product = "", string sel_wc = "")
        {
            TNCUtility util = new TNCUtility();

            var sql = dbTEMS.v_mold_status.Where(w => w.plant_id == sel_plant);
            if (sel_product != "") sql = sql.Where(w => w.prod_id == sel_product);
            if (sel_wc != "") sql = sql.Where(w => w.wc_id == sel_wc);

            var output = sql.ToList()
                    .Select(s => new
                    {
                        ItemCode = s.item_code,
                        Need = s.need,
                        Circulate = s.circulate,
                        Spare = s.spare,
                        Total = s.Total,
                        s.OK_Mold,
                        s.OK_Prod,
                        s.NG_Mold,
                        s.NG_Prod,
                        s.NG_TnD,
                        s.NG_Nok
                    });

            util.CreateExcel(output.ToList(), "Item_export");
        }

        [Chk_Auth]
        public ActionResult Mold_Item(string id)
        {
            var query = dbTEMS.td_mold.Where(w => w.item_code == id);
            return View(query);
        }

        [Chk_Auth]
        public ActionResult Mold_OK(string id, int stt, byte loc)//id = item_code
        {
            var get_loc = dbTEMS.tm_location.Find(loc);
            ViewBag.Title = "Mold OK at " + get_loc.location_name;
            var query = dbTEMS.td_mold.Where(w => w.item_code == id && w.status_id == stt && w.location_id == loc);
            return View(query);
        }

        [Chk_Auth]
        public ActionResult Mold_NG(string id, int stt, byte loc)//id = item_code
        {
            var get_loc = dbTEMS.tm_location.Find(loc);
            ViewBag.Title = "Mold NG at " + get_loc.location_name;
            var query = dbTEMS.td_mold.Where(w => w.item_code == id && w.status_id == stt && w.location_id == loc);
            if (loc == 10)
            {
                ViewBag.NoWord = "No PR no. (NOK) or W/O (T&D)";//Mold Control
            }
            else if (loc == 20)
            {
                ViewBag.NoWord = "No SA no.";//Production
            }
            else if (loc == 30)
            {
                ViewBag.NoWord = "No W/O (T&D)";//Tool&Die
            }
            else if (loc == 40)
            {
                ViewBag.NoWord = "No PR no. (NOK)";//NOK
            }
            else
            {
                ViewBag.NoWord = "-";
            }
            return View(query);
        }

        [Chk_Auth]
        public ActionResult NG_MC(string id, int stt, byte loc)//id = item_code
        {
            var query = dbTEMS.td_mold.Where(w => w.item_code == id && w.status_id == stt && w.location_id == loc);
            return View(query);
        }

        [Chk_Auth]
        public ActionResult NG_PD(string id, int stt, byte loc)//id = item_code
        {
            var query = dbTEMS.td_mold.Where(w => w.item_code == id && w.status_id == stt && w.location_id == loc);
            return View(query);
        }

        [Chk_Auth]
        public ActionResult NG_TnD(string id, int stt, byte loc)//id = item_code
        {
            var query = dbTEMS.td_mold.Where(w => w.item_code == id && w.status_id == stt && w.location_id == loc);
            return View(query);
        }

        [Chk_Auth]
        public ActionResult NG_Nok(string id, int stt, byte loc)//id = item_code
        {
            var query = dbTEMS.td_mold.Where(w => w.item_code == id && w.status_id == stt && w.location_id == loc);
            return View(query);
        }

        [Chk_Auth]
        public ActionResult Detail(string id, double no)//id=mold_code
        {
            var query = (from a in dbTEMS.td_tran
                         where a.mold_code == id && a.mold_no == no
                         select a).Take(100);
            ViewBag.MoldCode = id;
            ViewBag.MoldNo = no;
            return View(query);
        }

        [Chk_Auth]
        public ActionResult Manage(string id, double no)//id=mold_code
        {
            var query = dbTEMS.td_mold.Find(id, no);
            ViewBag.Ref = dbTEMS.v_ref.Where(w => w.mold_code == id && w.mold_no == no).OrderByDescending(o => o.entry_dt).Take(5);
            ViewBag.Problem = dbTEMS.tm_problem.Where(w => w.active == true);
            return View(query);
        }

        [Chk_Auth]
        public ActionResult Receive()
        {
            ViewBag.Status = dbTEMS.tm_status.Where(w => w.active == true);
            ViewBag.Unit = dbTEMS.tm_unit.Where(w => w.active == true);
            ViewBag.Location = dbTEMS.tm_location.Where(w => w.active == true);
            return View();
        }

        [Chk_Auth]
        public ActionResult ReceiveMore()
        {
            ViewBag.Status = dbTEMS.tm_status.Where(w => w.active == true);
            ViewBag.Unit = dbTEMS.tm_unit.Where(w => w.active == true);
            ViewBag.Location = dbTEMS.tm_location.Where(w => w.active == true);
            return View();
        }

        [HttpPost]
        public ActionResult CreateMold()
        {
            try
            {
                var mold_code = Request.Form["mold_code"];
                var mold_no = int.Parse(Request.Form["mold_no"]);

                var mold_exist = dbTEMS.td_mold.Any(w => w.mold_code == mold_code && w.mold_no == mold_no);

                if (!mold_exist)
                {
                    td_mold newmold = new td_mold();
                    newmold.mold_code = mold_code;
                    newmold.mold_no = mold_no;
                    newmold.item_code = Request.Form["item_code"];
                    newmold.status_id = int.Parse(Request.Form["status"]);
                    newmold.location_id = byte.Parse(Request.Form["location"]);
                    newmold.cav_per_unit = int.Parse(Request.Form["cav_per_unit"]);
                    newmold.unit_id = byte.Parse(Request.Form["unit"]);

                    dbTEMS.td_mold.Add(newmold);

                    var ulv = byte.Parse(Session["TEMS_ULv"].ToString());
                    var org = int.Parse(Session["TEMS_Org"].ToString());
                    var actor = Session["TEMS_Auth"].ToString();

                    ManageTran(mold_code, mold_no, 0, ulv, org, 1, "-", actor);

                    dbTEMS.SaveChanges();

                    TempData["alert-success"] = "New Mold received successfully.";
                }
                else
                {
                    TempData["alert-danger"] = "This mold already exist.";
                }
            }
            catch (Exception ex)
            {
                TempData["alert-danger"] = "Error on receive mold -> " + ex;
            }
            return RedirectToAction("Receive", "Mold");
        }

        [HttpPost]
        public ActionResult UpdateMold()
        {
            var mold_code = Request.Form["mold_code"];
            var mold_no = int.Parse(Request.Form["mold_no"]);

            try
            {
                var mold_exist = dbTEMS.td_mold.Find(mold_code, mold_no);

                if (mold_exist != null)
                {
                    mold_exist.item_code = Request.Form["item_code"];
                    mold_exist.status_id = int.Parse(Request.Form["status"]);
                    mold_exist.location_id = byte.Parse(Request.Form["location"]);
                    mold_exist.cav_per_unit = int.Parse(Request.Form["cav_per_unit"]);
                    mold_exist.unit_id = byte.Parse(Request.Form["unit"]);

                    dbTEMS.SaveChanges();

                    TempData["alert-success"] = "Update mold successfully.";
                    return RedirectToAction("Edit", "Mold", new { id = mold_code, no = mold_no });
                }
                else
                {
                    TempData["alert-danger"] = "This mold not in system";
                    return RedirectToAction("Edit", "Mold", new { id = mold_code, no = mold_no });
                }
            }
            catch (Exception ex)
            {
                TempData["alert-danger"] = "Error on update mold -> " + ex;
                return RedirectToAction("Edit", "Mold", new { id = mold_code, no = mold_no });
            }
        }

        [Chk_Auth]
        public ActionResult Edit(string id, double no)//id=mold_code
        {
            var query = dbTEMS.td_mold.Find(id, no);
            ViewBag.Status = dbTEMS.tm_status.Where(w => w.active == true);
            ViewBag.Unit = dbTEMS.tm_unit.Where(w => w.active == true);
            ViewBag.Location = dbTEMS.tm_location.Where(w => w.active == true);
            return View(query);
        }

        [HttpPost]
        public ActionResult CreateTnD()
        {
            var mold_code = Request.Form["tnd_mold"];
            var mold_no = double.Parse(Request.Form["tnd_no"]);

            try
            {
                td_reference tdr = new td_reference();
                tdr.mold_code = mold_code;
                tdr.mold_no = mold_no;
                tdr.ref_id = 2;//2 = WO T&D
                tdr.ref_no = Request.Form["woref"];
                tdr.active = true;
                tdr.prob_id = byte.Parse(Request.Form["tnd_prob"]);
                tdr.entry_dt = DateTime.Now;
                tdr.entry_user = Session["TEMS_Auth"].ToString();
                dbTEMS.td_reference.Add(tdr);
                dbTEMS.SaveChanges();

                UpdateMoldStatus(mold_code, mold_no);

                TempData["alert-success"] = "Successfully.";
            }
            catch (Exception ex)
            {
                TempData["alert-danger"] = "Error -> " + ex;
            }
            return RedirectToAction("Manage", "Mold", new { id = mold_code, no = mold_no });
        }

        [HttpPost]
        public ActionResult CreateNok()
        {
            var mold_code = Request.Form["nok_mold"];
            var mold_no = double.Parse(Request.Form["nok_no"]);

            try
            {
                td_reference tdr = new td_reference();
                tdr.mold_code = mold_code;
                tdr.mold_no = mold_no;
                tdr.ref_id = 3;//3 = PR NOK
                tdr.ref_no = Request.Form["prno"];
                tdr.active = true;
                tdr.prob_id = byte.Parse(Request.Form["nok_prob"]);
                tdr.entry_dt = DateTime.Now;
                tdr.entry_user = Session["TEMS_Auth"].ToString();
                dbTEMS.td_reference.Add(tdr);
                dbTEMS.SaveChanges();

                UpdateMoldStatus(mold_code, mold_no);

                TempData["alert-success"] = "Successfully.";
            }
            catch (Exception ex)
            {
                TempData["alert-danger"] = "Error -> " + ex;
            }
            return RedirectToAction("Manage", "Mold", new { id = mold_code, no = mold_no });
        }

        [HttpPost]
        public ActionResult CreateSA()
        {
            var mold_code = Request.Form["sa_mold"];
            var mold_no = double.Parse(Request.Form["sa_no"]);

            try
            {
                td_reference tdr = new td_reference();
                tdr.mold_code = mold_code;
                tdr.mold_no = mold_no;
                tdr.ref_id = 1;//1 = SA
                tdr.ref_no = Request.Form["sano"];
                tdr.active = true;
                tdr.entry_dt = DateTime.Now;
                tdr.entry_user = Session["TEMS_Auth"].ToString();
                dbTEMS.td_reference.Add(tdr);
                dbTEMS.SaveChanges();

                UpdateMoldStatus(mold_code, mold_no);

                TempData["alert-success"] = "Successfully.";
            }
            catch (Exception ex)
            {
                TempData["alert-danger"] = "Error -> " + ex;
            }
            return RedirectToAction("Manage", "Mold", new { id = mold_code, no = mold_no });
        }

        [HttpPost]
        public ActionResult CreateScrap()
        {
            var mold_code = Request.Form["sc_mold"];
            var mold_no = double.Parse(Request.Form["sc_no"]);

            try
            {

                UpdateMoldStatus(mold_code, mold_no, 101);
                
                TempData["alert-success"] = "Successfully.";
            }
            catch (Exception ex)
            {
                TempData["alert-danger"] = "Error -> " + ex;
            }
            return RedirectToAction("Manage", "Mold", new { id = mold_code, no = mold_no });
        }

        /// <param name="mcode"></param>
        /// <param name="mno"></param>
        /// <param name="status"></param>
        private void UpdateMoldStatus(string mcode, double mno, int status = 1)
        {
            using(var local = new TEMS1Entities()){
                var query = local.td_mold.Find(mcode, mno);
                if (query != null)
                {
                    query.status_id = status;
                    local.SaveChanges();
                }
            }
        }

        /// <param name="mold_code"></param>
        /// <param name="mold_no"></param>
        /// <param name="round"></param>
        /// <param name="ulv"></param>
        /// <param name="org"></param>
        /// <param name="act_id"></param>
        /// <param name="judge"></param>
        /// <param name="actor"></param>
        private void InsertMoldTran(string mold_code, int mold_no, int round, byte ulv, int org, byte act_id, string judge = null, string actor = null)
        {
            td_tran tran = new td_tran();
            tran.mold_code = mold_code;
            tran.mold_no = mold_no;
            tran.round = round;
            tran.ulv_id = ulv;
            tran.org_id = org;
            tran.act_id = act_id;
            if (judge != null) tran.judge_id = judge;
            tran.actor = actor;
            tran.act_dt = DateTime.Now;
            dbTEMS.td_tran.Add(tran);
        }

        /// <param name="mold_code"></param>
        /// <param name="mold_no"></param>
        /// <param name="round"></param>
        /// <param name="ulv"></param>
        /// <param name="org"></param>
        /// <param name="act_id"></param>
        /// <param name="judge"></param>
        /// <param name="actor"></param>
        private void ManageTran(string mold_code, int mold_no, int round, byte ulv, int org, byte act_id, string judge, string actor = null)
        {
            switch (act_id)
            {
                case 1://Receive New Mold
                    {
                        InsertMoldTran(mold_code, mold_no, round, ulv, org, act_id, judge, actor);
                        //InsertMoldTran(mold_code, mold_no, round, 1, org, 2);//Wait appearance check
                        break;
                    }
                case 2://Appearance Check
                    {
                        InsertMoldTran(mold_code, mold_no, round, 1, org, 3);//Wait judgement test
                        break;
                    }
                case 3:
                    {
                        InsertMoldTran(mold_code, mold_no, round, 1, org, 4);//Wait request job test
                        break;
                    }
                case 4:
                    {
                        InsertMoldTran(mold_code, mold_no, round, 1, org, 5);//Wait issue job test
                        break;
                    }
                case 5:
                    {

                        break;
                    }
                default:
                    break;
            }
        }

        public ActionResult Search()
        {
            ViewBag.Unit = dbTEMS.tm_unit.Where(w => w.active == true);
            ViewBag.Location = dbTEMS.tm_location.Where(w => w.active == true);
            ViewBag.Plant = dbTEMS.tm_plant.Where(w => w.active == true);
            return View();
        }

        [HttpPost]
        public ActionResult _SearchMold(string mold_code = "", float mold_no = 0, string item_code = "", string prod_code = "", byte unit = 0, string wc = "", int cav = 0, byte plant = 0, byte location = 0, int display = 20)
        {
            var sql = dbTEMS.td_mold.Where(w => w.active == true);
            if (mold_code != "")
            {
                sql = sql.Where(w => w.mold_code.Contains(mold_code));
            }

            if (mold_no != 0)
            {
                sql = sql.Where(w => w.mold_no == mold_no);
            }

            if (item_code != "")
            {
                sql = sql.Where(w => w.item_code.Contains(item_code));
            }

            if (wc != "")
            {
                sql = sql.Where(w => w.tm_item.tm_wc.wc_name.Contains(wc));
            }

            if (prod_code != "")
            {
                sql = sql.Where(w => w.tm_item.tm_wc.tm_product.prod_name.Contains(prod_code));
            }

            if (plant != 0)
            {
                sql = sql.Where(w => w.tm_item.tm_wc.tm_product.tm_plant.plant_id == plant);
            }

            if (unit != 0)
            {
                sql = sql.Where(w => w.tm_unit.unit_id == unit);
            }

            if (cav != 0)
            {
                sql = sql.Where(w => w.cav_per_unit == cav);
            }

            if (location != 0)
            {
                sql = sql.Where(w => w.tm_location.location_id == location);
            }

            return PartialView(sql.Take(display));
        }

        //-----------------------------------//

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult GetProductList(byte plant_id)
        {
            var get_selected = (from a in dbTEMS.tm_product
                                where a.plant_id == plant_id
                                select new { Id = a.prod_id, Value = a.prod_name }).ToArray();

            return Json(get_selected, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult GetWCList(string prod_id)
        {
            var get_selected = (from a in dbTEMS.tm_wc
                                where a.prod_id == prod_id
                                select new { Id = a.wc_id, Value = a.wc_name }).ToArray();

            return Json(get_selected, JsonRequestBehavior.AllowGet);
        }

        //-----------------------------------//

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult GetProductComplete(string query)
        {
            var get_selected = (from a in dbTEMS.tm_product
                                where a.prod_name.Contains(query)
                                orderby a.prod_name
                                select new { id = a.prod_id, label = a.prod_name }).Take(5).ToArray();

            return Json(get_selected, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult GetWCComplete(string query)
        {
            var get_selected = (from a in dbTEMS.tm_wc
                                where a.wc_name.Contains(query)
                                orderby a.wc_name
                                select new { id = a.wc_id, label = a.wc_name }).Take(5).ToArray();

            return Json(get_selected, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult GetItemComplete(string query)
        {
            var get_selected = (from a in dbTEMS.tm_item
                                where a.item_code.Contains(query)
                                orderby a.item_code
                                select new { id = a.item_code, label = a.item_code }).Take(5).ToArray();

            return Json(get_selected, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public JsonResult GetMoldComplete(string query)
        {
            var get_selected = (from a in dbTEMS.td_mold
                                where a.mold_code.Contains(query)
                                orderby a.mold_code
                                select new { id = a.mold_code, label = a.mold_code }).Distinct().Take(5).ToArray();

            return Json(get_selected, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetMoldAutoComp(string term)
        //{
        //    var result = (from r in dbTEMS.td_mold
        //                  where r.mold_code.Contains(term)
        //                  select r.mold_code).Distinct();
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //-----------------------------------//

        [HttpGet]
        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult GetItemSelect2(string searchTerm)
        {
            var item = dbTEMS.tm_item
                .Where(w => w.item_code.Contains(searchTerm))
                .OrderBy(o => o.item_code)
                .Select(s => new { id = s.item_code, text = s.item_code })
                .Take(16)
                .ToList();
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult GetSelectedItemSelect2(string id, double no)
        {
            var get_selected = dbTEMS.td_mold.Where(a => a.mold_code == id && a.mold_no == no)
                .Select(s => new { id = s.item_code, text = s.item_code });
            if (get_selected != null)
                return Json(get_selected, JsonRequestBehavior.AllowGet);
            else
                return Json(0, JsonRequestBehavior.AllowGet);
        }

        //-----------------------------------//

        private KeyValuePair<string, double> GetTnDData(string ref_old_td)
        {
            tooldieliveEntities tdl = new tooldieliveEntities();
            double percent = 0;
            string promise_dt = "-";
            var get_main_job = (from a in tdl.tr_main_job
                                where a.ref_old_td == ref_old_td
                                select new { a.main_job_no, a.promise_date }).First();
            if (get_main_job.main_job_no != null)
            {
                promise_dt = get_main_job.promise_date;

                var get_tr = tdl.tr_job_progress.Where(w => w.main_job_no == get_main_job.main_job_no).Count();
                var get_td = tdl.ts_process_result.Where(w => w.main_job_no == get_main_job.main_job_no && w.status == "Y").Count();

                if (get_tr != 0)
                {
                    percent = (double)get_td / get_tr * 100;
                }
            }

            return new KeyValuePair<string, double>(promise_dt, percent);
        }

        private KeyValuePair<string, string> GetSAData(string sa_no)
        {
            SAEntities sa = new SAEntities();
            var get_sa = (from a in sa.v_tems
                          where a.control_no == sa_no
                          select new { a.status_name, a.expect_date }).First();
            string status = "";
            string expect_dt = "";
            
            if (get_sa != null)
            {
                status = get_sa.status_name;
                expect_dt = get_sa.expect_date.ToString("dd/MM/yyyy");
            }

            return new KeyValuePair<string, string>(status, expect_dt);
        }

        private KeyValuePair<string, string> GetTNTData(string pr_no)
        {
            TNTEntities tnt = new TNTEntities();
            var get_tnt = (from a in tnt.v_connect_tems
                           where a.pr_no == pr_no
                           select new { a.po_no, a.eta_tnc }).First();
            string po_no = "";
            string eta = string.Empty;

            if (get_tnt != null)
            {
                po_no = get_tnt.po_no;
                if (get_tnt.eta_tnc != null) eta = get_tnt.eta_tnc.Value.ToString("dd/MM/yyyy");
            }

            return new KeyValuePair<string, string>(po_no, eta);
        }

        private KeyValuePair<string, string> GetRemedyData(string rm_no)
        {
            var get_rm = (from a in dbTEMS.td_mold
                           select a.tm_status.status_name).First();
            string status = "";

            if (get_rm != null)
            {
                status = get_rm;
            }

            return new KeyValuePair<string, string>(status, "");
        }

        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult GetMoreRefData(byte id, string no)
        {
            string html = string.Empty;

            switch (id)
            {
                case 1://SA
                    {
                        var x = GetSAData(no);
                        html = "<b>SA Status : </b>" + x.Key + "<br /><b>Expect Date : </b>" + x.Value;
                        break;
                    }
                case 2://T&D
                    {
                        var x = GetTnDData(no);
                        string val = x.Value.ToString("N0");
                        if (x.Value < 50)
                        {
                            html = "<div class='progress'><div class='progress-bar progress-bar-danger' role='progressbar' aria-valuenow='" + val + "' aria-valuemin='0' aria-valuemax='100' style='min-width: 2em; width: " + val + "%;'>" + val + "%</div></div>" + "<b>Promise Date: </b>" + x.Key;
                        }
                        else
                        {
                            html = "<div class='progress'><div class='progress-bar progress-bar-success' role='progressbar' aria-valuenow='" + val + "' aria-valuemin='0' aria-valuemax='100' style='width: " + val + "%;'>" + val + "%</div></div>" + "<b>Promise Date: </b>" + x.Key;
                        }
                        break;
                    }
                case 3://NOK
                    {
                        var x = GetTNTData(no);
                        html = "<b>PO No. : </b>" + x.Key + "<br /><b>ETA Date : </b>" + x.Value;
                        break;
                    }
                case 4://Remedy
                    {
                        var x = GetRemedyData(no);
                        html = "<b>Remedy Status : </b>" + x.Key;
                        break;
                    }
                default:
                    {
                        html = "-";
                        break;
                    }
            }

            return Json(html, JsonRequestBehavior.AllowGet);
        }
    }
}