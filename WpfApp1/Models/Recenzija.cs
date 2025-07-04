namespace ProdavnicaApp.Models
{
    public class Recenzija
    {
        public int KorisnikId { get; set; } // ID of the user who wrote the review
        public int ProizvodId { get; set; } // ID of the product being reviewed
        public int Ocjena { get; set; } // Rating given by the user, e.g., 1 to 5 stars
        public string Komentar { get; set; } // Content of the review
        public DateTime DatumRecenzije { get; set; } // Date when the review was written
    }
}
