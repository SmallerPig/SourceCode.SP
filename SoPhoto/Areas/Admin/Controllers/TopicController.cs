using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class TopicController : BaseController<Entity.SP_Topic>
    {
        public TopicController()
        {
            helper = new BLL.Topic();
        }


        public override ActionResult List(string keyword)
        {
            var topicHelper = helper as BLL.Topic;

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
            count = topicHelper.GetCount(false);
            pageCount = (count % pageSize == 0) ? count / pageSize : count / pageSize + 1;
            if (pageIndex >= pageCount && pageCount > 0)
            {
                pageIndex = pageCount;
            }

            if (string.IsNullOrWhiteSpace(keyword))
            {
                list = topicHelper.GetList(pageSize * (pageIndex - 1), pageSize,false);
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
                list = topicHelper.SearchByPicCode(keyword);
                ViewData["perPage"] = perPage;
                ViewData["nextPage"] = nextPage;
                ViewData["pageIndex"] = 1;
                ViewData["pageCount"] = 1;
            }

            return View(list);
        
        
        
        }


        //
        // GET: /Admin/Topic/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Entity.SP_Topic topic)
        {
            helper.Insert(topic);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(Entity.SP_Topic topic)
        {
            helper.Update(topic);
            return RedirectToAction("List");
        }

    }
}
