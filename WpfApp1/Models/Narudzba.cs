namespace ProdavnicaApp.Models
{
    public class Narudzba
    {
        public int Id { get; set; }
        public int KorisnikId { get; set; } // ID of the user who placed the order
        public int AdresaId { get; set; } // Nullable in case of no associated address
        public DateTime DatumNarudzbe { get; set; } // Date when the order was placed
        public decimal UkupnaCijena { get; set; } // Total price of the order
        public string Status { get; set; } // e.g., "Pending", "Shipped", "Delivered"
    }
}
