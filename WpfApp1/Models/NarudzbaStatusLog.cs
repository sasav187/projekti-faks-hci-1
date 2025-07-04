namespace ProdavnicaApp.Models
{
    public class NarudzbaStatusLog
    {
        public int Id { get; set; } // Unique identifier for the log entry
        public int NarudzbaId { get; set; } // ID of the order associated with this log entry
        public string StariStatus { get; set; } // Status of the order, e.g., "Pending", "Shipped", "Delivered"
        public string NoviStatus { get; set; } // New status of the order after the change, e.g., "Shipped", "Delivered"    
        public DateTime DatumPromjeneStatusa { get; set; } // Date and time when the status was changed
    }
}
