using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System.Text.RegularExpressions;
using System.Windows;

namespace ProdavnicaApp.Views
{
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
            this.Title = TryFindResource("TitleRegister")?.ToString(); 
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {

            string Ime = ImeTextBox.Text.Trim();
            string Prezime = PrezimeTextBox.Text.Trim();
            string Email = EmailTextBox.Text.Trim();
            string Lozinka = LozinkaBox.Password;
            string Potvrda = PotvrdaLozinkeBox.Password;
            DateTime DatumRegistracije = DateTime.Now;
            int UlogaId = 1;

            if (string.IsNullOrWhiteSpace(Ime) ||
                string.IsNullOrWhiteSpace(Prezime) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Lozinka) ||
                string.IsNullOrWhiteSpace(Potvrda))
            {
                MessageBox.Show(
                    TryFindResource("AllFieldsRequired")?.ToString() ?? "Sva polja su obavezna.",
                    TryFindResource("Error")?.ToString() ?? "Greška",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(Email))
            {
                MessageBox.Show(
                    TryFindResource("InvalidEmail")?.ToString() ?? "Email nije validan.",
                    TryFindResource("Error")?.ToString() ?? "Greška",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (!IsValidPassword(Lozinka))
            {
                MessageBox.Show(
                    TryFindResource("InvalidPassword")?.ToString() ??
                    "Lozinka mora imati bar 8 karaktera, " +
                    "jedno veliko slovo, jedno malo slovo, " +
                    "jedan broj i jedan specijalni znak.",
                    TryFindResource("Error")?.ToString() ?? "Greška",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (Lozinka != Potvrda)
            {
                MessageBox.Show(
                    TryFindResource("PasswordsDontMatch")?.ToString() ?? "Lozinke se ne podudaraju.",
                    TryFindResource("Error")?.ToString() ?? "Greška",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var korisnik = new Korisnik
            {
                Ime = Ime,
                Prezime = Prezime,
                Email = Email,
                Lozinka = Lozinka,
                DatumRegistracije = DatumRegistracije,
                UlogaId = UlogaId
            };

            try
            {
                KorisnikDAO.Insert(korisnik);
                MessageBox.Show(
                    TryFindResource("RegistrationSuccess")?.ToString() ?? "Registracija uspješna!",
                    TryFindResource("Info")?.ToString() ?? "Informacija",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                var login = new LoginView();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    (TryFindResource("RegistrationError")?.ToString() ?? "Greška prilikom registracije: ") + ex.Message,
                    TryFindResource("Error")?.ToString() ?? "Greška",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginView();
            login.Show();
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }

        private bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password,
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");
        }
    }
}
