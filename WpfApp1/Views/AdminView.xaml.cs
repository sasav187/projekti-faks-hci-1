using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System.Windows;
using System.Windows.Controls;
using ProdavnicaApp.Views;

namespace ProdavnicaApp
{
    public partial class AdminView : Window
    {
        private List<Kategorija> _kategorije;
        private List<Proizvod> _proizvodi;
        private readonly Korisnik _korisnik;

        public AdminView(Korisnik korisnik)
        {
            InitializeComponent();

            _korisnik = korisnik;

            SetSelectedLanguage(korisnik.Jezik);
            SetSelectedTheme(korisnik.Tema);

            ApplyLanguague(korisnik.Jezik);
            ApplyTheme(korisnik.Tema);

            LoadKategorije();
            LoadProizvodi();
            LoadKorisnici();
            LoadNarudzbe();
            LoadKuponi();
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

        private void ApplyLanguague(string lang)
        {
            string path = $"/Resources/StringResources.{lang}.xaml";
            var newLangDict = new ResourceDictionary { Source = new Uri(path, UriKind.Relative) };

            var existingLangDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("StringResources."));

            if (existingLangDict != null)
                Application.Current.Resources.MergedDictionaries.Remove(existingLangDict);

            Application.Current.Resources.MergedDictionaries.Add(newLangDict);
            this.Title = TryFindResource("TitleAdmin")?.ToString();
        }

        private void ApplyTheme(string themeKey)
        {
            App.ApplyMaterialTheme(themeKey);
        }

        private void LoadKategorije()
        {
            _kategorije = KategorijaDAO.GetAll();
            KategorijeComboBox.ItemsSource = _kategorije;
            KategorijeDataGrid.ItemsSource = _kategorije;
        }

        private void LoadProizvodi()
        {
            _proizvodi = ProizvodDAO.GetAll();
            ProizvodiDataGrid.ItemsSource = _proizvodi;
        }

        private void LoadKorisnici()
        {
            var korisnici = KorisnikDAO.GetNonAdminUsers();
            KorisniciDataGrid.ItemsSource = korisnici;
        }

        private void LoadNarudzbe()
        {
            var narudzbe = NarudzbaDAO.GetAll();
            NarudzbeDataGrid.ItemsSource = narudzbe;
        }

        private void DodajKategoriju_Click(object sender, RoutedEventArgs e)
        {
            var naziv = KategorijaNazivTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(naziv))
            {
                var kategorija = new Kategorija { Naziv = naziv };
                KategorijaDAO.Insert(kategorija);
                MessageBox.Show(TryFindResource("Msg_CategoryAdded")?.ToString() ?? "Kategorija je uspješno dodana.");
                KategorijaNazivTextBox.Clear();
                LoadKategorije();
            }
            else
            {
                MessageBox.Show(TryFindResource("Msg_CategoryNameRequired")?.ToString() ?? "Unesite naziv kategorije.");
            }
        }

        private void AzurirajKategoriju_Click(object sender, RoutedEventArgs e)
        {
            if (KategorijeDataGrid.SelectedItem is Kategorija kategorija)
            {
                kategorija.Naziv = KategorijaNazivTextBox.Text.Trim();
                KategorijaDAO.Update(kategorija);
                LoadKategorije();
            }
        }

        private void ObrisiKategoriju_Click(object sender, RoutedEventArgs e)
        {
            if (KategorijeDataGrid.SelectedItem is Kategorija kategorija)
            {
                KategorijaDAO.Delete(kategorija.IdKategorije);
                LoadKategorije();
                LoadProizvodi();
            }
        }


