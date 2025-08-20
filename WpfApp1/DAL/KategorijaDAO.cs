using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class KategorijaDAO
    {
        public static List<Kategorija> GetAll()
        {
            var list = new List<Kategorija>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM kategorija", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Kategorija
                {
                    IdKategorije = reader.GetInt32("Id"),
                    Naziv = reader["Naziv"].ToString()
                });
            }
            return list;
        }

        public static void Insert(Kategorija kategorija)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO kategorija (Naziv) VALUES (@naziv)", conn);
            cmd.Parameters.AddWithValue("@naziv", kategorija.Naziv);
            cmd.ExecuteNonQuery();
        }

        public static void Update(Kategorija kategorija)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("UPDATE kategorija SET Naziv = @naziv WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@naziv", kategorija.Naziv);
            cmd.Parameters.AddWithValue("@id", kategorija.IdKategorije);
            cmd.ExecuteNonQuery();
        }

        public static void Delete(int id)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM kategorija WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
