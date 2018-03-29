using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Salon.Database;
using System.Data;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для TariffForm.xaml
    /// </summary>
    public partial class TariffForm : Window
    {
        public TariffForm()
        {
            InitializeComponent();
        }

        private DataTable _currentFormData = new DataTable();
        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; TariffGrid.ItemsSource = _currentFormData.DefaultView; }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TariffActionForm tar = new TariffActionForm(FormState.Add);
            tar.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var tariffidx = ((DataView)TariffGrid.ItemsSource).Table.Columns.IndexOf("id");

            if (tariffidx == -1) return;

            var tariffid = ((DataRowView)TariffGrid.SelectedItem)?.Row[tariffidx].ToString();

            if (tariffid == null) return;

            var servidx = ((DataView)TariffGrid.ItemsSource).Table.Columns.IndexOf("serv_id");

            if (servidx == -1) return;

            var servid = ((DataRowView)TariffGrid.SelectedItem)?.Row[servidx].ToString();

            if (servid == null) return;

            var dateidx = ((DataView)TariffGrid.ItemsSource).Table.Columns.IndexOf("Дата начала");

            if (dateidx == -1) return;

            var dateid = ((DataRowView)TariffGrid.SelectedItem)?.Row[servidx].ToString();

            if (dateid == null) return;

            var costidx = ((DataView)TariffGrid.ItemsSource).Table.Columns.IndexOf("Стоимость");

            if (costidx == -1) return;

            var costid = ((DataRowView)TariffGrid.SelectedItem)?.Row[servidx].ToString();

            if (costid == null) return;

            TariffActionForm tar = new TariffActionForm(FormState.Edit,tariffid,servid);
            tar.ShowDialog();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBTariff.GetTariffs();
            foreach (DataRow type in DBService.GetServices().Rows)
            {
                ServiceCmbBox.Items.Add(type["Наименование"]);
            }
            TariffGrid.Columns[0].Visibility = Visibility.Hidden;
            TariffGrid.Columns[4].Visibility = Visibility.Hidden;
        }
    }
}
