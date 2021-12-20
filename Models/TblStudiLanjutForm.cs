using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Models
{
    public class TblStudiLanjutForm
    {
        public int IdStudiLanjut { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]
        public string Npp { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public int? IdRefJenjang { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]
        public string NamaSekolah { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]
        public string KotaSekolah { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string NegaraSekolah { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public DateTime? TglMulai { get; set; }
        public DateTime? TglLulus { get; set; }
        public DateTime? TglPenempatanKmbli { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string Fakultas { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string Prodi { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string DlmNegriLuarNegri { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string NoSkTugasBljr { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public int? TargetLulus { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public int? IdRefSs { get; set; }
        public int? IdSumberBiaya1 { get; set; }
        public int? IdSumberBiaya2 { get; set; }

        public string? hiddenIdSumberBiayaSL { get; set; }
        public string? hiddenSumberBiaya { get; set; }
        public string? hiddenSumberBiayaKe { get; set; }



        public IFormFile SK { get; set; }
        public byte[] SKm { get; set; }
        public IFormFile SkPenempatanKmbl { get; set; }
        public byte[] SkPenempatanKmblm { get; set; }

        public List<RefJenjang> listJenjang { get; set; }

        public List<RefSumberBiaya> listSumberBiaya { get; set; }

        public List<RefStatusStudi> listStatusStudi { get; set; }


        

    }
}
