using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIConsume.Models
{
    public partial class TblSumberBiayaSl
    {
        public TblSumberBiayaSl()
        {
            TblBiayaSlEksternal = new HashSet<TblBiayaSlEksternal>();
            TblBiayaSlInternal = new HashSet<TblBiayaSlInternal>();
        }
        [Required]
        public int IdStudiLanjut { get; set; }
        [Required]
        public int IdSumberBiaya { get; set; }
        [Required]
        public int SumberBiayaKe { get; set; }
        public int IdSumberBiayaSl { get; set; }

        public TblStudiLanjut IdStudiLanjutNavigation { get; set; }
        public RefSumberBiaya IdSumberBiayaNavigation { get; set; }
        public ICollection<TblBiayaSlEksternal> TblBiayaSlEksternal { get; set; }
        public ICollection<TblBiayaSlInternal> TblBiayaSlInternal { get; set; }
    }
}
