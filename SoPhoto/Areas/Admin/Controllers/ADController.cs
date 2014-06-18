using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class ADController : BaseController<Entity.SP_AD>
    {

        public ADController()
        {
            helper = new BLL.AD();
        }

        //
        // GET: /Admin/AD/

        [HttpPost]
        public ActionResult Create(Entity.SP_AD ad)
        {
            helper.Insert(ad);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(Entity.SP_AD ad)
        {
            helper.Update(ad);
            return RedirectToAction("List");
        }

    }
}
