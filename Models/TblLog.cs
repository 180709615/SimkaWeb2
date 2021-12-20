using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Models
{
    public class TblLog
    {
        public string Npp { get; set; }
        public int IdSiSubmenu { get; set; }
        public string Aksi { get; set; }
        public DateTime WaktuAksi { get; set; }

    }
}
