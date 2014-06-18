using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoPhoto.Areas.Admin.Controllers
{
    public abstract class BaseController<Tentity> : Controller where Tentity : RY.Common.Console.IConsoleEntity, new()
    {
        private String exceptionFileName = "exception.txt";


        //public BaseController(IConsoleListable<Tentity> Ihelper)
        //{
        //    helper = Ihelper;
        //}
        protected int pageIndex = 1;
        protected int pageSize = 20;
        protected int count = 0;
        protected int pageCount = 0;
        protected string perPage = "#";
        protected string nextPage = "#";

        protected IList<Tentity> list;

        protected Tentity entity = new Tentity();
        protected RY.Common.IConsoleListable<Tentity> helper;

        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext
           filterContext)
        {
            if (filterContext.HttpContext.Session["admin"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Admin/Console/Login");
            }
        }


        public virtual ActionResult List(string keyword)
        {
            InitList(false);
            return View(list);
        }

        [HttpPost]
        public JsonResult List(int id)
        {
            Tentity entity = helper.GetById(id);
            helper.ToRecycle(entity);
            return Json("success");
        }


        protected void InitList(bool status)
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
            count = helper.GetCount(status);
            pageCount = (count % pageSize == 0) ? count / pageSize : count / pageSize + 1;
            if (pageIndex >= pageCount && pageCount > 0)
            {
                pageIndex = pageCount;
            }
            list = helper.GetList(pageSize * (pageIndex - 1), pageSize, status);
            GetList();
            string actionName = status ? "Recycle" : "List";

            perPage = "/admin/" + entity.GetType().Name + "/" + actionName + "?pageIndex=" + (pageIndex - 1).ToString();
            nextPage = "/admin/" + entity.GetType().Name + "/" + actionName + "?pageIndex=" + (pageIndex + 1).ToString();
            ViewData["perPage"] = perPage;
            ViewData["nextPage"] = nextPage;
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;

        }

        /// <summary>
        /// 页面Model获得函数 ----模板方法模式
        /// </summary>
        public virtual void GetList()
        { }


        public ActionResult Recycle()
        {
            InitList(true);
            return View(list);
        }



        [HttpPost]
        public JsonResult Recycle(int id)
        {
            Tentity entity = helper.GetById(id);
            helper.Restore(entity);
            return Json("success");
        }


        public ActionResult Details(int id)
        {
            Tentity entity = helper.GetById(id);
            helper.Delete(entity);
            return RedirectToAction("List");
        }


        public virtual ActionResult Create()
        {
            return View();
        }

        public ActionResult ToRecycle(int id)
        {
            Tentity entity = helper.GetById(id);
            helper.ToRecycle(entity);
            return RedirectToAction("List");
        }

        public ActionResult Restore(int id)
        {
            Tentity entity = helper.GetById(id);
            helper.Restore(entity);
            return RedirectToAction("Recycle");
        }


        public virtual ActionResult Edit(int id)
        {
            Tentity entity = helper.GetById(id);
            return View(entity);
        }

        [HttpPost]
        public virtual JsonResult Delete(int id)
        {
            try
            {
                Tentity entity = helper.GetById(id);
                helper.Delete(entity);
                return Json("success");
            }
            catch
            {
                return Json("error");
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Log(filterContext);
            base.OnException(filterContext);
        }

        private void Log(ExceptionContext filterContext)
        {
            string fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, exceptionFileName);
            StreamWriter sw = System.IO.File.AppendText(fileName);

            System.DateTime dt = DateTime.Now;
            string ip = RY.Common.IPHelp.ClientIP;
            string url = Request.Url.ToString();
            sw.Write(string.Format("Time:{0:G}; ClientIp:{1}; URL:{2}; Message:{3}\n", dt, ip, url, filterContext.Exception.Message));
            sw.Close();
        }


        public ActionResult Error()
        {
            return View();
        }


    }
}
