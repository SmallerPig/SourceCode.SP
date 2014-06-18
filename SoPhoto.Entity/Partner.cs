using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.Entity
{
    public class Partner :RY.Common.Console.IConsoleEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public bool IsLock { get; set; }
    }
}
