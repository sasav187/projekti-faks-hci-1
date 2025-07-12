namespace ProdavnicaApp.Models
{
    public class Adresa
    {
        public int Id { get; set; }
        public int? KorisnikId { get; set; } 
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string PostanskiBroj { get; set; }
        public string Drzava { get; set; }
        public string Tip { get; set; } 
    }
}
