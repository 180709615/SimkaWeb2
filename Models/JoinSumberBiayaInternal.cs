using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Models
{
    public class JoinSumberBiayaInternal
    {
        public int IdBiayaSlInt { get; set; }
        public int? IdSumberBiayaSl { get; set; }
        public string Tahun { get; set; }
        public string Semester { get; set; }
        public string Deskripsi { get; set; }
        public DateTime? TglPengajuan { get; set; }
        public decimal? JumlahPengajuan { get; set; }
        public DateTime? TglApprovalFakultas { get; set; }
        public DateTime? TglCair { get; set; }
        public decimal? JumlahDicairkan { get; set; }
        public DateTime? TglTransfer { get; set; }

        public byte[] NO_BUKTI { get; set; }

        public string buktiBase64 { get; set; }
    }
}
