using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System;
using System.Windows;

namespace ProdavnicaApp.Views
{
    public partial class PaymentView : Window
    {
        private readonly int _narudzbaId;
        private readonly decimal _ukupnaCijena;

        public PaymentView(int narudzbaId, decimal ukupnaCijena)
        {
            InitializeComponent();
            _narudzbaId = narudzbaId;
            _ukupnaCijena = ukupnaCijena;
            this.Title = TryFindResource("TitlePayment")?.ToString();
        }

        private void ProcessPayment(string nacin)
        {
            try
            {
                var placanje = new Placanje
                {
                    NarudzbaId = _narudzbaId,
                    Iznos = _ukupnaCijena,
                    NacinPlacanja = nacin,
                    DatumPlacanja = DateTime.Now
                };

                PlacanjeDAO.Insert(placanje);

                MessageBox.Show(
                    TryFindResource("PaymentSuccess")?.ToString() ?? "Plaćanje uspješno!",
                    TryFindResource("Info")?.ToString() ?? "Informacija",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    (TryFindResource("PaymentError")?.ToString() ?? "Greška prilikom plaćanja: ") + ex.Message,
                    TryFindResource("Error")?.ToString() ?? "Greška",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void CardPayment_Click(object sender, RoutedEventArgs e) => ProcessPayment("Kartica");
        private void CashPayment_Click(object sender, RoutedEventArgs e) => ProcessPayment("Pouzećem");
        private void PaypalPayment_Click(object sender, RoutedEventArgs e) => ProcessPayment("PayPal");
    }
}
