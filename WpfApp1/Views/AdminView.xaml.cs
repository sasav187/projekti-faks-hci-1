using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProdavnicaApp
{
    public partial class AdminView : Window
    {
        private List<Kategorija> _kategorije;

        public AdminView()
        {
            InitializeComponent();
            LoadKategorije();
            LoadKorisnici();
        }

        private void LoadKategorije()
        {
            _kategorije = KategorijaDAO.GetAll();
            KategorijeComboBox.ItemsSource = _kategorije;
        }

        private void LoadKorisnici()
        {
            var korisnici = KorisnikDAO.GetNonAdminUsers();
            KorisniciListBox.ItemsSource = korisnici;
        }

        private void DodajKategoriju_Click(object sender, RoutedEventArgs e)
        {
            var naziv = KategorijaNazivTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(naziv))
            {
                KategorijaDAO.Insert(new Kategorija { Naziv = naziv });
                MessageBox.Show("Kategorija je uspješno dodana.");
                KategorijaNazivTextBox.Clear();
                LoadKategorije();
            }
            else
            {
                MessageBox.Show("Unesite naziv kategorije.");
            }
        }

        private void DodajProizvod_Click(object sender, RoutedEventArgs e)
        {
            if (KategorijeComboBox.SelectedItem is not Kategorija odabranaKategorija)
            {
                MessageBox.Show("Odaberite kategoriju.");
                return;
            }

            if (!decimal.TryParse(ProizvodCijenaTextBox.Text, out decimal cijena) ||
                !int.TryParse(ProizvodNaStanjuTextBox.Text, out int naStanju))
            {
                MessageBox.Show("Unesite ispravne vrijednosti za cijenu i količinu.");
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
                MessageBox.Show("Unesite naziv proizvoda.");
                return;
            }

            ProizvodDAO.Insert(proizvod);
            MessageBox.Show("Proizvod je uspješno dodan.");

            ProizvodNazivTextBox.Clear();
            ProizvodOpisTextBox.Clear();
            ProizvodCijenaTextBox.Clear();
            ProizvodNaStanjuTextBox.Clear();
            KategorijeComboBox.SelectedIndex = -1;
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
