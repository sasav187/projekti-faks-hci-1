namespace ProdavnicaApp.Models
{
    public class StavkaNarudzbe
    {
        public int Id { get; set; } // Unique identifier for the order item
        public int NarudzbaId { get; set; } // ID of the order this item belongs to
        public int ProizvodId { get; set; } // ID of the product being ordered
        public int Kolicina { get; set; } // Quantity of the product ordered
        public decimal Cijena { get; set; } // Price of the product at the time of order
    }
}
