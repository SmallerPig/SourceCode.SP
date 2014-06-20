using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/Admin/
        BLL.AdminManage helper = new BLL.AdminManage();

        public ActionResult ChangePassword()
        {
            Entity.SP_Admin admin = helper.getAdmin();
            return View(admin);
        }

        [HttpPost]
        public JsonResult ChangePassword(string userName, string oldPassword, string newPassword)
        {

            //Entity.SP_Admin model = new Entity.SP_Admin() { UserName = userName, Password = newPassword };
            Entity.SP_Admin admin = helper.ChangePassword(oldPassword, newPassword);

            if (admin == null)
            {
                return Json("Error");
            }
            else
            {
                return Json("Success");
            }
        }


    }
}
