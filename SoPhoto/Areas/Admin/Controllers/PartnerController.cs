using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class PartnerController : BaseController<Entity.Partner>
    {
        BLL.Partner phelper = new BLL.Partner();

        public PartnerController()
        {
            helper = new BLL.Partner();
        }

        [HttpPost]
        public ActionResult Reset(int id)
        {
            Entity.Partner partner = helper.GetById(id);
            partner.PassWord = partner.UserName + "123456";
            helper.Update(partner);
            return Json("success");
        }

        [HttpPost]
        public ActionResult Create(Entity.Partner model)
        {
            helper.Insert(model);
            return RedirectToAction("List");
        }


        [HttpPost]
        public ActionResult Edit(Entity.Partner model)
        {
            helper.Update(model);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult CheckUser(string username)
        {
            if(!string.IsNullOrEmpty(username) && phelper.GetByUserName(username)==null)
            {
                return Json("success");
            }
            return Json("error");
        }

    }
}
