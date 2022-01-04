using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIConsume.Models
{
    public partial class RefTrAkademik
    {
        [Required]
        public int IdRefTrAkademik { get; set; }
        public string Deskripsi { get; set; }
    }
}
