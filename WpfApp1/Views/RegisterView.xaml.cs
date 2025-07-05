using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System;
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
            var korisnik = new Korisnik
            {
                Ime = ImeTextBox.Text.Trim(),
                Prezime = PrezimeTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                Lozinka = LozinkaBox.Password,
                DatumRegistracije = DateTime.Now,
                UlogaId = 1 
            };

            if (string.IsNullOrWhiteSpace(korisnik.Ime) ||
                string.IsNullOrWhiteSpace(korisnik.Prezime) ||
                string.IsNullOrWhiteSpace(korisnik.Email) ||
                string.IsNullOrWhiteSpace(korisnik.Lozinka))
            {
                MessageBox.Show("Sva polja su obavezna.");
                return;
            }

            try
            {
                KorisnikDAO.Insert(korisnik);
                MessageBox.Show("Registracija uspješna!");
                var login = new LoginView();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom registracije: " + ex.Message);
            }
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginView();
            login.Show();
            this.Close();
        }
    }
}
