using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class ListaZeljaDAO
    {
        public static List<ListaZelja> GetAll()
        {
            var list = new List<ListaZelja>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM listazelja", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ListaZelja
                {
                    KorisnikId = reader.GetInt32("KorisnikId"),
                    ProizvodId = reader.GetInt32("ProizvodId"),
                    DatumDodavanja = reader.GetDateTime("DatumDodavanja")
                });
            }
            return list;
        }
    }
}
