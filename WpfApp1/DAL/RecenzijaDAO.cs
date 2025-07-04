using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class RecenzijaDAO
    {
        public static List<Recenzija> GetAll()
        {
            var list = new List<Recenzija>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM recenzija", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Recenzija
                {
                    KorisnikId = reader.GetInt32("KorisnikId"),
                    ProizvodId = reader.GetInt32("ProizvodId"),
                    Ocjena = reader.GetInt32("Ocjena"),
                    Komentar = reader["Komentar"].ToString(),
                    DatumRecenzije = reader.GetDateTime("Datum")
                });
            }
            return list;
        }
    }
}
