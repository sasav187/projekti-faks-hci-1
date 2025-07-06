using System.Windows;
using ProdavnicaApp.Models;

namespace ProdavnicaApp.Views
{
    public partial class ConfirmOrderView : Window
    {
        public bool PotvrdaNarudzbe { get; private set; } = false;

        public ConfirmOrderView(Proizvod proizvod, int kolicina)
        {
            InitializeComponent();

            decimal ukupno = proizvod.Cijena * kolicina;

            ProizvodTextBlock.Text = proizvod.Naziv;
            KolicinaTextBlock.Text = kolicina.ToString();   
            CijenaTextBlock.Text = $"{proizvod.Cijena} KM";
            UkupnoTextBlock.Text = $"{ukupno} KM";
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            PotvrdaNarudzbe = true;
            this.Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
