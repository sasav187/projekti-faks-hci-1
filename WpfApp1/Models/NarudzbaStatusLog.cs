namespace ProdavnicaApp.Models
{
    public class NarudzbaStatusLog
    {
        public int Id { get; set; } 
        public int NarudzbaId { get; set; } 
        public string StariStatus { get; set; } 
        public string NoviStatus { get; set; } 
        public DateTime DatumPromjeneStatusa { get; set; } 
    }
}
