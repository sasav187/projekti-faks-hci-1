using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class ProizvodDAO
    {
        public static List<Proizvod> GetAll()
        {
            var list = new List<Proizvod>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM proizvod", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Proizvod
                {
                    IdProizvoda = reader.GetInt32("Id"),
                    Naziv = reader["Naziv"].ToString(),
                    Opis = reader["Opis"].ToString(),
                    Cijena = reader.GetDecimal("Cijena"),
                    NaStanju = reader.GetInt32("NaStanju"),
                    KategorijaId = reader.GetInt32("KategorijaId")
                });
            }
            return list;
        }

        public static List<Proizvod> GetByKategorijaId(int kategorijaId)
        {
            var list = new List<Proizvod>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM proizvod WHERE KategorijaId = @katId", conn);
            cmd.Parameters.AddWithValue("@katId", kategorijaId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Proizvod
                {
                    IdProizvoda = reader.GetInt32("Id"),
                    Naziv = reader["Naziv"].ToString(),
                    Opis = reader["Opis"].ToString(),
                    Cijena = reader.GetDecimal("Cijena"),
                    NaStanju = reader.GetInt32("NaStanju"),
                    KategorijaId = reader.GetInt32("KategorijaId")
                });
            }
            return list;
        }

        public static void Insert(Proizvod proizvod)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO proizvod (Naziv, Opis, Cijena, NaStanju, KategorijaId)" +
                                        "VALUES (@naziv, @opis, @cijena, @naStanju, @kategorijaId)", conn);
            cmd.Parameters.AddWithValue("@naziv", proizvod.Naziv);
            cmd.Parameters.AddWithValue("@opis", proizvod.Opis);
            cmd.Parameters.AddWithValue("@cijena", proizvod.Cijena);
            cmd.Parameters.AddWithValue("@naStanju", proizvod.NaStanju);
            cmd.Parameters.AddWithValue("@kategorijaId", proizvod.KategorijaId);
            cmd.ExecuteNonQuery();
        }
    }
}
