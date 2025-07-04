namespace ProdavnicaApp.Models
{
    public class Proizvod
    {
        public int IdProizvoda { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
        public int NaStanju { get; set; }
        public int? KategorijaId { get; set; } // Nullable in case of no associated category
    }
}
