namespace ProdavnicaApp.Models
{
    public class StavkaNarudzbe
    {
        public int Id { get; set; } 
        public int NarudzbaId { get; set; } 
        public int ProizvodId { get; set; } 
        public int Kolicina { get; set; } 
        public decimal Cijena { get; set; } 
        public string NazivProizvoda { get; set; }
    }
}
