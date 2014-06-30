using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoPhoto.Areas.Admin.Models;
using SoPhoto.Entity;
using SoPhoto.Models;
using System.Text.RegularExpressions;

namespace SoPhoto.Controllers
{
    public class PicsController : Controller
    {
        //
        // GET: /Pics/
        private BLL.Pics helper = new BLL.Pics();

        protected int pageIndex = 1;
        protected int pageSize = 21;
        protected int count = 0;
        protected int pageCount = 1;
        protected string perPage = "";
        protected string nextPage = "";
        
        public ActionResult Search()
        {

            IEnumerable<Entity.SP_Pics> piclist;
            IEnumerable<KeyValuePair<string, string[]>> parms = ProcessRequest();
            string keyword = Request.QueryString["keyword"];
            if (!string.IsNullOrEmpty(keyword))
            {
                Entity.SP_Pics p = helper.GetByCode(keyword);
                if(p!=null)
                {
                    return View(new List<Entity.SP_Pics>() { p });
                }
                //count = helper.CountByKeyWord(keyword);
                piclist = helper.SearchByKeyWord(keyword);

                //keyword = RY.Common.StringHelp.FilterSymbolStr(keyword);
                ////int count1 = count;
                //piclist = BLL.SearchHelper.GetInstance()
                //    .SearchIndex(keyword, pageSize, pageIndex);
                count = piclist.Count();
                ProcessPage();
                if(piclist !=null)
                {
                    piclist = helper.Filter(piclist, parms);
                    piclist = piclist.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
            }
            else
            {
                count = helper.CountByFilter(parms);
                ProcessPage();
                piclist = helper.Filter(parms, pageIndex, pageSize);
            }
            return View(piclist);
        }

        void ProcessPage()
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
            pageCount = (count % pageSize == 0) ? count / pageSize : count / pageSize + 1;
            if (pageIndex >= pageCount && pageCount > 0)
            {
                pageIndex = pageCount;
            }
            string url = DeletePageIndex(Request.Url.Query);
            if(Request.Url.Query.IndexOf("pageindex")<0)
            {
                ViewData["perPage"] = "#";
                ViewData["nextPage"] = "/pics/search?" + url + "&pageindex=2";
            }else
            {
                string currentPage = Request.QueryString["pageindex"];
                if (!string.IsNullOrEmpty(currentPage))
                {
                    int currentPageIndex = int.Parse(currentPage);
                    ViewData["perPage"] = "/pics/search?" + url + "&pageindex=" + (currentPageIndex - 1);
                    ViewData["nextPage"] = "/pics/search?" + url + "&pageindex=" + (currentPageIndex + 1);
                }
            }
            ViewData["pageIndex"] = pageIndex;
            ViewData["pageCount"] = pageCount;
        }

        private string DeletePageIndex(string p)
        {
            p =  p.Replace("?", "");
            if(p.IndexOf("pageindex")>=0)
            {
                var regex = new Regex("&?pageindex=\\d+");
                return regex.Replace(p, "");                
            }
            return p;
        }




        IEnumerable<KeyValuePair<string, string[]>> ProcessRequest()
        {
            KeyValuePair<string, string[]> parms;
            string[] baseCategory = (Request.QueryString["baseCategory"] ?? "0").Split(',');
            string[] creativeType = (Request.QueryString["creativeType"] ?? "0").Split(',');
            string[] editType = (Request.QueryString["editType"]?? "0").Split(',');
            string[] class_style = (Request.QueryString["class_style"]?? "0").Split(',');
            string[] class_location = (Request.QueryString["class_location"]?? "0").Split(',');
            string[] class_color = (Request.QueryString["class_color"]?? "0").Split(',');
            string[] class_place = (Request.QueryString["class_place"]?? "0").Split(',');
            string[] class_scene = (Request.QueryString["class_scene"]?? "0").Split(',');
            string[] class_sex = (Request.QueryString["class_sex"]?? "0").Split(',');
            string[] class_age = (Request.QueryString["class_age"] ?? "0").Split(',');
            return new List<KeyValuePair<string, string[]>>()
            {
                new KeyValuePair<string, string[]>("Category", baseCategory[0]=="0"?null:baseCategory),
                new KeyValuePair<string, string[]>("creativeType", creativeType[0]=="0"?null:creativeType),
                new KeyValuePair<string, string[]>("editType", editType[0]=="0"?null:editType),
                new KeyValuePair<string, string[]>("style", class_style[0]=="0"?null:class_style),
                new KeyValuePair<string, string[]>("location", class_location[0]=="0"?null:class_location),
                new KeyValuePair<string, string[]>("color", class_color[0]=="0"?null:class_color),
                new KeyValuePair<string, string[]>("place", class_place[0]=="0"?null:class_place),
                new KeyValuePair<string, string[]>("scene", class_scene[0]=="0"?null:class_scene),
                new KeyValuePair<string, string[]>("sex", class_sex[0]=="0"?null:class_sex),
                new KeyValuePair<string, string[]>("age", class_age[0]=="0"?null:class_age),
            };
        }

        


        public ActionResult Detail(int id)
        {
            Entity.SP_Pics pic = helper.GetById(id);
            if(pic!=null)
            {
                IEnumerable<Entity.SP_Pics> RelatedPic = BLL.SearchHelper.GetInstance()
                        .SearchIndex(pic.KeyWords.Replace(";","+"), 9, pageIndex);
                ViewBag.Related = RelatedPic;
            }

            return View(pic);
        }


        public ActionResult PicCode(string code)
        {
            Entity.SP_Pics pic = helper.GetByCode(code);
            if (pic != null)
            {
                IEnumerable<Entity.SP_Pics> RelatedPic = BLL.SearchHelper.GetInstance()
                        .SearchIndex(pic.KeyWords.Replace(";", "+"), 9, pageIndex);
                ViewBag.Related = RelatedPic;
            }
            return View(pic);
        }

    }
}
