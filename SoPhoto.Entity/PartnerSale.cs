using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.Entity
{
    public class PartnerSale :RY.Common.Console.IConsoleEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int PartnerId { get; set; }

        public string PicCode { get; set; }

        public DateTime SaleTime { get; set; }

        public double Price { get; set; }

        public bool IsLock { get; set; }
    }
}
