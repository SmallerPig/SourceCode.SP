using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Admin/Demo/

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

    }
}
