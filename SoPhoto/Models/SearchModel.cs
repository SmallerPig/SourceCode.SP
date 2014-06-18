using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoPhoto.Models
{
    public class SearchModel
    {
        public IList<ColumnModel> Column { get; set; }

        public IEnumerable<Entity.SP_Pics> Pics { get; set; }
    }
}