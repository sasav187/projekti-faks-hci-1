using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<Proizvod> _sviProizvodi; // Svi proizvodi u bazi

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

            // Učitavanje svih proizvoda za globalnu pretragu
            _sviProizvodi = ProizvodDAO.GetAll();
            ProizvodiListBox.ItemsSource = _sviProizvodi;
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

            RefreshDataGridHeaders();
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
                var proizvodiKategorije = ProizvodDAO.GetByKategorijaId(kategorija.IdKategorije);

                if (proizvodiKategorije.Count == 0)
                {
                    MessageBox.Show(
                        TryFindResource("Msg_NoProductsInCategory")?.ToString() ?? "Nema proizvoda u odabranoj kategoriji.",
                        TryFindResource("Info")?.ToString() ?? "Obavještenje",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                }

                // Prikaz proizvoda izabrane kategorije (ne utiče na globalni search)
                ProizvodiListBox.ItemsSource = proizvodiKategorije;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchTextBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(search))
            {
                ProizvodiListBox.ItemsSource = _sviProizvodi;
            }
            else
            {
                var filtrirani = _sviProizvodi
                                 .Where(p => p.Naziv.ToLower().Contains(search) ||
                                             p.Opis.ToLower().Contains(search))
                                 .ToList();
                ProizvodiListBox.ItemsSource = filtrirani;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox_TextChanged(sender, null);
        }

        private void NaruciButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = string.Empty;

            if (ProizvodiListBox.SelectedItem is not Proizvod proizvod)
            {
                StatusTextBlock.Text = TryFindResource("Msg_SelectProduct")?.ToString() ?? "Molimo odaberite proizvod.";
                return;
            }

            if (!int.TryParse(KolicinaTextBox.Text, out int kolicina) || kolicina <= 0)
            {
                StatusTextBlock.Text = TryFindResource("Msg_EnterQuantity")?.ToString() ?? "Unesite validnu količinu.";
                return;
            }

            if (kolicina > proizvod.NaStanju)
            {
                StatusTextBlock.Text = TryFindResource("Msg_NotEnoughStock")?.ToString() ?? "Nema dovoljno proizvoda na stanju.";
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
            StatusTextBlock.Text = TryFindResource("Msg_AddedToCart")?.ToString() ?? "Proizvod dodat u korpu.";
            KolicinaTextBox.Clear();
        }

        private void ZavrsiNarudzbu_Click(object sender, RoutedEventArgs e)
        {
            if (!_stavkeNarudzbe.Any())
            {
                StatusTextBlock.Text = TryFindResource("Msg_EmptyCart")?.ToString() ?? "Korpa je prazna.";
                return;
            }

            decimal ukupnaCijena = _stavkeNarudzbe.Sum(s => s.Cijena * s.Kolicina);

            string kodKupona = KuponTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(kodKupona))
            {
                var kupon = KuponDAO.GetByKod(kodKupona);
                if (kupon == null)
                {
                    StatusTextBlock.Text = TryFindResource("Msg_CouponNotExist")?.ToString() ?? "Kupon ne postoji.";
                    return;
                }
                if (kupon.VaziDo < DateTime.Today)
                {
                    StatusTextBlock.Text = TryFindResource("Msg_CouponExpired")?.ToString() ?? "Kupon je istekao.";
                    return;
                }

                ukupnaCijena = Math.Round(ukupnaCijena * (1 - kupon.Popust / 100), 2);
            }

            var confirmView = new ConfirmOrderView(_stavkeNarudzbe, ukupnaCijena);
            confirmView.ShowDialog();

            if (!confirmView.PotvrdaNarudzbe)
            {
                StatusTextBlock.Text = TryFindResource("Msg_OrderCancelled")?.ToString() ?? "Narudžba otkazana.";
                return;
            }

            var addressView = new AddressView(_korisnik.Id);
            addressView.ShowDialog();

            if (addressView.UnesenaAdresa == null)
            {
                StatusTextBlock.Text = TryFindResource("Msg_NoAddress")?.ToString() ?? "Niste unijeli adresu.";
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
                StatusTextBlock.Text = TryFindResource("Msg_OrderSuccess")?.ToString() ?? "Narudžba uspješno kreirana.";
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

        private void RefreshDataGridHeaders()
        {
            if (NarudzbeDataGrid.Columns.Count >= 5)
            {
                NarudzbeDataGrid.Columns[0].Header = TryFindResource("OrderId")?.ToString();
                NarudzbeDataGrid.Columns[1].Header = TryFindResource("AddressId")?.ToString();
                NarudzbeDataGrid.Columns[2].Header = TryFindResource("OrderDate")?.ToString();
                NarudzbeDataGrid.Columns[3].Header = TryFindResource("Total")?.ToString();
                NarudzbeDataGrid.Columns[4].Header = TryFindResource("Status")?.ToString();
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
