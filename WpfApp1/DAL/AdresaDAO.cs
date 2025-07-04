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
    }
}
