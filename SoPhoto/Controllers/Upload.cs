using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;

namespace SoPhoto.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/
        [HttpPost]
        public string Image()
        {
            if (System.Web.HttpContext.Current.Session["Admin"] == null)
            {
                return "nologin";
            }
            if (Request.Files.Count == 0) return null;

            HttpPostedFileBase file = Request.Files[0];
            DateTime dateTime = System.DateTime.Now;

            string directory = "Images\\" + string.Format("{0:yyyyMM}", dateTime);//this.Request.PhysicalApplicationPath + 

            RY.Common.DirectoryAndFile.CreateDirectory(directory);
            string fileExtent = RY.Common.DirectoryAndFile.GetFileExt(file.FileName);
            string datetimestring = dateTime.ToString("yyyyMMddHHmmssfff");
            // 文件名（时间前缀+扩展名）
            string filenewname = datetimestring +"."+ fileExtent;
            string fullPath = this.Request.PhysicalApplicationPath + directory + "/";
            string fullname = fullPath + filenewname;

            // 保存位置（服务器地址+文件夹地址+文件名）
            file.SaveAs(fullname);
            string coverName = fullPath + datetimestring + "_Cover" + "." + fileExtent;

            if (fileExtent.ToLower()=="zip")
            {
                int index = 0;
                IList<string> filenameStrings =new List<string>();
                using (ZipInputStream zis = new ZipInputStream(System.IO.File.Open(fullname, FileMode.Open)))
                {
                    ZipEntry entry = zis.GetNextEntry();
                    while (entry != null)
                    {
                        #region 搜索 jpg
                        string extent = RY.Common.DirectoryAndFile.GetFileExt(entry.Name).ToLower();
                        if (extent.Contains("jpg")||extent == "png")
                        {
                            string title = GetFileName(entry.Name,extent);
                            filenameStrings.Add(title);
                            string address = fullPath + datetimestring + "_" + index + "." + extent;
                            string cover = fullPath + datetimestring + "_" + index+"_Cover" + "." + extent;
                            FileStream writer = System.IO.File.Create(address);
                            //解压后的文件
                            int bufferSize = 1024*2; //缓冲区大小
                            int readCount = 0; //读入缓冲区的实际字节
                            byte[] buffer = new byte[bufferSize];
                            readCount = zis.Read(buffer, 0, bufferSize);
                            while (readCount > 0)
                            {
                                writer.Write(buffer, 0, readCount);
                                readCount = zis.Read(buffer, 0, bufferSize);
                            }
                            writer.Close();
                            writer.Dispose();
                            RY.Common.ImageHelp.LocalImage2Thumbs(address, cover, 256, 256, "WH");
                            index++;
                        }
                        #endregion
                        entry = zis.GetNextEntry();
                    }
                }
                Session["filenameStrings"] = filenameStrings;
                return "/Images/" + string.Format("{0:yyyyMM}", dateTime) + "/" + datetimestring + "_" + index + "." + fileExtent;
            }
            RY.Common.ImageHelp.LocalImage2Thumbs(fullname, coverName, 256, 256, "WH");
            return "/Images/" + string.Format("{0:yyyyMM}", dateTime) + "/" + filenewname;
        }

        private string GetFileName(string p, string extent)
        {
            int indexofX = p.LastIndexOf('/');
            indexofX = indexofX <= 0 ? 0 : indexofX;
            int indexofdot = p.LastIndexOf('.');
            int count = indexofX == 0 ? indexofdot - indexofX : indexofdot - indexofX - 1;
            int startindex = indexofX == 0 ? 0 : indexofX + 1;
            string result = p.Substring(startindex, count);
            return result + "." + extent;
        }

        [HttpPost]
        public string Video()
        {
            if (System.Web.HttpContext.Current.Session["Admin"] == null)
            {
                return "nologin";
            }
            if (Request.Files.Count == 0) return null;

            HttpPostedFileBase file = Request.Files[0];
            DateTime dateTime = System.DateTime.Now;

            string directory = "Video\\" + string.Format("{0:Y}", dateTime);//this.Request.PhysicalApplicationPath + 

            RY.Common.DirectoryAndFile.CreateDirectory(directory);

            // 文件名（时间前缀+扩展名）
            string filenewname = dateTime.ToString("yyyyMMddHHmmssfff.") + RY.Common.DirectoryAndFile.GetFileExt(file.FileName);
            // 保存位置（服务器地址+文件夹地址+文件名）
            file.SaveAs(this.Request.PhysicalApplicationPath + directory + "/" + filenewname);
            return "/Video/" + string.Format("{0:yyyyMM}", dateTime) + "/" + filenewname;
        }
        
    }
}