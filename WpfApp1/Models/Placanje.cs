namespace ProdavnicaApp.Models
{
    public class Placanje
    {
        public int Id { get; set; } // Unique identifier for the payment record
        public int NarudzbaId { get; set; } // ID of the order associated with this payment
        public decimal Iznos { get; set; } // Amount paid, e.g., 100.00 for a $100 payment
        public string NacinPlacanja { get; set; } // Payment method, e.g., "Credit Card", "PayPal", "Bank Transfer"
        public DateTime DatumPlacanja { get; set; } // Date and time when the payment was made
    }
}