        private void DodajProizvod_Click(object sender, RoutedEventArgs e)
        {
            if (KategorijeComboBox.SelectedItem is not Kategorija odabranaKategorija)
            {
                MessageBox.Show(TryFindResource("Msg_SelectCategory")?.ToString() ?? "Odaberite kategoriju.");
                return;
            }

            if (!decimal.TryParse(ProizvodCijenaTextBox.Text, out decimal cijena) ||
                !int.TryParse(ProizvodNaStanjuTextBox.Text, out int naStanju))
            {
                MessageBox.Show(TryFindResource("Msg_InvalidPriceOrStock")?.ToString() ?? "Unesite ispravne vrijednosti za cijenu i količinu.");
                return;
            }

            var proizvod = new Proizvod
            {
                Naziv = ProizvodNazivTextBox.Text.Trim(),
                Opis = ProizvodOpisTextBox.Text.Trim(),
                Cijena = cijena,
                NaStanju = naStanju,
                KategorijaId = odabranaKategorija.IdKategorije
            };

            if (string.IsNullOrEmpty(proizvod.Naziv))
            {
                MessageBox.Show(TryFindResource("Msg_ProductNameRequired")?.ToString() ?? "Unesite naziv proizvoda.");
                return;
            }

            ProizvodDAO.Insert(proizvod);
            MessageBox.Show(TryFindResource("Msg_ProductAdded")?.ToString() ?? "Proizvod je uspješno dodan.");
            LoadProizvodi();

            ProizvodNazivTextBox.Clear();
            ProizvodOpisTextBox.Clear();
            ProizvodCijenaTextBox.Clear();
            ProizvodNaStanjuTextBox.Clear();
            KategorijeComboBox.SelectedIndex = -1;
        }

        private void AzurirajProizvod_Click(object sender, RoutedEventArgs e)
        {
            if (ProizvodiDataGrid.SelectedItem is Proizvod proizvod &&
                KategorijeComboBox.SelectedItem is Kategorija odabranaKategorija &&
                decimal.TryParse(ProizvodCijenaTextBox.Text, out decimal cijena) &&
                int.TryParse(ProizvodNaStanjuTextBox.Text, out int naStanju))
            {
                proizvod.Naziv = ProizvodNazivTextBox.Text.Trim();
                proizvod.Opis = ProizvodOpisTextBox.Text.Trim();
                proizvod.Cijena = cijena;
                proizvod.NaStanju = naStanju;
                proizvod.KategorijaId = odabranaKategorija.IdKategorije;

                ProizvodDAO.Update(proizvod);
                LoadProizvodi();
            }
        }

        private void ObrisiProizvod_Click(object sender, RoutedEventArgs e)
        {
            if (ProizvodiDataGrid.SelectedItem is Proizvod proizvod)
            {
                ProizvodDAO.Delete(proizvod.IdProizvoda);
                LoadProizvodi();
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
                this.Title = TryFindResource("TitleAdmin")?.ToString();

                if (_korisnik != null)
                {
                    _korisnik.Jezik = lang;
                    KorisnikDAO.UpdateSettings(_korisnik.Id, lang, _korisnik.Tema);
                }
            }
        }

        private void PromijeniStatus_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Narudzba narudzba)
            {
                var statusChangeView = new OrderStatusChangeView(narudzba);
                statusChangeView.ShowDialog();

                if (statusChangeView.StatusPromijenjen)
                {
                    LoadNarudzbe();
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

        private void DodajKupon_Click(object sender, RoutedEventArgs e)
        {
            string kod = KuponKodTextBox.Text.Trim();
            if (!decimal.TryParse(KuponPopustTextBox.Text, out decimal popust) || popust < 0 || popust > 100)
            {
                MessageBox.Show(
                    TryFindResource("Msg_InvalidDiscount")?.ToString() ?? "Popust mora biti broj između 0 i 100.",
                    TryFindResource("Title_Error")?.ToString() ?? "Greška",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (KuponVaziDoPicker.SelectedDate == null)
            {
                MessageBox.Show(
                    TryFindResource("Msg_SelectCouponDate")?.ToString() ?? "Odaberite datum do kada važi kupon.",
                    TryFindResource("Title_Error")?.ToString() ?? "Greška",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var kupon = new Kupon
            {
                Kod = kod,
                Popust = popust,
                VaziDo = KuponVaziDoPicker.SelectedDate.Value
            };

            try
            {
                KuponDAO.Insert(kupon);
                MessageBox.Show(
                    TryFindResource("Msg_CouponAdded")?.ToString() ?? "Kupon uspješno dodat.",
                    TryFindResource("Title_Info")?.ToString() ?? "Informacija",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                KuponKodTextBox.Clear();
                KuponPopustTextBox.Clear();
                KuponVaziDoPicker.SelectedDate = null;
                LoadKuponi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    (TryFindResource("Msg_CouponError")?.ToString() ?? "Greška prilikom dodavanja kupona: ") + ex.Message,
                    TryFindResource("Title_Error")?.ToString() ?? "Greška",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadKuponi()
        {
            var kuponi = KuponDAO.GetAll();
            KuponiDataGrid.ItemsSource = kuponi;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            this.Close();
        }
    }
}
