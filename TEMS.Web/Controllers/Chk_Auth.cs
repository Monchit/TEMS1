using System.Web;
using System.Web.Mvc;

namespace TEMS.Web.Controllers
{
    public class Chk_Auth : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["TEMS_Auth"] == null)
            {
                string loginpath = "~/Home/Index";
                if (HttpContext.Current.Request.Url != null)
                {
                    HttpContext.Current.Session["TEMS_Redirect"] = HttpContext.Current.Request.Url;
                }
                filterContext.Result = new RedirectResult(loginpath);
            }
        }
    }
}