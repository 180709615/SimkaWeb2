using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.Models
{
    public class RiwayatPendidikanSisterDetail
    {
        public string id { get; set; }
        public string kategori_kegiatan { get; set; }
        public string jenjang_pendidikan { get; set; }
        public string gelar_akademik { get; set; }
        public string id_sdm { get; set; }
        public int id_gelar_akademik { get; set; }
        public string id_program_studi { get; set; }
        public string bidang_studi { get; set; }
        public string nama_program_studi { get; set; }
        public string nama_perguruan_tinggi { get; set; }
        public int tahun_masuk { get; set; }
        public int tahun_lulus { get; set; }
        public object tanggal_lulus { get; set; }
        public string nomor_induk { get; set; }
        public int jumlah_semester { get; set; }
        public int jumlah_sks { get; set; }
        public int ipk { get; set; }
        public string sk_penyetaraan { get; set; }
        public int id_bidang_studi { get; set; }
        public int id_jenjang_pendidikan { get; set; }
        public string tanggal_sk_penyetaraan { get; set; }
        public string nomor_ijazah { get; set; }
        public string judul_tugas_akhir { get; set; }

        public string message { get; set; }
        public string detail { get; set; }

        //public object[] dokumen { get; set; }
    }
}
