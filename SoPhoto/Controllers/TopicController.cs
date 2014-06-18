using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Controllers
{
    public class TopicController : Controller
    {
        //
        // GET: /Topic/
        private BLL.Topic helper = new BLL.Topic();


        protected int pageIndex = 1;
        protected int pageSize = 8;
        protected int count = 0;
        protected int pageCount = 0;
        protected string perPage = "";
        protected string nextPage = "";

        public ActionResult Index()
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
            count = helper.GetCount(false);
            pageCount = (count % pageSize == 0) ? count / pageSize : count / pageSize + 1;
            perPage = "/" + "Topic" + "/Index" + "?pageIndex=" + (pageIndex - 1).ToString();
            nextPage = "/" + "Topic" + "/Index" + "?pageIndex=" + (pageIndex + 1).ToString();
            ViewData["perPage"] = perPage;
            ViewData["nextPage"] = nextPage;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            IEnumerable<Entity.SP_Topic> model = helper.GetList((pageIndex - 1) * pageSize, pageSize, false);

            return View(model);
        }

        public ActionResult Content(int id)
        {
            var model = helper.GetById(id);
            return View(model);
        }

    }
}
