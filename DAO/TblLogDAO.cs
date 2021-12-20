using APIConsume.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace APIConsume.DAO
{
	public class TblLogDAO
	{
		public DBOutput AddTblLog(string npp, int idsisubmenu, string aksi)
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

					string query = @"INSERT INTO [siatmax].[TBL_LOG]
								   ([NPP]
								   ,[ID_SI_SUBMENU]
								   ,[AKSI]
								   ,[WAKTU_AKSI])
							 VALUES
								   (@npp, @idSiSubmenu,@aksi,@waktu)";


					var param = new
					{
						npp = npp,
						idSiSubmenu = idsisubmenu,
						aksi = aksi,
						waktu = DateTime.Now


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

		public DateTime GetLastUpdated(string aksi, string npp)
		{
			DBOutput output = new DBOutput();

			using (SqlConnection conn = new SqlConnection(Connection.ConnectionStringg))
			{
				try
				{

					string query = @"SELECT WAKTU_AKSI FROM [siatmax].[TBL_LOG]
								   WHERE NPP = @npp AND aksi = @aksi ORDER BY WAKTU_AKSI DESC";


					var param = new
					{
						npp = npp,
						aksi = aksi,

					};
					DateTime date = conn.QueryFirstOrDefault<DateTime>(query, param);

					output.status = true;

					return date;
				}
				catch (Exception ex)
				{
					output.status = false;
					output.pesan = ex.Message;
					output.data = new List<string>();
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
