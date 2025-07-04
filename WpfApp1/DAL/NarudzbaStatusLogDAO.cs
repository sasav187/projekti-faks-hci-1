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
    }
}
