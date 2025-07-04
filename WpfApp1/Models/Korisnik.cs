namespace ProdavnicaApp.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; } // Consider hashing this in production
        public DateTime DatumRegistracije { get; set; }
        public int UlogaId { get; set; } // Nullable in case of no associated role
        public Uloga Uloga { get; set; } // Navigation property for role
    }
}
