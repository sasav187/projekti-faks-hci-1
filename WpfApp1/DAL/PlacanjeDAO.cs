using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class PlacanjeDAO
    {
        public static List<Placanje> GetAll()
        {
            var list = new List<Placanje>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM placanje", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Placanje
                {
                    Id = reader.GetInt32("Id"),
                    NarudzbaId = reader.GetInt32("NarudzbaId"),
                    Iznos = reader.GetDecimal("Iznos"),
                    NacinPlacanja = reader["Nacin"] as string ?? string.Empty,
                    DatumPlacanja = reader.GetDateTime("DatumPlacanja")
                });
            }
            return list;
        }
    }   
}
