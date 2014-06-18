using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class SelectConditionController :BaseController<Entity.SelectCondition>
    {
        BLL.SelectCondition selectConditionHelper = new BLL.SelectCondition();



        public SelectConditionController()
        {
            helper = new BLL.SelectCondition();
        }

        public ActionResult ListByItem(int itemId)
        {
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
            count = selectConditionHelper.GetCount(itemId, false);
            pageCount = (count % pageSize == 0) ? count / pageSize : count / pageSize + 1;
            if (pageIndex >= pageCount && pageCount > 0)
            {
                pageIndex = pageCount;
            }

            list = selectConditionHelper.GetList(itemId, pageSize * (pageIndex - 1), pageSize, false);

            GetList();
            string actionName = "ListByItem";

            perPage = "/admin/" + entity.GetType().Name + "/" + actionName + "?pageIndex=" + (pageIndex - 1).ToString();
            nextPage = "/admin/" + entity.GetType().Name + "/" + actionName + "?pageIndex=" + (pageIndex + 1).ToString();

            ViewData["perPage"] = perPage;
            ViewData["nextPage"] = nextPage;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            return View(list);
        }

        [HttpPost]
        public ActionResult Create(Entity.SelectCondition model)
        {
            model.ItemId = int.Parse(Request.QueryString["itemid"]);

            helper.Insert(model);
            return RedirectToAction("ListByItem", new {itemid = model.ItemId});
        }

        [HttpPost]
        public ActionResult Edit(Entity.SelectCondition model)
        {
            model.ItemId = int.Parse(Request.QueryString["itemid"]);

            helper.Update(model);
            return RedirectToAction("ListByItem", new { itemid = model.ItemId });
        }



    }
}
