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

        public static void Insert(Recenzija recenzija)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("INSERT INTO recenzija (KorisnikId, ProizvodId, Ocjena, Komentar, Datum)" +
                                       "VALUES (@kid, @pid, @ocj, @kom, @dat)", conn);

            cmd.Parameters.AddWithValue("@kid", recenzija.KorisnikId);
            cmd.Parameters.AddWithValue("@pid", recenzija.ProizvodId);
            cmd.Parameters.AddWithValue("@ocj", recenzija.Ocjena);
            cmd.Parameters.AddWithValue("@kom", recenzija.Komentar);
            cmd.Parameters.AddWithValue("@dat", recenzija.DatumRecenzije);

            cmd.ExecuteNonQuery();
        }
    }
}