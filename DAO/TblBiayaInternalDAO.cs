using APIConsume.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.DAO
{
	public class TblBiayaInternalDAO
	{
		public List<TblSumberBiayaDapper> GetTblSumberBiaya(int idSL)
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

					string query = @"SELECT simka.TBL_SUMBER_BIAYA_SL.ID_STUDI_LANJUT, simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA_SL, simka.TBL_SUMBER_BIAYA_SL.SUMBER_BIAYA_KE, simka.REF_SUMBER_BIAYA.DESKRIPSI AS SB
FROM     simka.TBL_SUMBER_BIAYA_SL INNER JOIN
				  simka.REF_SUMBER_BIAYA ON simka.TBL_SUMBER_BIAYA_SL.ID_SUMBER_BIAYA = simka.REF_SUMBER_BIAYA.ID_SUMBER_BIAYA INNER JOIN
				  simka.REF_SUMBER_BIAYA AS REF_SUMBER_BIAYA_1 ON simka.TBL_SUMBER_BIAYA_SL.SUMBER_BIAYA_KE = REF_SUMBER_BIAYA_1.ID_SUMBER_BIAYA
									WHERE ID_STUDI_LANJUT = @idSL";


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
	}
}
