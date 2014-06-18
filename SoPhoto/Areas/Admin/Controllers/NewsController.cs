using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class NewsController : BaseController<Entity.SP_News>
    {
        //
        // GET: /Admin/News/
        public NewsController()
        {
            helper = new BLL.News();
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Entity.SP_News news)
        {
            helper.Insert(news);
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Entity.SP_News news)
        {
            helper.Update(news);
            return RedirectToAction("List");
        }


    }
}
