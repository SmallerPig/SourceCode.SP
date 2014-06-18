using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.Entity
{
    public class SP_Pics : RY.Common.Console.IConsoleEntity
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Address { get; set; }

        public string Cover { get; set; }
        
        public string Author { get; set; }

        public string PicCode { get; set; }

        public string Affirming { get; set; }

        public string FileSize { get; set; }

        public string PicSize { get; set; }

        public DateTime CreateDate { get; set; }

        public string UseAndBuy { get; set; }

        public int BaseCategory { get; set; }

        public int CreativeType { get; set; }

        public string Class_Edit { get; set; }

        public int Class_Style { get; set; }



        public int Class_Location { get; set; }

        public int Class_Color { get; set; }

        public int Class_Layout { get; set; }

        public int Class_Place { get; set; }

        public int Class_Scene { get; set; }

        public int Class_Sex { get; set; }

        public string Class_Age { get; set; }

        public string KeyWords { get; set; }





        public bool IsLock { get; set; }
    }
}
