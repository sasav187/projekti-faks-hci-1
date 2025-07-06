using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System.Windows;
using System.Windows.Controls;

namespace ProdavnicaApp.Views
{
    public partial class AddressView : Window
    {
        public Adresa UnesenaAdresa { get; private set; }

        private readonly int _korisnikId;

        public AddressView(int korisnikId)
        {
            InitializeComponent();
            _korisnikId = korisnikId;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string Ulica = UlicaTextBox.Text.Trim();
            string Grad = GradTextBox.Text.Trim();
            string PostanskiBroj = PostanskiBrojTextBox.Text.Trim();
            string Drzava = DrzavaTextBox.Text.Trim();
            string Tip = (TipAdreseComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(Ulica) ||
                string.IsNullOrWhiteSpace(Grad) ||
                string.IsNullOrWhiteSpace(PostanskiBroj) ||
                string.IsNullOrWhiteSpace(Drzava) ||
                string.IsNullOrWhiteSpace(Tip))
            {
                MessageBox.Show("Sva polja su obavezna.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var adresa = new Adresa
            {
                KorisnikId = _korisnikId,
                Ulica = Ulica,
                Grad = Grad,
                PostanskiBroj = PostanskiBroj,
                Drzava = Drzava,
                Tip = Tip
            };

            try
            {
                AdresaDAO.Insert(adresa);
                adresa.Id = AdresaDAO.GetLastInsertedId(); // Get the last inserted ID
                UnesenaAdresa = adresa;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom unosa adrese: " + ex.Message);
            }
        }

        private void BackToOrder_Click(object sender, RoutedEventArgs e)
        {
            UnesenaAdresa = null; // Clear the address if going back
            this.Close();
        }
    }
}
