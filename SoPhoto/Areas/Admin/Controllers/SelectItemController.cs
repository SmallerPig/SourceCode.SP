using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class SelectItemController :BaseController<Entity.SelectItem>
    {
        //
        // GET: /Admin/SelectItem/
        public SelectItemController()
        {
            helper = new BLL.SelectItem();
        }


        [HttpPost]
        public ActionResult Create(Entity.SelectItem model)
        {
            model.IsLock = false;
            helper.Insert(model);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(Entity.SelectItem model)
        {


            helper.Update(model);
            return RedirectToAction("List");
        }
    }
}
