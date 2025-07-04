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
    }
}
