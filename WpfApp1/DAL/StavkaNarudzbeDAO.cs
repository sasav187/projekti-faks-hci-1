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

        public static void Insert(StavkaNarudzbe stavka)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();

            string query = @"INSERT INTO stavkanarudzbe (NarudzbaId, ProizvodId, Kolicina, Cijena)
                             VALUES (@narudzbaId, @proizvodId, @kolicina, @cijena)";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@narudzbaId", stavka.NarudzbaId);
            cmd.Parameters.AddWithValue("@proizvodId", stavka.ProizvodId);
            cmd.Parameters.AddWithValue("@kolicina", stavka.Kolicina);
            cmd.Parameters.AddWithValue("@cijena", stavka.Cijena);

            cmd.ExecuteNonQuery();
        }
    }
}
