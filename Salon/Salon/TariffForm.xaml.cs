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
using Salon.Misc;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для TariffForm.xaml
    /// </summary>
    public partial class TariffForm : Window
    {
        private DataTable currentData = new DataTable();
        private readonly List<Filter> Filter = new List<Filter>();
        private readonly Action<string> Back;
        public TariffForm(Action<string> b = null)
        {
            InitializeComponent();
            Back = b;
        }

        private DataTable CurrentData
        {
            get { return currentData; }
            set { currentData = value; TariffGrid.ItemsSource = currentData.DefaultView;
            TariffGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new TariffActionForm(() => { CurrentData = DBTariff.GetTariffs(); }, FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var tariffidx = ((DataView)TariffGrid.ItemsSource).Table.Columns.IndexOf("id");

            if (tariffidx == -1) return;

            var tariffid = ((DataRowView)TariffGrid.SelectedItem)?.Row[tariffidx].ToString();

            if (tariffid == null) return;

            var form = new TariffActionForm(() => { CurrentData = DBTariff.GetTariffs(); }, FormState.Add);
            form.ShowDialog();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            CurrentData = DBTariff.GetTariffs();
            ServiceCmbBox.Items.Add("Все");
            foreach (DataRow type in DBService.GetServices().Rows)
            {
                ServiceCmbBox.Items.Add(type["Наименование"]);
            }
            ServiceCmbBox.SelectedValue = "Все";
            TariffGrid.Columns[0].Visibility = Visibility.Hidden;
            TariffGrid.Columns[4].Visibility = Visibility.Hidden;
        }

        private void StartDatePicker_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void EndDatePicker_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
