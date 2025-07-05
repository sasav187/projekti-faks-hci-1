using MySql.Data.MySqlClient;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.DAL
{
    public class KorisnikDAO
    {
        public static List<Korisnik> GetAll()
        {
            var list = new List<Korisnik>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM korisnik", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var korisnik = new Korisnik
                {
                    Id = reader.GetInt32("Id"),
                    Ime = reader["Ime"].ToString(),
                    Prezime = reader["Prezime"].ToString(),
                    Email = reader["Email"].ToString(),
                    Lozinka = reader["Lozinka"].ToString(),
                    DatumRegistracije = reader.GetDateTime("DatumRegistracije"),
                    UlogaId = reader.GetInt32("UlogaId")
                };

                korisnik.Uloga = UlogaDAO.GetById(korisnik.UlogaId);
                list.Add(korisnik);
            }
            return list;
        }

        public static List<Korisnik> GetNonAdminUsers()
        {
            var list = new List<Korisnik>();
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"
                SELECT k.*, u.Naziv AS Naziv
                FROM korisnik k
                JOIN uloga u ON k.UlogaId = u.Id
                WHERE u.Naziv != 'Admin'", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Korisnik
                {
                    Id = reader.GetInt32("Id"),
                    Ime = reader["Ime"].ToString(),
                    Prezime = reader["Prezime"].ToString(),
                    Email = reader["Email"].ToString(),
                    Lozinka = reader["Lozinka"].ToString(),
                    DatumRegistracije = reader.GetDateTime("DatumRegistracije"),
                    UlogaId = reader.GetInt32("UlogaId"),
                    Uloga = new Uloga
                    {
                        Id = reader.GetInt32("UlogaId"),
                        Naziv = reader["Naziv"].ToString()
                    }
                });
            }
            return list;
        }

        public static void Insert(Korisnik korisnik)
        {
            using var conn = new MySqlConnection(Database.ConnectionString);
            conn.Open();

            var cmd = new MySqlCommand(@"INSERT INTO korisnik (Ime, Prezime, Email, Lozinka, DatumRegistracije, UlogaId) 
                                 VALUES (@Ime, @Prezime, @Email, @Lozinka, @Datum, @Uloga)", conn);
            cmd.Parameters.AddWithValue("@Ime", korisnik.Ime);
            cmd.Parameters.AddWithValue("@Prezime", korisnik.Prezime);
            cmd.Parameters.AddWithValue("@Email", korisnik.Email);
            cmd.Parameters.AddWithValue("@Lozinka", korisnik.Lozinka);
            cmd.Parameters.AddWithValue("@Datum", korisnik.DatumRegistracije);
            cmd.Parameters.AddWithValue("@Uloga", korisnik.UlogaId);
            cmd.ExecuteNonQuery();
        }


    }
}
