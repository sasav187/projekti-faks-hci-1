using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class NarudzbaStatusLogDAO
    {
        public static List<NarudzbaStatusLog> GetAll()
        {
            var list = new List<NarudzbaStatusLog>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM narudzbastatuslog", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new NarudzbaStatusLog
                {
                    Id = reader.GetInt32("Id"),
                    NarudzbaId = reader.GetInt32("NarudzbaId"),
                    StariStatus = reader["StariStatus"].ToString(),
                    NoviStatus = reader["NoviStatus"].ToString(),
                    DatumPromjeneStatusa = reader.GetDateTime("DatumPromjene")
                });
            }
            return list;
        }

        public static void Insert(NarudzbaStatusLog log)
        {
            var lastLog = GetLastForOrder(log.NarudzbaId); 
            if (lastLog != null && lastLog.NoviStatus == log.NoviStatus)
                return;

            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO narudzbastatuslog (NarudzbaId, StariStatus, NoviStatus, DatumPromjene) " +
                                       "VALUES (@nid, @stari, @novi, @datum)", conn);
            cmd.Parameters.AddWithValue("@nid", log.NarudzbaId);
            cmd.Parameters.AddWithValue("@stari", log.StariStatus);
            cmd.Parameters.AddWithValue("@novi", log.NoviStatus);
            cmd.Parameters.AddWithValue("@datum", log.DatumPromjeneStatusa);
            cmd.ExecuteNonQuery();
        }

        private static NarudzbaStatusLog GetLastForOrder(int narudzbaId)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM narudzbastatuslog " +
                "WHERE NarudzbaId = @nid ORDER BY DatumPromjene DESC LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@nid", narudzbaId);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new NarudzbaStatusLog
                {
                    Id = reader.GetInt32("Id"),
                    NarudzbaId = reader.GetInt32("NarudzbaId"),
                    StariStatus = reader["StariStatus"].ToString(),
                    NoviStatus = reader["NoviStatus"].ToString(),
                    DatumPromjeneStatusa = reader.GetDateTime("DatumPromjene")
                };
            }
            return null;
        }
    }
}
