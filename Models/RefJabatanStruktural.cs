using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIConsume.Models
{
    public partial class RefJabatanStruktural
    {
        public RefJabatanStruktural()
        {
            MstUnit = new HashSet<MstUnit>();
             MstPayrolls= new HashSet<MstTarifPayroll>();  //tambahan
            RefJabatanStrukturals = new HashSet<TrKarirStruktural>();
        }

        public int IdRefStruktural { get; set; }
        [Required]
        public string Deskripsi { get; set; }
        public int? SetaraSks { get; set; }
        public int? KelasAsuransi { get; set; }

        public ICollection<MstUnit> MstUnit { get; set; }
        public ICollection<MstTarifPayroll> MstPayrolls { get; set; } //tambahan
        public ICollection<TrKarirStruktural> RefJabatanStrukturals { get; set; } //tambahan


    }
}
