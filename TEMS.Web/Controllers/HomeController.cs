using System.Linq;
using System.Web.Mvc;
using TEMS.Web.Models;
using WebCommonFunction;

namespace TEMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private TNCSecurity secure = new TNCSecurity();
        private TEMS1Entities dbTEMS = new TEMS1Entities();
        private TNC_ADMINEntities dbTNC = new TNC_ADMINEntities();

        [AllowAnonymous]
        public ActionResult Login(string key = null)
        {
            string username = key == null ? Request.Form["Username"].ToString() : "";
            string pass = key == null ? Request.Form["Password"].ToString() : "";

            var chklogin = secure.Login(username, pass, false);//set false to true for Real

            if (key != null)
            {
                username = secure.WebCenterDecode(key);
                chklogin = secure.Login(username, "a", false);
            }

            if (chklogin != null)
            {
                Session["TEMS_Auth"] = chklogin.emp_code;

                var ulv = (from a in dbTNC.tnc_user_level
                           where a.position_min <= chklogin.position_level && a.position_max >= chklogin.position_level
                           select a).FirstOrDefault();

                if (ulv != null)
                {
                    Session["TEMS_ULv"] = ulv.ulv_id;
                    Session["TEMS_Org"] = chklogin.LeafOrgGroupId;

                    if (chklogin.emp_lname.Length > 2)
                    {
                        Session["TEMS_Name"] = chklogin.emp_fname + " " + chklogin.emp_lname.Substring(0, 2);
                    }
                    else
                    {
                        Session["TEMS_Name"] = chklogin.emp_fname + " " + chklogin.emp_lname;
                    }
                }

                //-------------------------------------//

                var chk_sys_user = (from a in dbTEMS.tm_user
                                    where a.emp_code == chklogin.emp_code
                                    select a).FirstOrDefault();

                Session["TEMS_UType"] = chk_sys_user != null ? chk_sys_user.utype_id : 0;

                //-------------------------------------//

                var chk_sys_group = (from a in dbTEMS.tm_group
                                     where a.group_id == chklogin.LeafOrgGroupId
                                     select a).FirstOrDefault();

                Session["TEMS_GType"] = chk_sys_group != null ? chk_sys_group.gtype_id : 0;

                //-------------------------------------//

                if (Session["TEMS_Redirect"] != null)
                {
                    string url = Session["TEMS_Redirect"].ToString();
                    Session.Remove("TEMS_Redirect");
                    return Redirect(url);
                }
                else
                {
                    return RedirectToAction("MainMenu", "Home");
                }
            }
            else
            {
                TempData["ErrorMsg"] = "Username or Password is incorrect";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("TEMS_Auth");
            Session.Remove("TEMS_Name");
            Session.Remove("TEMS_UType");
            Session.Remove("TEMS_ULv");
            Session.Remove("TEMS_Org");
            Session.Remove("TEMS_GType");
            return RedirectToAction("Index", "Home");
        }

        //-------------------------------------------//

        public ActionResult Index(string key = null)
        {
            if (key != null)
            {
                return Login(key);
            }
            else
            {
                return View();
            }
        }

        [Chk_Auth]
        public ActionResult MainMenu()
        {
            return View();
        }

        [Chk_Auth]
        public ActionResult Dashboard()
        {
            return View();
        }

        //-------------------------------------------//

        
    }
}