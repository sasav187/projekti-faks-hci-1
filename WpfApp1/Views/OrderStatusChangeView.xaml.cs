using ProdavnicaApp.DAL;
using ProdavnicaApp.Models;
using System.Windows;
using System.Windows.Controls;

namespace ProdavnicaApp.Views
{
    public partial class OrderStatusChangeView : Window
    {
        private readonly Narudzba _narudzba;
        public bool StatusPromijenjen { get; private set; }

        public OrderStatusChangeView(Narudzba narudzba)
        {
            InitializeComponent();
            _narudzba = narudzba;
        }

        private void Sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (StatusComboBox.SelectedItem is ComboBoxItem item)
            {
                string noviStatus = item.Content.ToString();
                string stariStatus = _narudzba.Status;

                try
                {
                    NarudzbaDAO.UpdateStatus(_narudzba.Id, noviStatus);
                    NarudzbaStatusLogDAO.Insert(new NarudzbaStatusLog
                    {
                        NarudzbaId = _narudzba.Id,
                        StariStatus = stariStatus,
                        NoviStatus = noviStatus,
                        DatumPromjeneStatusa = DateTime.Now
                    });

                    MessageBox.Show("Status uspješno ažuriran.");
                    StatusPromijenjen = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Odaberite status.");
            }
        }
    }
}
