using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class NarudzbaDAO
    {
        public static List<Narudzba> GetAll()
        {
            var list = new List<Narudzba>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM narudzba", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Narudzba
                {
                    Id = reader.GetInt32("Id"),
                    KorisnikId = reader.GetInt32("KorisnikId"),
                    AdresaId = reader.GetInt32("AdresaId"),
                    DatumNarudzbe = reader.GetDateTime("Datum"),
                    UkupnaCijena = reader.GetDecimal("UkupnaCijena"),
                    Status = reader["Status"].ToString()
                });
            }
            return list;
        }

        public static void Insert(Narudzba narudzba)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("INSERT INTO narudzba (KorisnikId, AdresaId, Datum, UkupnaCijena, Status)" +
                                       " VALUES (@kid, @aid, @datum, @ukup, @stat)", conn);
            cmd.Parameters.AddWithValue("@kid", narudzba.KorisnikId);
            cmd.Parameters.AddWithValue("@aid", narudzba.AdresaId);
            cmd.Parameters.AddWithValue("@datum", narudzba.DatumNarudzbe);
            cmd.Parameters.AddWithValue("@ukup", narudzba.UkupnaCijena);
            cmd.Parameters.AddWithValue("@stat", narudzba.Status);

            cmd.ExecuteNonQuery();
        }

        public static int GetLastInsertedId()
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();

            string query = "SELECT LAST_INSERT_ID();";

            using var cmd = new MySqlCommand(query, conn);

            object result = cmd.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int lastId))
            {
                return lastId;
            }
            return 0;
        }

        public static List<Narudzba> GetByKorisnikId(int korisnikId)
        {
            var list = new List<Narudzba>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM narudzba WHERE KorisnikId = @kid", conn);
            cmd.Parameters.AddWithValue("@kid", korisnikId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Narudzba
                {
                    Id = reader.GetInt32("Id"),
                    AdresaId = reader.GetInt32("AdresaId"),
                    DatumNarudzbe = reader.GetDateTime("Datum"),
                    UkupnaCijena = reader.GetDecimal("UkupnaCijena"),
                    Status = reader["Status"].ToString()
                });
            }
            return list;
        }

        public static void UpdateStatus(int narudzbaId, string noviStatus)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("UPDATE narudzba SET Status = @status WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@status", noviStatus);
            cmd.Parameters.AddWithValue("@id", narudzbaId);
            cmd.ExecuteNonQuery();
        }   
    }
}
