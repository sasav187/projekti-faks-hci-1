using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class KuponDAO
    {
        public static List<Kupon> GetAll()
        {
            var list = new List<Kupon>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM kupon", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Kupon
                {
                    Kod = reader["Kod"].ToString(),
                    Popust = reader.GetDecimal("Popust"),
                    VaziDo = reader.GetDateTime("VaziDo")
                });
            }
            return list;
        }

        public static Kupon? GetByKod(string kod)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM kupon WHERE Kod = @kod", conn);
            cmd.Parameters.AddWithValue("@kod", kod);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Kupon
                {
                    Kod = reader["Kod"].ToString(),
                    Popust = reader.GetDecimal("Popust"),
                    VaziDo = reader.GetDateTime("VaziDo")
                };
            }
            return null;
        }
    }
}
