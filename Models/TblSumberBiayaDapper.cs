using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Models
{
    public class TblSumberBiayaDapper
    {
        
        public int id_studi_lanjut { get; set; }
        public string? sb { get; set; }
        public string? sbke { get; set; }
        public int id_sumber_biaya { get; set; }
        public int sumber_biaya_ke { get; set; }

        public decimal jumlah_dicairkan { get; set; }
        public DateTime tgl_approval_fakultas { get; set; }
        public int id_sumber_biaya_sl { get; set; }
        public bool is_internal { get; set; }


    }
}
