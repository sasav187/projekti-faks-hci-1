namespace ProdavnicaApp.Models
{
    public class ListaZelja
    {
        public int KorisnikId { get; set; }
        public int ProizvodId { get; set; } // ID of the product in the wishlist
        public DateTime DatumDodavanja { get; set; } // Date when the product was added to the wishlist
    }
}
