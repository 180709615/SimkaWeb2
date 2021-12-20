using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Models
{
    public class TblSumberBiayaForm
    {
        public int id_studi_lanjut { get; set; }

        public int id_sumber_biaya_sl { get; set; }

        public int id_sumber_biaya { get; set; }
        //[Required]
        public int sumber_biaya_ke { get; set; }

        public string npp { get; set; }

        // TBL SUMBER BIAYA SL

        public int Tahun { get; set; }
        public string Semester { get; set; }
        public string Deskripsi { get; set; }
        public DateTime TglPengajuan { get; set; }
        public double JumlahPengajuan { get; set; }

        public DateTime TglTransfer { get; set; }

        public IFormFile Bukti { get; set; }

        public byte[] BuktiPreview { get; set; }







        public List<RefSumberBiaya> listSumberBiaya { get; set; }

    }
}
