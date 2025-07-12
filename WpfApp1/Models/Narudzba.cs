namespace ProdavnicaApp.Models
{
    public class Narudzba
    {
        public int Id { get; set; }
        public int KorisnikId { get; set; } 
        public int AdresaId { get; set; } 
        public DateTime DatumNarudzbe { get; set; } 
        public decimal UkupnaCijena { get; set; } 
        public string Status { get; set; } 
    }
}
