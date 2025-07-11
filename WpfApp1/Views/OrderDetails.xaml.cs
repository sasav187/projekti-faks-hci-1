using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System.Windows;

namespace ProdavnicaApp.Views
{
    public partial class OrderDetailsView : Window
    {
        public OrderDetailsView(Narudzba narudzba)
        {
            InitializeComponent();

            var adresa = AdresaDAO.GetByNarudzbaId(narudzba.Id);
            if (adresa != null)
            {
                AdresaTextBlock.Text = $"{adresa.Ulica}, {adresa.Grad}, {adresa.Drzava} ({adresa.Tip})";
            }

            var stavke = StavkaNarudzbeDAO.GetByNarudzbaId(narudzba.Id);
            var detalji = new List<StavkaNarudzbe>();

            foreach (var stavka in stavke)
            {
                var proizvod = ProizvodDAO.GetById(stavka.ProizvodId);
                detalji.Add(new StavkaNarudzbe
                {
                    NazivProizvoda = stavka.NazivProizvoda,
                    Kolicina = stavka.Kolicina,
                    Cijena = stavka.Cijena,
                });
            }

            StavkeDataGrid.ItemsSource = detalji;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
