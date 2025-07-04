namespace ProdavnicaApp.Models
{
    public class Kupon
    {
        public string Kod { get; set; }
        public decimal Popust { get; set; } // Discount amount, e.g., 10.00 for a $10 discount
        public DateTime VaziDo { get; set; } // Expiration date of the coupon
    }
}
