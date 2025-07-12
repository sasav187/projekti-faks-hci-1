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
        private List<StavkaNarudzbe> _stavkeNarudzbe;

        public MainView(Korisnik korisnik)
        {
            InitializeComponent();
            _korisnik = korisnik;

            SetSelectedLanguage(korisnik.Jezik);
            SetSelectedTheme(korisnik.Tema);

            ApplyLanguage(korisnik.Jezik);
            ApplyTheme(korisnik.Tema);

            _stavkeNarudzbe = new List<StavkaNarudzbe>();
            UserInfo.Text = $" {_korisnik.Ime} {_korisnik.Prezime}";

            LoadKategorije();
            LoadNarudzbe(korisnik);
        }

        private void SetSelectedLanguage(string lang)
        {
            foreach (ComboBoxItem item in LanguageComboBox.Items)
            {
                if (item.Tag.ToString() == lang)
                {
                    LanguageComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void SetSelectedTheme(string themeKey)
        {
            foreach (ComboBoxItem item in ThemeComboBox.Items)
            {
                if (item.Tag.ToString() == themeKey)
                {
                    ThemeComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void ApplyLanguage(string lang)
        {
            string path = $"/Resources/StringResources.{lang}.xaml";
            var newLangDict = new ResourceDictionary { Source = new Uri(path, UriKind.Relative) };

            var existingLangDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("StringResources."));

            if (existingLangDict != null)
                Application.Current.Resources.MergedDictionaries.Remove(existingLangDict);

            Application.Current.Resources.MergedDictionaries.Add(newLangDict);
            this.Title = TryFindResource("TitleMain")?.ToString();
        }

        private void ApplyTheme(string themeKey)
        {
            App.ApplyMaterialTheme(themeKey);
        }

        private void LoadKategorije()
        {
            _kategorije = KategorijaDAO.GetAll();
            KategorijeComboBox.ItemsSource = _kategorije;
        }

        private void LoadNarudzbe(Korisnik korisnik)
        {
            var narudzbe = NarudzbaDAO.GetByKorisnikId(korisnik.Id);
            NarudzbeDataGrid.ItemsSource = narudzbe;
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

            if (ProizvodiListBox.SelectedItem is not Proizvod proizvod)
            {
                StatusTextBlock.Text = "Molimo odaberite proizvod.";
                return;
            }

            if (!int.TryParse(KolicinaTextBox.Text, out int kolicina) || kolicina <= 0)
            {
                StatusTextBlock.Text = "Unesite validnu količinu.";
                return;
            }

            if (kolicina > proizvod.NaStanju)
            {
                StatusTextBlock.Text = "Nema dovoljno proizvoda na stanju.";
                return;
            }

            decimal cijena = proizvod.Cijena;
         
            var postojeca = _stavkeNarudzbe.FirstOrDefault(s => s.ProizvodId == proizvod.IdProizvoda);
            if (postojeca != null)
            {
                postojeca.Kolicina += kolicina;
            }
            else
            {
                _stavkeNarudzbe.Add(new StavkaNarudzbe
                {
                    ProizvodId = proizvod.IdProizvoda,
                    Kolicina = kolicina,
                    Cijena = cijena
                });
            }

            StatusTextBlock.Foreground = System.Windows.Media.Brushes.Green;
            StatusTextBlock.Text = "Proizvod dodat u korpu.";
            KolicinaTextBox.Clear();
        }

        private void ZavrsiNarudzbu_Click(object sender, RoutedEventArgs e)
        {
            if (!_stavkeNarudzbe.Any())
            {
                StatusTextBlock.Text = "Korpa je prazna.";
                return;
            }

            decimal ukupnaCijena = _stavkeNarudzbe.Sum(s => s.Cijena * s.Kolicina);

            string kodKupona = KuponTextBox.Text.Trim();

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

                ukupnaCijena = Math.Round(ukupnaCijena * (1 - kupon.Popust / 100), 2);
            }

            var confirmView = new ConfirmOrderView(_stavkeNarudzbe, ukupnaCijena);
            confirmView.ShowDialog();


            if (!confirmView.PotvrdaNarudzbe)
            {
                StatusTextBlock.Text = "Narudžba otkazana.";
                return;
            }

            var addressView = new AddressView(_korisnik.Id);
            addressView.ShowDialog();

            if (addressView.UnesenaAdresa == null)
            {
                StatusTextBlock.Text = "Niste unijeli adresu.";
                return;
            }

            try
            {
                var narudzba = new Narudzba
                {
                    KorisnikId = _korisnik.Id,
                    AdresaId = addressView.UnesenaAdresa.Id,
                    DatumNarudzbe = DateTime.Now,
                    UkupnaCijena = ukupnaCijena,
                    Status = "U obradi"
                };

                NarudzbaDAO.Insert(narudzba);
                int narudzbaId = NarudzbaDAO.GetLastInsertedId();

                foreach (var stavka in _stavkeNarudzbe)
                {
                    stavka.NarudzbaId = narudzbaId;
                    StavkaNarudzbeDAO.Insert(stavka);

                    var proizvod = ProizvodDAO.GetById(stavka.ProizvodId);
                    proizvod.NaStanju -= stavka.Kolicina;
                    ProizvodDAO.Update(proizvod);
                }

                var paymentView = new PaymentView(narudzbaId, ukupnaCijena);
                paymentView.ShowDialog();

                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Green;
                StatusTextBlock.Text = "Narudžba uspješno kreirana.";
                _stavkeNarudzbe.Clear();
                ProizvodiListBox.Items.Refresh();
            }
            catch (Exception ex)
            {
                StatusTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                StatusTextBlock.Text = $"Greška: {ex.Message}";
            }
        }

        private void NarudzbeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NarudzbeDataGrid.SelectedItem is Narudzba narudzba)
            {
                var orderDetailsView = new OrderDetailsView(narudzba);
                orderDetailsView.ShowDialog();

                NarudzbeDataGrid.SelectedItem = null;
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

                if (_korisnik != null)
                {
                    _korisnik.Jezik = lang;
                    KorisnikDAO.UpdateSettings(_korisnik.Id, lang, _korisnik.Tema);
                }
            }
        }

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeComboBox.SelectedItem is ComboBoxItem item)
            {
                string themeKey = item.Tag.ToString();
                App.ApplyMaterialTheme(themeKey);

                if (_korisnik != null)
                {
                    _korisnik.Tema = themeKey;
                    KorisnikDAO.UpdateSettings(_korisnik.Id, _korisnik.Jezik, themeKey);
                }
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
