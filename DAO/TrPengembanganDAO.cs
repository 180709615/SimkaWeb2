using APIConsume.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConsume.DAO
{
	public class TrPengembanganDAO
	{
		public DBOutput GetTrPengembangan()
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

					string query = @"SELECT LEF.ID_TR_PENGEMBANGAN, LEF.NPP, LEF.ID_REF_APPRAISAL, LEF.ID_REF_PENGEMBANGAN, LEF.JUDUL, simka.REF_JENIS_APPRAISAL.DESKRIPSI AS apr, simka.REF_PENGEMBANGAN.DESKRIPSI AS refPe
FROM     simka.TR_PENGEMBANGAN AS LEF INNER JOIN
				  simka.REF_JENIS_APPRAISAL ON LEF.ID_REF_APPRAISAL = simka.REF_JENIS_APPRAISAL.ID_REF_JNS_APPRAISAL LEFT OUTER JOIN
				  simka.REF_PENGEMBANGAN ON LEF.ID_REF_PENGEMBANGAN = simka.REF_PENGEMBANGAN.ID_REF_PENGEMBANGAN";


					output.data = conn.Query(query).ToList();

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
