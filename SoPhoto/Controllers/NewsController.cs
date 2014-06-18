using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoPhoto.Models;

namespace SoPhoto.Controllers
{
    public class NewsController : Controller
    {
        private BLL.News helper = new BLL.News();
        private BLL.AD adHelper = new BLL.AD();

        protected int pageIndex = 1;
        protected int pageSize = 10;
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
            perPage = "/" + "News" + "/Index"  + "?pageIndex=" + (pageIndex - 1).ToString();
            nextPage = "/" +"News" + "/Index" + "?pageIndex=" + (pageIndex + 1).ToString();
            ViewData["perPage"] = perPage;
            ViewData["nextPage"] = nextPage;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
            IEnumerable<Entity.SP_News> newses = helper.GetList((pageIndex-1)*pageSize, pageSize, false);
            NewsIndex model = new NewsIndex()
            {
                Newses = newses,
                Ads = adHelper.GetList(0, 2, false)
            };


            return View(model);
        }

        public ActionResult Detail(int id)
        {
            Entity.SP_News news = helper.GetById(id);


            NewsDetail model = new NewsDetail()
            {
                News = news,
                Ads = adHelper.GetList(0, 2, false)
            };

            return View(model);
        }




    }
}
