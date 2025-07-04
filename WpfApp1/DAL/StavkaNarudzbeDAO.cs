using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class StavkaNarudzbeDAO
    {
        public static List<StavkaNarudzbe> GetAll()
        {
            var list = new List<StavkaNarudzbe>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM stavkanarudzbe", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StavkaNarudzbe
                {
                    Id = reader.GetInt32("Id"),
                    NarudzbaId = reader.GetInt32("NarudzbaId"),
                    ProizvodId = reader.GetInt32("ProizvodId"),
                    Kolicina = reader.GetInt32("Kolicina"),
                    Cijena = reader.GetDecimal("Cijena")
                });
            }
            return list;
        }
    }
}
