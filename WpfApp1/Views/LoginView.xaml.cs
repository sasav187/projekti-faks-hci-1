using ProdavnicaApp.Models;
using ProdavnicaApp.Views;
using System.Windows;
using System.Windows.Controls;

namespace ProdavnicaApp
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            Korisnik? korisnik = AuthService.Login(email, password);

            if (korisnik != null)
            {
                ApplyLanguage(korisnik.Jezik);
                ApplyTheme(korisnik.Tema);

                switch (korisnik.UlogaId)
                { 
                    case 1:
                        MainView mainView = new MainView(korisnik);
                        mainView.Show();
                        this.Close();
                        break;
                    case 2:
                        AdminView adminView = new AdminView(korisnik);
                        adminView.Show();
                        this.Close();
                        break;
                    default:
                        MessageBox.Show(
                            TryFindResource("UnknownRoleMessage")?.ToString() ?? "Nepoznata uloga korisnika.",
                            TryFindResource("UnknownRoleTitle")?.ToString() ?? "Greška",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        this.Close();
                        return;
                }
            }
            else
            {
                MessageBox.Show(
                    TryFindResource("LoginErrorMessage")?.ToString() ?? "Neispravni podaci.",
                    TryFindResource("LoginErrorTitle")?.ToString() ?? "Greška",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
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
            this.Title = TryFindResource("TitleLogin")?.ToString();
        }

        private void ApplyTheme(string themeKey)
        {
            App.ApplyMaterialTheme(themeKey);
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

                this.Title = TryFindResource("TitleLogin")?.ToString();
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

        private void OpenRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerView = new RegisterView();
            registerView.Show();
            this.Close(); 
        }

    }
}