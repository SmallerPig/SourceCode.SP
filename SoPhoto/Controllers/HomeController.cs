using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Controllers
{
    public class HomeController : Controller
    {

        //private BLL.Pics picsHelper = new BLL.Pics();
        private BLL.Banner bannerHelper = new BLL.Banner();
        private BLL.Topic topicHelper = new BLL.Topic();
        private BLL.AD adHelper = new BLL.AD();
        private BLL.News newsHelper = new BLL.News();


        //
        // GET: /Home/

        public ActionResult Index()
        {
            Models.Home model = new Models.Home();
            model.Banners = bannerHelper.GetList(0, 5, false);
            model.Topics = topicHelper.GetList(0, 4, false);
            model.Newses = newsHelper.GetList(0, 8, false);
            model.Ads = adHelper.GetList(0, 2, false);
            return View(model);
        }

    }
}
