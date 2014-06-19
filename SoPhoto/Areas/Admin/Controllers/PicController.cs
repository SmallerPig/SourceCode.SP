using System;
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
        //
        // GET: /Admin/Pic/
        private BLL.SelectItem selectItemHelper = new BLL.SelectItem();
        private BLL.SelectValue selectValueHelper = new BLL.SelectValue();
        private BLL.EditType editTypeHelper = new BLL.EditType();

        public PicController()
        {
            helper = new BLL.Pics();
        }

        public override ActionResult Create()
        {
            IEnumerable<Entity.SelectItem> items = selectItemHelper.GetList();
            IList<Models.PicSelect> selects = items.Select(selectItem => new PicSelect { SelectItems = selectItem }).ToList();
            PicModel model = new PicModel()
            {
                Selects = selects
            };
            return View(model);
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
            //if (model.BaseCategory==2)
            //{
            //    string[] editType_strings = Request.Form["editType"].Split(',');
            //    foreach (var editTypeString in editType_strings)
            //    {
            //        int editType = 0;
            //        if (int.TryParse(editTypeString,out editType))
            //        {
            //            Entity.SP_EditType type = new Entity.SP_EditType()
            //            {
            //                PicId = model.Id,
            //                Value = editType
            //            };
            //            editTypeHelper.Insert(type);
            //        }
            //    }
            //}

            //foreach (string key in Request.Form)
            //{
            //    if (key.StartsWith("Selects["))
            //    {
            //        var regex = new Regex(@"\d+");
            //        int itemId =int.Parse(regex.Match(key).Value);
            //        string[] valueStrings = Request.Form[key].Split(',');
            //        foreach (var valueString in valueStrings)
            //        {
            //            var conditionId = int.Parse(valueString);
            //            Entity.SelectValue value = new Entity.SelectValue()
            //            {
            //                PicId = model.Id,
            //                ItemId = itemId,
            //                ConditionId = conditionId
            //            };
            //            selectValueHelper.Insert(value);
            //        }
            //    }
            //}
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
            if (pic.BaseCategory == 2)
            {
                string[] editType_strings = Request.Form["editType"].Split(',');
                foreach (var editTypeString in editType_strings)
                {
                    int editType = 0;
                    if (int.TryParse(editTypeString, out editType))
                    {
                        Entity.SP_EditType type = new Entity.SP_EditType()
                        {
                            PicId = pic.Id,
                            Value = editType
                        };
                        editTypeHelper.Insert(type);
                    }
                }
            }

            foreach (string key in Request.Form)
            {
                if (key.StartsWith("Selects["))
                {
                    var regex = new Regex(@"\d+");
                    int itemId = int.Parse(regex.Match(key).Value);
                    string[] valueStrings = Request.Form[key].Split(',');
                    foreach (var valueString in valueStrings)
                    {
                        var conditionId = int.Parse(valueString);
                        Entity.SelectValue value = new Entity.SelectValue()
                        {
                            PicId = pic.Id,
                            ItemId = itemId,
                            ConditionId = conditionId
                        };
                        selectValueHelper.Insert(value);
                    }
                }
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
            //selectValueHelper.DeleteByPic(model.Id);
            //editTypeHelper.DeleteByPicId(model.Id);
            //if (model.BaseCategory == 2)
            //{
            //    string[] editType_strings = Request.Form["editType"].Split(',');
            //    foreach (var editTypeString in editType_strings)
            //    {
            //        int editType = 0;
            //        if (int.TryParse(editTypeString, out editType))
            //        {
            //            Entity.SP_EditType type = new Entity.SP_EditType()
            //            {
            //                PicId = model.Id,
            //                Value = editType
            //            };
            //            editTypeHelper.Insert(type);
            //        }
            //    }
            //}
            //foreach (string key in Request.Form)
            //{
            //    if (key.StartsWith("Selects["))
            //    {
            //        var regex = new Regex(@"\d+");
            //        int itemId = int.Parse(regex.Match(key).Value);
            //        string[] valueStrings = Request.Form[key].Split(',');
            //        foreach (var valueString in valueStrings)
            //        {
            //            var conditionId = int.Parse(valueString);
            //            Entity.SelectValue value = new Entity.SelectValue()
            //            {
            //                PicId = model.Id,
            //                ItemId = itemId,
            //                ConditionId = conditionId
            //            };
            //            selectValueHelper.Insert(value);
            //        }
            //    }
            //}
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
