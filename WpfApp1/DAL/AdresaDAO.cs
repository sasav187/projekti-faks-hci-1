using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class AdresaDAO
    {
        public static List<Adresa> GetAll()
        {
            var list = new List<Adresa>();

            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM adresa", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Adresa
                {
                    Id = reader.GetInt32("Id"),
                    KorisnikId = reader["KorisnikId"] as int?,
                    Ulica = reader["Ulica"].ToString(),
                    Grad = reader["Grad"].ToString(),
                    PostanskiBroj = reader["PostanskiBroj"].ToString(),
                    Drzava = reader["Drzava"].ToString(),
                    Tip = reader["Tip"].ToString()
                });
            }

            return list;
        }

        public static void Insert(Adresa adresa)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand("INSERT INTO adresa (KorisnikId, Ulica, Grad, PostanskiBroj, Drzava, Tip)" +
                                       " VALUES (@kid, @ulica, @grad, @pbroj, @drzava, @tip)", conn);
            cmd.Parameters.AddWithValue("@kid", adresa.KorisnikId);
            cmd.Parameters.AddWithValue("@ulica", adresa.Ulica);
            cmd.Parameters.AddWithValue("@grad", adresa.Grad);
            cmd.Parameters.AddWithValue("@pbroj", adresa.PostanskiBroj);
            cmd.Parameters.AddWithValue("@drzava", adresa.Drzava);
            cmd.Parameters.AddWithValue("@tip", adresa.Tip);

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

        public static Adresa GetByNarudzbaId(int narudzbaId)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT a.* FROM adresa a " +
                                       "JOIN narudzba n ON a.Id = n.AdresaId " +
                                       "WHERE n.Id = @narudzbaId", conn);

            cmd.Parameters.AddWithValue("@narudzbaId", narudzbaId);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Adresa
                {
                    Ulica = reader["Ulica"].ToString(),
                    Grad = reader["Grad"].ToString(),
                    PostanskiBroj = reader["PostanskiBroj"].ToString(),
                    Drzava = reader["Drzava"].ToString(),
                    Tip = reader["Tip"].ToString()
                };
            }
            return null;
        }
    }
}
