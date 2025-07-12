namespace ProdavnicaApp.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; } 
        public DateTime DatumRegistracije { get; set; }
        public int UlogaId { get; set; } 
        public Uloga Uloga { get; set; }
        public string Jezik { get; set; }
        public string Tema { get; set; }
    }
}
