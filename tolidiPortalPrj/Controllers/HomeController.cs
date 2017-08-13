using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tpr.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView
            return View();    
        }
        public ActionResult About()
        {
            // DXCOMMENT: Pass a data model for GridView
            return View();
        }

    }
}

public enum HeaderViewRenderMode { Full, Menu, Title }