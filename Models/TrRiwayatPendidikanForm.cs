using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIConsume.Models
{
    public partial class TrRiwayatPendidikanForm
    {
        public TrRiwayatPendidikanForm()
        {
            TrRiwayatPendidikanDokumen = new HashSet<TrRiwayatPendidikanDokumen>();
        }

        public int IdTrRp { get; set; }
        [Required(ErrorMessage ="Field tidak boleh kosong")]
        public int? IdRefJenjang { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]
        public string Npp { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]
        public int? IdRefSs { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]
        public string NamaSekolah { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string NoIjazah { get; set; }

        [Required(ErrorMessage = "Field tidak boleh kosong")]
        [Range(1900, 2100, ErrorMessage = "Harap masukkan tahun yang valid")]
     
        public int? TahunMasuk { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]
        [Range(1900, 2100, ErrorMessage = "Harap masukkan tahun yang valid")]
        public int? TahunLulus { get; set; }

        [Required(ErrorMessage = "Field tidak boleh kosong")]
        //[Range(0, 4, ErrorMessage = "IPK hanya 0 hingga 4")]

        public double? Ipk { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string Gelar { get; set; }
        public string Keterangan { get; set; }

       

        public string AsalBeasiswa { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string Fakultas { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string Jurusan { get; set; }
        [Required(ErrorMessage = "Field tidak boleh kosong")]

        public string ProgramStudi { get; set; }
        public byte[] ScanIjazah { get; set; }
        public byte[] ScanTranskrip { get; set; }
        public IFormFile ScanIjazahForm { get; set; }
        public IFormFile ScanTranskripForm { get; set; }
        public DateTime TglIjazah { get; set; }
        public int? IsLast { get; set; }
        public string JenisFileIjazah { get; set; }
        public string JenisFileTranskrip { get; set; }
        public DateTime? DateInserted { get; set; }
        public bool? IsVerifiedKsdm { get; set; }

        public List<MstKaryawan> karyawan { get; set; }
        public List<RefJenjang> jenjang { get; set; }
        public List<RefStatusStudi> StatusStudi { get; set; }



        public RefJenjang IdRefJenjangNavigation { get; set; }
        public RefStatusStudi IdRefSsNavigation { get; set; }
        public MstKaryawan NppNavigation { get; set; }
        public ICollection<TrRiwayatPendidikanDokumen> TrRiwayatPendidikanDokumen { get; set; }
    }
}
