using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class BannerController : BaseController<Entity.SP_Banner>
    {
        public override ActionResult List(string keyword)
        {
            var bannerHelper = helper as BLL.Banner;

            try
            {
                if (!string.IsNullOrEmpty(this.Request.QueryString["pageIndex"]))
                {
                    pageIndex = int.Parse(this.Request.QueryString["pageIndex"]);
                }
                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }
            }
            catch
            {
                pageIndex = 1;
            }
            count = bannerHelper.GetCount(false);
            pageCount = (count % pageSize == 0) ? count / pageSize : count / pageSize + 1;
            if (pageIndex >= pageCount && pageCount > 0)
            {
                pageIndex = pageCount;
            }

            if (string.IsNullOrWhiteSpace(keyword))
            {
                list = bannerHelper.GetList(pageSize * (pageIndex - 1), pageSize, false);
                string actionName = "List";
                perPage = "/admin/" + entity.GetType().Name + "/" + actionName + "?pageIndex=" + (pageIndex - 1).ToString();
                nextPage = "/admin/" + entity.GetType().Name + "/" + actionName + "?pageIndex=" + (pageIndex + 1).ToString();
                ViewData["perPage"] = perPage;
                ViewData["nextPage"] = nextPage;
                ViewData["pageIndex"] = pageIndex;
                ViewData["pageCount"] = pageCount;

            }
            else
            {
                list = bannerHelper.SearchByPicCode(keyword);
                ViewData["perPage"] = perPage;
                ViewData["nextPage"] = nextPage;
                ViewData["pageIndex"] = 1;
                ViewData["pageCount"] = 1;
            }

            return View(list);
        }


        public BannerController()
        {
            helper = new BLL.Banner();
        }

        [HttpPost]
        public ActionResult Create(Entity.SP_Banner banner)
        {
            helper.Insert(banner);
            return RedirectToAction("List");

        }

        [HttpPost]
        public ActionResult Edit(Entity.SP_Banner banner)
        {
            helper.Update(banner);
            return RedirectToAction("List");

        }




    }
}
