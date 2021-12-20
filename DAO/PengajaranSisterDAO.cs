using APIConsume.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIConsume.DAO
{
    public class PengajaranSisterDAO
    {
        public DBOutput GetPengajaranTrPengajaran(string npp, string id_semester)
        {
            using (SqlConnection conn = new SqlConnection(Connection.DataSisterConnection))
            {
                DBOutput output = new DBOutput();
                try
                {
                    //string query = @"SELECT *
                    //                FROM     PUBLIKASI INNER JOIN PENULIS ON PUBLIKASI.id_riwayat_publikasi_paten = PENULIS.id_riwayat_publikasi_paten
                    //                WHERE(PENULIS.id_sdm = @id_dosen)            ";
                    string query = @"SELECT *
FROM     PENGAJARAN 
WHERE  (PENGAJARAN.NPP = @npp) AND PENGAJARAN.id_semester = @id_semester";
                    var param = new { npp = npp , id_semester = id_semester};
                    output.status = true;
                    output.data = conn.Query<TrPengajaran_Data_SISTER>(query, param).ToList();

                    return output;
                }
                catch (Exception ex)
                {
                    output.status = true;
                    output.data = null;
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
