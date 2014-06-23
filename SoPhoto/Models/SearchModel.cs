using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoPhoto.Models
{
    public class SearchModel
    {
        public IEnumerable<Entity.SP_Pics> Pics { get; set; }
    }
}