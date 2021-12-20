using APIConsume.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.DAO
{
	public class TblSumberBiayaSLDAO
	{
		public List<TblSumberBiayaDapper> GetTblSumberBiaya(int idSL)
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

//					string query = @"SELECT simka.TBL_SUMBER_BIAYA_SL.ID_STUDI_LANJUT, simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA_SL, simka.TBL_SUMBER_BIAYA_SL.SUMBER_BIAYA_KE, simka.REF_SUMBER_BIAYA.DESKRIPSI AS SB
//FROM     simka.TBL_SUMBER_BIAYA_SL INNER JOIN
//				  simka.REF_SUMBER_BIAYA ON simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA = simka.REF_SUMBER_BIAYA.ID_SUMBER_BIAYA
//									WHERE ID_STUDI_LANJUT = @idSL";

					string query = @"SELECT simka.TBL_BIAYA_SL.ID_BIAYA_SL_INT, simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL, simka.TBL_BIAYA_SL.TGL_APPROVAL_FAKULTAS, simka.TBL_BIAYA_SL.JUMLAH_DICAIRKAN, 
                  simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA, simka.TBL_SUMBER_BIAYA_SL.ID_STUDI_LANJUT, simka.REF_SUMBER_BIAYA.DESKRIPSI AS SB, simka.TBL_SUMBER_BIAYA_SL.SUMBER_BIAYA_KE, simka.REF_SUMBER_BIAYA.IS_INTERNAL
FROM     simka.TBL_BIAYA_SL INNER JOIN
                  simka.TBL_SUMBER_BIAYA_SL ON simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL = simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA_SL INNER JOIN
                  simka.TBL_STUDI_LANJUT ON simka.TBL_SUMBER_BIAYA_SL.ID_STUDI_LANJUT = simka.TBL_STUDI_LANJUT.ID_STUDI_LANJUT INNER JOIN
                  simka.REF_SUMBER_BIAYA ON simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA = simka.REF_SUMBER_BIAYA.ID_SUMBER_BIAYA
									WHERE simka.TBL_SUMBER_BIAYA_SL.ID_STUDI_LANJUT = @idSL";

					var param = new
					{
						idSL = idSL,
						
					};
					var data = conn.Query<TblSumberBiayaDapper>(query, param).ToList();

					//output.status = true;

					return data;
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					conn.Dispose();
				}
			}
		}

		public object GetTblSumberBiayaByIDSB(int idSB)
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{
					string query = @"SELECT  simka.TBL_BIAYA_SL.ID_BIAYA_SL_INT, simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL, simka.TBL_BIAYA_SL.TAHUN, simka.TBL_BIAYA_SL.SEMESTER, simka.TBL_BIAYA_SL.DESKRIPSI, 
				  simka.TBL_BIAYA_SL.TGL_PENGAJUAN, simka.TBL_BIAYA_SL.JUMLAH_PENGAJUAN, simka.MST_KARYAWAN.NAMA, simka.TBL_STUDI_LANJUT.NAMA_SEKOLAH, simka.TBL_STUDI_LANJUT.PRODI, 
				  simka.TBL_SUMBER_BIAYA_SL.SUMBER_BIAYA_KE, simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA, simka.TBL_BIAYA_SL.NO_BUKTI, simka.TBL_BIAYA_SL.TGL_TRANSFER, simka.TBL_BIAYA_SL.JUMLAH_DICAIRKAN, 
				  simka.TBL_BIAYA_SL.TGL_CAIR
FROM     simka.TBL_BIAYA_SL INNER JOIN
				  simka.TBL_SUMBER_BIAYA_SL ON simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL = simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA_SL INNER JOIN
				  simka.REF_SUMBER_BIAYA ON simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA = simka.REF_SUMBER_BIAYA.ID_SUMBER_BIAYA INNER JOIN
				  simka.TBL_STUDI_LANJUT ON simka.TBL_SUMBER_BIAYA_SL.ID_STUDI_LANJUT = simka.TBL_STUDI_LANJUT.ID_STUDI_LANJUT INNER JOIN
				  simka.MST_KARYAWAN ON simka.TBL_STUDI_LANJUT.NPP = simka.MST_KARYAWAN.NPP
WHERE  (simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA_SL = @idSB)";

					var param = new
					{
						idSB = idSB,

					};
					var data = conn.QuerySingleOrDefault<object>(query, param);

					//output.status = true;

					return data;
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					conn.Dispose();
				}
			}
		}

		public List<object> GetTblSumberBiayaButuhApproval()
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{
					string query = @"SELECT  simka.TBL_BIAYA_SL.ID_BIAYA_SL_INT, simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL, simka.TBL_BIAYA_SL.TAHUN, simka.TBL_BIAYA_SL.SEMESTER, simka.TBL_BIAYA_SL.DESKRIPSI, 
				  simka.TBL_BIAYA_SL.TGL_PENGAJUAN, simka.TBL_BIAYA_SL.JUMLAH_PENGAJUAN, simka.MST_KARYAWAN.NAMA, simka.TBL_STUDI_LANJUT.NAMA_SEKOLAH, simka.TBL_STUDI_LANJUT.PRODI, 
				  simka.TBL_SUMBER_BIAYA_SL.SUMBER_BIAYA_KE, simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA, simka.TBL_BIAYA_SL.NO_BUKTI, simka.TBL_BIAYA_SL.TGL_TRANSFER, simka.TBL_BIAYA_SL.JUMLAH_DICAIRKAN, 
				  simka.TBL_BIAYA_SL.TGL_CAIR
FROM     simka.TBL_BIAYA_SL INNER JOIN
				  simka.TBL_SUMBER_BIAYA_SL ON simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL = simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA_SL INNER JOIN
				  simka.REF_SUMBER_BIAYA ON simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA = simka.REF_SUMBER_BIAYA.ID_SUMBER_BIAYA INNER JOIN
				  simka.TBL_STUDI_LANJUT ON simka.TBL_SUMBER_BIAYA_SL.ID_STUDI_LANJUT = simka.TBL_STUDI_LANJUT.ID_STUDI_LANJUT INNER JOIN
				  simka.MST_KARYAWAN ON simka.TBL_STUDI_LANJUT.NPP = simka.MST_KARYAWAN.NPP
WHERE  (simka.REF_SUMBER_BIAYA.IS_INTERNAL = 1) AND (simka.TBL_BIAYA_SL.TGL_APPROVAL_FAKULTAS IS NULL)";

					
					var data = conn.Query(query).ToList();

					//output.status = true;

					return data;
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					conn.Dispose();
				}
			}
		}

		public List<object> GetTblSumberBiayaPencairanYSR()
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{
					string query = @"SELECT  simka.TBL_BIAYA_SL.ID_BIAYA_SL_INT, simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL, simka.TBL_BIAYA_SL.TAHUN, simka.TBL_BIAYA_SL.SEMESTER, simka.TBL_BIAYA_SL.DESKRIPSI, 
				  simka.TBL_BIAYA_SL.TGL_PENGAJUAN, simka.TBL_BIAYA_SL.JUMLAH_PENGAJUAN, simka.MST_KARYAWAN.NAMA, simka.TBL_STUDI_LANJUT.NAMA_SEKOLAH, simka.TBL_STUDI_LANJUT.PRODI, 
				  simka.TBL_SUMBER_BIAYA_SL.SUMBER_BIAYA_KE, simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA, simka.TBL_BIAYA_SL.NO_BUKTI, simka.TBL_BIAYA_SL.TGL_TRANSFER, simka.TBL_BIAYA_SL.JUMLAH_DICAIRKAN, 
				  simka.TBL_BIAYA_SL.TGL_CAIR
FROM     simka.TBL_BIAYA_SL INNER JOIN
				  simka.TBL_SUMBER_BIAYA_SL ON simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL = simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA_SL INNER JOIN
				  simka.REF_SUMBER_BIAYA ON simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA = simka.REF_SUMBER_BIAYA.ID_SUMBER_BIAYA INNER JOIN
				  simka.TBL_STUDI_LANJUT ON simka.TBL_SUMBER_BIAYA_SL.ID_STUDI_LANJUT = simka.TBL_STUDI_LANJUT.ID_STUDI_LANJUT INNER JOIN
				  simka.MST_KARYAWAN ON simka.TBL_STUDI_LANJUT.NPP = simka.MST_KARYAWAN.NPP
WHERE  (simka.REF_SUMBER_BIAYA.IS_INTERNAL = 1) AND (simka.TBL_BIAYA_SL.TGL_APPROVAL_FAKULTAS IS NOT NULL) AND (simka.TBL_BIAYA_SL.JUMLAH_DICAIRKAN IS NULL )";


					var data = conn.Query(query).ToList();

					//output.status = true;

					return data;
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					conn.Dispose();
				}
			}
		}

		public List<object> GetRekapPengajuanYSR()
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{
					string query = @"SELECT  simka.TBL_BIAYA_SL.ID_BIAYA_SL_INT, simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL, simka.TBL_BIAYA_SL.TAHUN, simka.TBL_BIAYA_SL.SEMESTER, simka.TBL_BIAYA_SL.DESKRIPSI, 
				  simka.TBL_BIAYA_SL.TGL_PENGAJUAN, simka.TBL_BIAYA_SL.JUMLAH_PENGAJUAN, simka.MST_KARYAWAN.NAMA, simka.TBL_STUDI_LANJUT.NAMA_SEKOLAH, simka.TBL_STUDI_LANJUT.PRODI, 
				  simka.TBL_SUMBER_BIAYA_SL.SUMBER_BIAYA_KE, simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA, simka.TBL_BIAYA_SL.NO_BUKTI, simka.TBL_BIAYA_SL.TGL_TRANSFER, simka.TBL_BIAYA_SL.JUMLAH_DICAIRKAN, 
				  simka.TBL_BIAYA_SL.TGL_CAIR
FROM     simka.TBL_BIAYA_SL INNER JOIN
				  simka.TBL_SUMBER_BIAYA_SL ON simka.TBL_BIAYA_SL.ID_SUMBER_BIAYA_SL = simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA_SL INNER JOIN
				  simka.REF_SUMBER_BIAYA ON simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA = simka.REF_SUMBER_BIAYA.ID_SUMBER_BIAYA INNER JOIN
				  simka.TBL_STUDI_LANJUT ON simka.TBL_SUMBER_BIAYA_SL.ID_STUDI_LANJUT = simka.TBL_STUDI_LANJUT.ID_STUDI_LANJUT INNER JOIN
				  simka.MST_KARYAWAN ON simka.TBL_STUDI_LANJUT.NPP = simka.MST_KARYAWAN.NPP
WHERE  (simka.REF_SUMBER_BIAYA.IS_INTERNAL = 1) AND (simka.TBL_BIAYA_SL.TGL_APPROVAL_FAKULTAS IS NOT NULL) AND (simka.TBL_BIAYA_SL.JUMLAH_DICAIRKAN IS NOT NULL )";


					var data = conn.Query(query).ToList();

					//output.status = true;

					return data;
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					conn.Dispose();
				}
			}
		}
		public async Task<DBOutput>  AddTblSumberBiaya(int id, int sumberBiaya, int sumberBiayaKe)
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

					string query = @"INSERT INTO [simka].[TBL_SUMBER_BIAYA_SL]
								   ([ID_STUDI_LANJUT]
								   ,[ID_SUMBER_BIAYA]
								   ,[SUMBER_BIAYA_KE])
							 VALUES
								   (@id, @sumberBiaya,@sumberBiayaKe)";


					var param = new
					{
						id = id,
						sumberBiaya = sumberBiaya,
						sumberBiayaKe = sumberBiayaKe,
					};
					output.data = conn.Execute(query, param);

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
		public DBOutput UpdateTblSumberBiaya(int idSL,int idSB, int sumberBiaya, int sumberBiayaKe)
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

					string query = @"UPDATE [simka].[TBL_SUMBER_BIAYA_SL]
									SET
								   
								   [ID_SUMBER_BIAYA] = @sumberBiaya
								   ,[SUMBER_BIAYA_KE] = @sumberBiayaKe
							 WHERE ID_SUMBER_BIAYA_SL = @idSB";


					var param = new
					{
						idSB = idSB,
						sumberBiaya = sumberBiaya,
						sumberBiayaKe = sumberBiayaKe,
					};
					output.data = conn.Execute(query, param);

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
		public DBOutput DeleteTblSumberBiaya(int idSB)
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

					string query = @"DELETE FROM [simka].[TBL_SUMBER_BIAYA_SL]									
							 WHERE ID_SUMBER_BIAYA_SL = @idSB";
					var param = new
					{
						idSB = idSB,
					
					};
					output.data = conn.Execute(query, param);

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
	}
}
