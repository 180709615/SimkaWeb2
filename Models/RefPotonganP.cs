using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIConsume.Models
{
    public partial class RefPotonganP
    {
        public RefPotonganP()
        {
            TblDetailPayrollPotongan = new HashSet<TblDetailPayrollPotongan>();
            TrHutangP = new HashSet<TrHutangP>();
        }

        public int IdRefPotongan { get; set; }
        [Required]
        public string NamaPotongan { get; set; }
        public decimal? Nominal { get; set; }
        public bool? IsTetap { get; set; }

        public ICollection<TblDetailPayrollPotongan> TblDetailPayrollPotongan { get; set; }
        public ICollection<TrHutangP> TrHutangP { get; set; }
    }
}
