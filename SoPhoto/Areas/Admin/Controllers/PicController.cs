﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SoPhoto.Areas.Admin.Models;

namespace SoPhoto.Areas.Admin.Controllers
{
    public class PicController : BaseController<Entity.SP_Pics>
    {

        public PicController()
        {
            helper = new BLL.Pics();
        }

        public override ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Entity.SP_Pics model)
        {
            model.Class_Edit = Request.Form["Class_Edit"];
            model.Class_Age = Request.Form["class_age"];
            model.Cover =CreateCover(model.Address);
            if (model.BaseCategory==2)
            {
                model.CreativeType = 3;
            }
            #region zip
            if (model.Address.EndsWith("zip"))
            {
                IList<string> filenameStrings = Session["filenameStrings"] as List<string>;
                if (filenameStrings!=null)
                {
                    int _index = GetIndexOfString(model.Address, "_");
                    int dotindex = GetIndexOfString(model.Address, ".");
                    string countstring = model.Address.Substring(_index + 1, dotindex-_index-1);
                    string fileName = model.Address.Substring(0,_index);
                    int count = int.Parse(countstring);
                    for (int i = 0; i < count; i++)
                    {
                        string extent = RY.Common.DirectoryAndFile.GetFileExt(filenameStrings[i]);
                        string address = fileName + "_" + i + "." + extent;
                        model.Address = address;
                        model.Cover = CreateCover(address);
                        model.Title = filenameStrings[i];
                        model.IsLock = true;
                        Insert(model);
                    }                    
                }
                return RedirectToAction("ConsoleList");
            }
            #endregion
            model = helper.Insert(model);
            if (!model.IsLock)
            {
                BLL.SearchHelper.GetInstance().AddArticle(model.Id);
            }
            return RedirectToAction("ConsoleList");
        }

        string CreateCover(string address)
        {
            string datastring = address.Substring(0, address.LastIndexOf('.'));
            string result = datastring + "_Cover." + RY.Common.DirectoryAndFile.GetFileExt(address);
            return result;
        }

        private void Insert(Entity.SP_Pics pic)
        {
            pic = helper.Insert(pic);
            if (!pic.IsLock)
            {
                BLL.SearchHelper.GetInstance().AddArticle(pic.Id);
            }
        }

        public ActionResult ConsoleList(string keyword)
        {
            var picHelper = helper as BLL.Pics;

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
            count = picHelper.GetCount();
            pageCount = (count % pageSize == 0) ? count / pageSize : count / pageSize + 1;
            if (pageIndex >= pageCount && pageCount > 0)
            {
                pageIndex = pageCount;
            }

            if (string.IsNullOrWhiteSpace(keyword))
            {
                list = picHelper.GetList(pageSize * (pageIndex - 1), pageSize);
                string actionName = "ConsoleList";

                perPage = "/admin/" + entity.GetType().Name + "/" + actionName + "?pageIndex=" + (pageIndex - 1).ToString();
                nextPage = "/admin/" + entity.GetType().Name + "/" + actionName + "?pageIndex=" + (pageIndex + 1).ToString();
                ViewData["perPage"] = perPage;
                ViewData["nextPage"] = nextPage;
                ViewData["pageIndex"] = pageIndex;
                ViewData["pageCount"] = pageCount;

            }
            else
            {
                list = picHelper.SearchByPicCode(keyword);
                ViewData["perPage"] = perPage;
                ViewData["nextPage"] = nextPage;
                ViewData["pageIndex"] = 1;
                ViewData["pageCount"] = 1;
            }

            return View(list);
        }


        public override ActionResult Edit(int id)
        {
            Entity.SP_Pics pic = helper.GetById(id);
            return View(pic);
        }


        [HttpPost]
        public ActionResult Edit(Entity.SP_Pics model)
        {
            model.Class_Edit = Request.Form["Class_Edit"];
            model.Class_Age = Request.Form["class_age"];
            model.Cover =CreateCover(model.Address);
            if (model.BaseCategory == 2)
            {
                model.CreativeType = 4;
            }
            helper.Update(model);

            if (model.IsLock)
            {
                BLL.SearchHelper.GetInstance().RemoveArticle(model.Id);
            }
            else
            {
                //BLL.SearchHelper.GetInstance().RemoveArticle(model.Pic.Id);
                BLL.SearchHelper.GetInstance().AddArticle(model.Id);
            }
            return RedirectToAction("ConsoleList");
        }

        public override JsonResult Delete(int id)
        {
            BLL.SearchHelper.GetInstance().RemoveArticle(id);
            return base.Delete(id);
        }


        int GetIndexOfString(string orgion, string query)
        {
            return orgion.LastIndexOf(query, System.StringComparison.Ordinal);
        }
    
    }
}
