using APIConsume.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.DAO
{
	public class TblStudiLanjutDAO
	{
		DBOutput output = new DBOutput();
		public DBOutput GetAllDataStudiLanjut()
		{
			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{
					
					string query = @"SELECT simka.TBL_STUDI_LANJUT.ID_STUDI_LANJUT, simka.TBL_STUDI_LANJUT.NPP, simka.TBL_STUDI_LANJUT.ID_REF_JENJANG, simka.TBL_STUDI_LANJUT.NAMA_SEKOLAH, simka.TBL_STUDI_LANJUT.KOTA_SEKOLAH, 
				  simka.TBL_STUDI_LANJUT.NEGARA_SEKOLAH, simka.TBL_STUDI_LANJUT.TGL_MULAI, simka.TBL_STUDI_LANJUT.TGL_LULUS, simka.TBL_STUDI_LANJUT.SK, simka.TBL_STUDI_LANJUT.TGL_PENEMPATAN_KMBLI, 
				  simka.TBL_STUDI_LANJUT.SK_PENEMPATAN_KMBL, simka.TBL_STUDI_LANJUT.FAKULTAS, simka.TBL_STUDI_LANJUT.PRODI, simka.TBL_STUDI_LANJUT.TARGET_LULUS, simka.TBL_STUDI_LANJUT.ID_REF_SS, 
				  simka.REF_JENJANG.DESKRIPSI AS Jenjang, simka.TBL_STUDI_LANJUT.DLM_NEGRI_LUAR_NEGRI AS DalamAtauLuarNegeri, simka.TBL_STUDI_LANJUT.NO_SK_TUGAS_BLJR, 
				   simka.MST_KARYAWAN.NAMA, REF_JENJANG_1.DESKRIPSI AS JENJANG, 
				  simka.REF_STATUS_STUDI.DESKRIPSI AS STATUS_STUDI
FROM     simka.TBL_STUDI_LANJUT INNER JOIN
				  simka.REF_JENJANG ON simka.TBL_STUDI_LANJUT.ID_REF_JENJANG = simka.REF_JENJANG.ID_REF_JENJANG INNER JOIN
				  simka.MST_KARYAWAN ON simka.TBL_STUDI_LANJUT.NPP = simka.MST_KARYAWAN.NPP INNER JOIN
				  simka.REF_JENJANG AS REF_JENJANG_1 ON simka.TBL_STUDI_LANJUT.ID_REF_JENJANG = REF_JENJANG_1.ID_REF_JENJANG INNER JOIN
				  simka.REF_STATUS_STUDI ON simka.TBL_STUDI_LANJUT.ID_REF_SS = simka.REF_STATUS_STUDI.ID_REF_SS";

					//CASE WHEN simka.TBL_STUDI_LANJUT.DLM_NEGRI_LUAR_NEGRI LIKE 'L' THEN 'Luar Negeri' ELSE 'Dalam Negeri' END AS TEXT_DALAM_LUAR_NEGERI,

					output.status = true;
					output.data = conn.Query<object>(query).ToList();

					return output;
				}
				catch (Exception ex)
				{
					output.status = false;
					output.pesan = ex.Message;
					output.data = new List<string>();
					return output;
				}
				finally
				{
					conn.Dispose();
				}
			}
		}
		public DBOutput GetDataStudiLanjutByCriteria(string npp, string? nama, int? idJenjang, int? idSumberBiaya, string? Universitas, string? Fakultas, string? ProgramStudi, string? Kota, string? Negara, DateTime? tanggalMulai, DateTime? tanggalLulus, int? targetLulus, string? DalamAtauLuarNegeri, int? idRefSs)
		{
			DBOutput output = new DBOutput();
			var kondisi = "";
			

			if (!String.IsNullOrEmpty(npp))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.NPP LIKE @npp ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.NPP LIKE @npp ";

			}
			if (!String.IsNullOrEmpty(nama))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.MST_KARYAWAN.NAMA LIKE @nama ";
				else
					kondisi = kondisi + "AND simka.MST_KARYAWAN.NAMA LIKE @nama ";

			}

			if (idJenjang != 0) // Menggunakan exception karena bisa saja parameter tersebut tidak diisi
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.ID_REF_JENJANG = @idJenjang ";
				else
					kondisi = kondisi + " AND simka.TBL_STUDI_LANJUT.ID_REF_JENJANG = @idJenjang  ";

			}

			if (!String.IsNullOrEmpty(Universitas))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.NAMA_SEKOLAH LIKE @Universitas ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.NAMA_SEKOLAH LIKE @Universitas ";

			}

			if (!String.IsNullOrEmpty(Fakultas))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.FAKULTAS LIKE @Fakultas ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.FAKULTAS LIKE @Fakultas ";

			}
			if (!String.IsNullOrEmpty(ProgramStudi))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.PRODI LIKE @ProgramStudi ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.PRODI LIKE @ProgramStudi ";

			}

			if (!String.IsNullOrEmpty(Kota))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.KOTA_SEKOLAH LIKE @Kota ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.KOTA_SEKOLAH LIKE @Kota ";

			}
			if (!String.IsNullOrEmpty(Negara))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.NEGARA_SEKOLAH LIKE @Negara ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.NEGARA_SEKOLAH LIKE @Negara ";

			}
			if (tanggalMulai!= default(DateTime))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.TGL_MULAI = @tanggalMulai ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.TGL_MULAI = @tanggalMulai ";

			}
			if (tanggalLulus != default(DateTime))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.TGL_LULUS = @tanggalLulus ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.TGL_LULUS = @tanggalLulus ";

			}
			if (targetLulus != 0)
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.TARGET_LULUS = @targetLulus ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.TARGET_LULUS = @targetLulus ";

			}
			if (!String.IsNullOrEmpty(DalamAtauLuarNegeri))
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.DLM_NEGRI_LUAR_NEGRI LIKE @DalamAtauLuarNegeri ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.DLM_NEGRI_LUAR_NEGRI LIKE @DalamAtauLuarNegeri ";

			}
			if (idRefSs!= 0)
			{
				if (String.IsNullOrEmpty(kondisi))
					kondisi = " WHERE simka.TBL_STUDI_LANJUT.ID_REF_SS LIKE @idRefSs ";
				else
					kondisi = kondisi + "AND simka.TBL_STUDI_LANJUT.ID_REF_SS LIKE @idRefSs ";

			}


			//if (idSumberBiaya != 0) // Menggunakan exception karena bisa saja parameter tersebut tidak diisi
			//{
			//	if (String.IsNullOrEmpty(kondisi))
			//		kondisi = " WHERE ID_SUMBER_BIAYA = @idSumberBiaya ";
			//	else
			//		kondisi = kondisi + " AND ID_REF_JENJANG = @idSumberBiaya  ";

			//}
			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

					//string query = @" SELECT mstKaryawan.NAMA, idUnit.NAMA_UNIT, mstIdUnit.NAMA_UNIT AS PENEMPATAN, fungsional.DESKRIPSI AS FUNGSIONAL, idUnitAkademik.NAMA_UNIT_AKADEMIK, mstKaryawan.NPP" + selectJenjang + @" FROM  [simka].[MST_KARYAWAN] AS mstKaryawan INNER JOIN[simka].[REF_FUNGSIONAL] AS fungsional ON mstKaryawan.ID_REF_FUNGSIONAL = fungsional.ID_REF_FUNGSIONAL" + joinRiwayatPendidikan + @" LEFT OUTER JOIN
					//				[siatma].[MST_UNIT_AKADEMIK] AS idUnitAkademik ON mstKaryawan.ID_UNIT_AKADEMIK = idUnitAkademik.ID_UNIT LEFT OUTER JOIN [siatmax].[MST_UNIT] AS mstIdUnit ON mstKaryawan.MST_ID_UNIT = mstIdUnit.ID_UNIT LEFT OUTER JOIN [siatmax].[MST_UNIT] AS idUnit ON mstKaryawan.ID_UNIT = idUnit.ID_UNIT" + kondisi;
					string query = @"SELECT simka.TBL_STUDI_LANJUT.ID_STUDI_LANJUT, simka.TBL_STUDI_LANJUT.NAMA_SEKOLAH, simka.TBL_STUDI_LANJUT.PRODI,
				  simka.REF_JENJANG.DESKRIPSI AS Jenjang, simka.TBL_STUDI_LANJUT.DLM_NEGRI_LUAR_NEGRI AS DalamAtauLuarNegeri, simka.TBL_STUDI_LANJUT.NO_SK_TUGAS_BLJR, 
				   simka.MST_KARYAWAN.NAMA, REF_JENJANG_1.DESKRIPSI AS JENJANG, 
				  simka.REF_STATUS_STUDI.DESKRIPSI AS STATUS_STUDI
