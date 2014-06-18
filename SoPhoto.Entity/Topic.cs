using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoPhoto.Entity
{
    public class SP_Topic :RY.Common.Console.IConsoleEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Cover { get; set; }

        public string Summary { get; set; }

        public string PicsCode { get; set; }

        public bool IsLock { get; set; }
    }
}
