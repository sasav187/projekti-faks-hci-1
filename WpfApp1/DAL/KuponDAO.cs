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

        public static void Insert(Kupon kupon)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO kupon (Kod, Popust, VaziDo) VALUES (@kod, @popust, @vazido)", conn);
            cmd.Parameters.AddWithValue("@kod", kupon.Kod);
            cmd.Parameters.AddWithValue("@popust", kupon.Popust);
            cmd.Parameters.AddWithValue("@vazido", kupon.VaziDo);
            cmd.ExecuteNonQuery();
        }
    }
}
