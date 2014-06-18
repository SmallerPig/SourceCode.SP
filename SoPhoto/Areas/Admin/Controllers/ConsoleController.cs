using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class ConsoleController : Controller
    {
        //
        // GET: /Admin/Console/
        BLL.AdminManage adminHelper = new BLL.AdminManage();




        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Main()
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("/Admin/Console/Login");
            }

            return View();
        }

        [HttpPost]
        public JsonResult Login(Entity.Admin admin)
        {
            string loginResult = adminHelper.Login(admin);
            if (loginResult != "success")
            {
                HttpContext.Session["Admin"] = null;
                return Json("Error");
            }
            else
            {
                HttpContext.Session.Timeout = 600;
                HttpContext.Session["Admin"] = admin;
                return Json("Success");
            }
        }






    }
}
