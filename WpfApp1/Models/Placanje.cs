namespace ProdavnicaApp.Models
{
    public class Placanje
    {
        public int Id { get; set; } 
        public int NarudzbaId { get; set; } 
        public decimal Iznos { get; set; } 
        public string NacinPlacanja { get; set; } 
        public DateTime DatumPlacanja { get; set; } 
    }
}