FROM     simka.TBL_STUDI_LANJUT INNER JOIN
				  simka.REF_JENJANG ON simka.TBL_STUDI_LANJUT.ID_REF_JENJANG = simka.REF_JENJANG.ID_REF_JENJANG INNER JOIN
				  simka.MST_KARYAWAN ON simka.TBL_STUDI_LANJUT.NPP = simka.MST_KARYAWAN.NPP INNER JOIN
				  simka.REF_JENJANG AS REF_JENJANG_1 ON simka.TBL_STUDI_LANJUT.ID_REF_JENJANG = REF_JENJANG_1.ID_REF_JENJANG INNER JOIN
				  simka.REF_STATUS_STUDI ON simka.TBL_STUDI_LANJUT.ID_REF_SS = simka.REF_STATUS_STUDI.ID_REF_SS" + kondisi;
					var param = new { npp = npp, nama = nama,   idJenjang = idJenjang, idSumberBiaya = idSumberBiaya, Universitas = Universitas, Fakultas = Fakultas, ProgramStudi = ProgramStudi, Kota = Kota, Negara = Negara, tanggalMulai = tanggalMulai, tanggalLulus = tanggalLulus, targetLulus = targetLulus, DalamAtauLuarNegeri = DalamAtauLuarNegeri, idRefSs = idRefSs };
					output.data = conn.Query<object>(query, param).ToList();

					output.status = true;

					return output;
				}
				catch (Exception ex)
				{
					output.status = false;
					output.pesan = ex.Message;
					output.data = new List<string>();
					return output;
				}
				finally
				{
					conn.Dispose();
				}
			}
		}

		public DBOutput GetListUniversitas()
		{
			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

					string query = @"SELECT DISTINCT NAMA_SEKOLAH
									FROM     simka.TBL_STUDI_LANJUT
									ORDER BY NAMA_SEKOLAH";

					//CASE WHEN simka.TBL_STUDI_LANJUT.DLM_NEGRI_LUAR_NEGRI LIKE 'L' THEN 'Luar Negeri' ELSE 'Dalam Negeri' END AS TEXT_DALAM_LUAR_NEGERI,

					output.status = true;
					output.data = conn.Query<string>(query).ToList();

					return output;
				}
				catch (Exception ex)
				{
					output.status = false;
					output.pesan = ex.Message;
					output.data = new List<string>();
					return output;
				}
				finally
				{
					conn.Dispose();
				}
			}
		}



	}
}
//
//