using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIConsume.Models
{
    public partial class TrKarirStrukturalForm
    {
        [Required]
        public string Npp { get; set; }
        [Required]
        public string NoSk { get; set; }
        [Required]
        public int? IdUnit { get; set; }
        public int? MstIdUnit { get; set; }
        [Required]
        public int? IdRefStruktural { get; set; }
        [Required]
        public DateTime? TglAwal { get; set; }
        [Required]
        public DateTime? TglAkhir { get; set; }
        [Required]
        public string Status { get; set; }
        public int IsLast { get; set; }

      
        public MstUnit IdUnitNavigation { get; set; }     
        public MstKaryawan NppNavigation { get; set; }
        public RefJabatanStruktural IdRefStrukturalNavigation { get; set; }

        public List<MstKaryawan> karyawan { get; set; }
        public List<RefGolongan> golongan { get; set; }
        public List<MstUnit> unit { get; set; }
        public List<RefJabatanStruktural> struktural { get; set; }
    }
}
