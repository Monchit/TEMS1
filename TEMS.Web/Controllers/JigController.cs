﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEMS.Web.Controllers
{
    public class JigController : Controller
    {
        //
        // GET: /Jig/
        [Chk_Auth]
        public ActionResult Index()
        {
            return View();
        }

        [Chk_Auth]
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
