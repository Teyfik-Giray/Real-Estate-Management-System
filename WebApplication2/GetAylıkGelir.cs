using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication2
{
    public class GetAylıkGelir
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["EmlakEntitiesADO"].ConnectionString;

        public List<TBL_aylık_gelir> GetVotes(int konutSahibiId)
        {
            List<TBL_aylık_gelir> votes = new List<TBL_aylık_gelir>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM TBL_aylık_gelir WHERE konut_sahibi = @KonutSahibiId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@KonutSahibiId", konutSahibiId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        votes.Add(new TBL_aylık_gelir
                        {
                            konut_sahibi = reader.GetInt32(reader.GetOrdinal("konut_sahibi")),
                            Ocak = reader.GetDecimal(reader.GetOrdinal("Ocak")),
                            Subat = reader.GetDecimal(reader.GetOrdinal("Subat")),
                            Mart = reader.GetDecimal(reader.GetOrdinal("Mart")),
                            Nisan = reader.GetDecimal(reader.GetOrdinal("Nisan")),
                            Mayis = reader.GetDecimal(reader.GetOrdinal("Mayis")),
                            Haziran = reader.GetDecimal(reader.GetOrdinal("Haziran")),
                            Temmuz = reader.GetDecimal(reader.GetOrdinal("Temmuz")),
                            Agustos = reader.GetDecimal(reader.GetOrdinal("Agustos")),
                            Eylul = reader.GetDecimal(reader.GetOrdinal("Eylul")),
                            Ekim = reader.GetDecimal(reader.GetOrdinal("Ekim")),
                            Kasim = reader.GetDecimal(reader.GetOrdinal("Kasim")),
                            Aralik = reader.GetDecimal(reader.GetOrdinal("Aralik"))
                        });
                    }
                }
            }

            return votes;
        }
    }
}
