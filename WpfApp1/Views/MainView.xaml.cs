using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System.Windows;
using System.Windows.Controls;
using ProdavnicaApp.Views;

namespace ProdavnicaApp
{
    public partial class MainView : Window
    {
        private readonly Korisnik _korisnik;
        private List<Kategorija> _kategorije;

        public MainView(Korisnik korisnik)
        {
            InitializeComponent();
            _korisnik = korisnik;
            UserInfo.Text = $" {_korisnik.Ime} {_korisnik.Prezime}";
            LoadKategorije();
        }

        private void LoadKategorije()
        {
            _kategorije = KategorijaDAO.GetAll();
            KategorijeComboBox.ItemsSource = _kategorije;
        }

        private void KategorijeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KategorijeComboBox.SelectedItem is Kategorija kategorija)
            {
                var proizvodi = ProizvodDAO.GetByKategorijaId(kategorija.IdKategorije);
                ProizvodiListBox.ItemsSource = proizvodi;

                if (proizvodi.Count == 0)
                {
                    MessageBox.Show("Nema proizvoda u odabranoj kategoriji.", "Obavještenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void NaruciButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = string.Empty;

            if (ProizvodiListBox.SelectedItem is not Proizvod odabraniProizvod)
            {
                StatusTextBlock.Text = "Molimo odaberite proizvod.";
                return;
            }

            if (!int.TryParse(KolicinaTextBox.Text, out int kolicina) || kolicina <= 0)
            {
                StatusTextBlock.Text = "Unesite validnu količinu (pozitivan cijeli broj).";
                return;
            }

            if (kolicina > odabraniProizvod.NaStanju)
            {
                StatusTextBlock.Text = "Nema dovoljno proizvoda na stanju.";
                return;
            }

            string kodKupona = KuponTextBox.Text.Trim();
            decimal popust = 0;

            if (!string.IsNullOrEmpty(kodKupona))
            {
                var kupon = KuponDAO.GetByKod(kodKupona);
                if (kupon == null)
                {
                    StatusTextBlock.Text = "Kupon ne postoji.";
                    return;
                }
                if (kupon.VaziDo < DateTime.Today)
                {
                    StatusTextBlock.Text = "Kupon je istekao.";
                    return;
                }

                popust = kupon.Popust;
                odabraniProizvod.Cijena = Math.Round(odabraniProizvod.Cijena * (1 - popust / 100), 2);
            }

            var confirmWindow = new Views.ConfirmOrderView(odabraniProizvod, kolicina);
            confirmWindow.ShowDialog();

            if (!confirmWindow.PotvrdaNarudzbe)
            {
                StatusTextBlock.Text = "Narudžba otkazana.";
                return;
            }

            var addressView = new AddressView(_korisnik.Id);
            addressView.ShowDialog();

            var adresa = addressView.UnesenaAdresa;

            if (addressView.UnesenaAdresa == null)
            {
                StatusTextBlock.Text = "Niste unijeli adresu za dostavu.";
                return;
            }

            try
            {
                var narudzba = new Narudzba
                {
                    KorisnikId = _korisnik.Id,
                    AdresaId = adresa.Id,
                    DatumNarudzbe = DateTime.Now,
                    UkupnaCijena = odabraniProizvod.Cijena * kolicina,
                    Status = "U obradi"
                };

                NarudzbaDAO.Insert(narudzba);

                int narudzbaId = NarudzbaDAO.GetLastInsertedId();

                var stavka = new StavkaNarudzbe
                {
                    NarudzbaId = narudzbaId,
                    ProizvodId = odabraniProizvod.IdProizvoda,
                    Kolicina = kolicina,
                    Cijena = odabraniProizvod.Cijena
                };

                var paymentWindow = new PaymentView(narudzbaId, narudzba.UkupnaCijena);
                paymentWindow.ShowDialog();

                StavkaNarudzbeDAO.Insert(stavka);

                odabraniProizvod.NaStanju -= kolicina;
                ProizvodiListBox.Items.Refresh();

                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Green;
                StatusTextBlock.Text = "Narudžba je uspješno kreirana!";

                KolicinaTextBox.Clear();
            }
            catch (Exception ex)
            {
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                StatusTextBlock.Text = $"Greška prilikom naručivanja: {ex.Message}";
            }
        }

        private void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cb && cb.SelectedItem is ComboBoxItem selected)
            {
                string lang = selected.Tag.ToString();
                string path = $"/Resources/StringResources.{lang}.xaml";

                var newLangDict = new ResourceDictionary { Source = new Uri(path, UriKind.Relative) };

                var existingLangDict = Application.Current.Resources.MergedDictionaries
                    .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("StringResources."));

                if (existingLangDict != null)
                    Application.Current.Resources.MergedDictionaries.Remove(existingLangDict);

                Application.Current.Resources.MergedDictionaries.Add(newLangDict);

                this.Title = TryFindResource("TitleMain")?.ToString();
            }
        }

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeComboBox.SelectedItem is ComboBoxItem item)
            {
                string themeKey = item.Tag.ToString();
                App.ApplyMaterialTheme(themeKey);
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            this.Close();
        }
    }
}
