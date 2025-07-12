namespace ProdavnicaApp.Models
{
    public class Recenzija
    {
        public int KorisnikId { get; set; } 
        public int ProizvodId { get; set; } 
        public int Ocjena { get; set; } 
        public string Komentar { get; set; } 
        public DateTime DatumRecenzije { get; set; } 
    }
}
