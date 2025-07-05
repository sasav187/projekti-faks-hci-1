using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System.Windows;
using System.Windows.Controls;

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
            UserInfo.Text = $"Prijavljeni korisnik: {_korisnik.Ime} {_korisnik.Prezime}";
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
                    MessageBox.Show("Nema proizvoda u odabranoj kategoriji.", "Obavještenje", MessageBoxButton.OK, MessageBoxImage.Information);
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
