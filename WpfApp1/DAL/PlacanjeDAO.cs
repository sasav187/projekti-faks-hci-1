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

        public static void Insert(Placanje placanje)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO placanje (NarudzbaId, Iznos, Nacin, DatumPlacanja) " +
                                       "VALUES (@NarudzbaId, @Iznos, @Nacin, @DatumPlacanja)", conn);
            cmd.Parameters.AddWithValue("@NarudzbaId", placanje.NarudzbaId);
            cmd.Parameters.AddWithValue("@Iznos", placanje.Iznos);
            cmd.Parameters.AddWithValue("@Nacin", placanje.NacinPlacanja);
            cmd.Parameters.AddWithValue("@DatumPlacanja", placanje.DatumPlacanja);
            cmd.ExecuteNonQuery();
        }
    }
}
