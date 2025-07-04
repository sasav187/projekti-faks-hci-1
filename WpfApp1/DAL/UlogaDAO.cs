using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class UlogaDAO
    {
        public static Uloga? GetById(int id)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM uloga WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Uloga
                {
                    Id = reader.GetInt32("Id"),
                    Naziv = reader["Naziv"].ToString()
                };
            }
            return null;
        }
    }
}
