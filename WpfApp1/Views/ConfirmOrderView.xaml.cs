using ProdavnicaApp.Models;
using ProdavnicaApp.DAL;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ProdavnicaApp.Views
{
    public partial class ConfirmOrderView : Window
    {
        private readonly List<StavkaNarudzbe> _stavke;
        public bool PotvrdaNarudzbe { get; private set; }

        public ConfirmOrderView(List<StavkaNarudzbe> stavke, decimal ukupno)
        {
            InitializeComponent();
            _stavke = stavke;
            LoadStavke();
            UkupnoTextBlock.Text = $"{TryFindResource("Lbl_Total") ?? "Ukupno:"} {Math.Round(ukupno, 2)} KM";
        }

        private void LoadStavke()
        {
            foreach (var stavka in _stavke)
            {
                Proizvod proizvod = ProizvodDAO.GetById(stavka.ProizvodId);
                if (proizvod != null)
                {
                    string red = $"{proizvod.Naziv} - {stavka.Kolicina} x {stavka.Cijena} KM = " +
                                 $"{Math.Round(stavka.Kolicina * stavka.Cijena, 2)} KM";
                    StavkeListBox.Items.Add(red);
                }
                else
                {
                    string poruka = TryFindResource("Msg_UnknownProduct")?.ToString() ?? "Nepoznat proizvod ID: ";
                    StavkeListBox.Items.Add($"{poruka}{stavka.ProizvodId}");
                }
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            PotvrdaNarudzbe = true;
            this.Close();
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            PotvrdaNarudzbe = false;
            this.Close();
        }
    }
}
