using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SoPhoto.BLL
{
    public class Pics : RY.Common.IConsoleListable<Entity.SP_Pics>
    {
        private DAL.Pics helper;

        public Pics()
        {
            helper = new DAL.Pics();
        }


        public void Delete(Entity.SP_Pics t)
        {
            helper.Delete(t);
            //TODO ：删除与之匹配的图片属性

        }

        public IEnumerable<Entity.SP_Pics> Filter(IEnumerable<KeyValuePair<string, string[]>> parms,int pageindex,int pagecount)
        {
            Expression<Func<Entity.SP_Pics, bool>> expression = GetFilter(parms);
            return helper.Filter(expression, pageindex, pagecount);
        }


        Expression<Func<Entity.SP_Pics, bool>> GetFilter(IEnumerable<KeyValuePair<string, string[]>> parms)
        {
            Expression<Func<Entity.SP_Pics, bool>> expression = PredicateBuilder.True<Entity.SP_Pics>();
            Expression<Func<Entity.SP_Pics, bool>> express = PredicateBuilder.False<Entity.SP_Pics>();//基础类别的两个特殊筛选需要特殊处理
            bool isHasBaseCategory = false;
            foreach (var keyValuePair in parms)
            {
                KeyValuePair<string, string[]> pair = keyValuePair;
                if (pair.Value != null)
                {
                    
                    int[] valus = pair.Value.ToList().Select(int.Parse).ToArray();
                    switch (keyValuePair.Key)
                    {
                        case "Category":
                            expression = expression.And(t => valus.Contains(t.BaseCategory));
                            break;
                        case "creativeType":
                            isHasBaseCategory = true;
                            express = express.Or(t => t.BaseCategory == 1 && valus.Contains(t.CreativeType));
                            break;
                        case "editType":
                            isHasBaseCategory = true;
                            express = express.Or(t => t.BaseCategory == 2 && pair.Value.Contains(t.Class_Edit));
                            break;
                        case "style":
                            expression = expression.And(t => valus.Contains(t.Class_Style));
                            break;
                        case "location":
                            expression = expression.And(t => valus.Contains(t.Class_Location));
                            break;
                        case "color":
                            expression = expression.And(t => valus.Contains(t.Class_Color));
                            break;
                        case "place":
                            expression = expression.And(t => valus.Contains(t.Class_Place));
                            break;
                        case "scene":
                            expression = expression.And(t => valus.Contains(t.Class_Scene));
                            break;
                        case "sex":
                            expression = expression.And(t => valus.Contains(t.Class_Sex));
                            break;
                        case "age":
                            expression = expression.And(t => pair.Value.Contains(t.Class_Age));
                            break;
                    }
                }
            }
            if (isHasBaseCategory)
            {
                expression = expression.And(express);
            }

            return expression;
        }

        static bool IsContains(string[] array, string content)
        {
            string[] contents = content.Split(',');
            return contents.Any(array.Contains);
        }




        public bool IsContent(Entity.SP_Pics pics)
        {
            return true;
        }





        public Entity.SP_Pics GetById(int id)
        {
            return helper.GetById(id);
        }

        public int GetCount(bool islocked)
        {
            return helper.GetCount(islocked);
        }

        public IList<Entity.SP_Pics> GetList(int from, int count, bool isLock)
        {
            return helper.GetList(from, count, isLock);
        }

        public Entity.SP_Pics Insert(Entity.SP_Pics t)
        {
            t.KeyWords = ProcessKeyWord(t.KeyWords);
            if (string.IsNullOrEmpty(t.PicCode))
            { 
                t.PicCode = CreateCode(t);            
            }
            return helper.Insert(t);
        }


        /// <summary>
        /// 处理关键字逻辑，使每个关键字都已;结束
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string ProcessKeyWord(string p)
        {
            p = p.Trim();
            if (p.LastIndexOfAny(new char[] { ';' }) < p.Length - 1)
                return p + ";";
            return p;
        }

        private string CreateCode(Entity.SP_Pics t,int index=0)
        {
            return System.DateTime.Now.ToString("yyMMddhhmmssfff");
        }

        public void Restore(Entity.SP_Pics t)
        {
            helper.Restore(t);
        }

        public void ToRecycle(Entity.SP_Pics t)
        {
            helper.ToRecycle(t);
        }

        public void Update(Entity.SP_Pics t)
        {
            t.KeyWords = ProcessKeyWord(t.KeyWords);
            helper.Update(t);
        }

        public int GetCount()
        {
            return helper.GetCount();
        }

        public IList<Entity.SP_Pics> GetList(int p, int pageSize)
        {
            return helper.ConsoleList(p, pageSize);
        }



        public IList<Entity.SP_Pics> SearchByPicCode(string keyword)
        {
            return helper.SearchByPicCode(keyword);
        }

        public Entity.SP_Pics GetByCode(string code)
        {
            return helper.GetByCode(code);
        }

        public Entity.SP_Pics GetByPicCode(string pic)
        {
            return helper.GetByPicCode(pic);
        }

        public IEnumerable<Entity.SP_Pics> Filter(IEnumerable<Entity.SP_Pics> piclist, IEnumerable<KeyValuePair<string, string[]>> parms,int pageIndex=1,int pageSize =20)
        {
            Expression<Func<Entity.SP_Pics, bool>> expression = GetFilter(parms);
            return piclist.AsQueryable().Where(expression);
        }

        public int CountByFilter(IEnumerable<KeyValuePair<string, string[]>> parms)
        {
            Expression<Func<Entity.SP_Pics, bool>> expression = GetFilter(parms);
            return helper.CountByFilter(expression);
        }

        public IEnumerable<Entity.SP_Pics> SearchByKeyWord(string keyword)
        {
            //Expression<Func<Entity.SP_Pics, bool>> expression = GetFilter(parms);
            //expression.And(item => item.IsLock == false && (item.Title.Contains(keyword) || item.Summary.Contains(keyword) || item.KeyWords.Contains(keyword + ";")));
            return helper.SearchByKeyword(keyword);
        }

        public int CountByKeyWord(string keyword, IEnumerable<KeyValuePair<string, string[]>> parms)
        {
            Expression<Func<Entity.SP_Pics, bool>> expression = GetFilter(parms);
            expression.And(item => item.IsLock == false && (item.Title.Contains(keyword) || item.Summary.Contains(keyword) || item.KeyWords.Contains(keyword + ";")));
            return helper.CountByKeyWord(expression);
        }
    }




}
