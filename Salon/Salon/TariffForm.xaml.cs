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

        private void RemoveFilter(string key)
        {
            Filter.RemoveAll(filter => filter.InnerKey.Equals(key));

            DataFilter();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var tariffidx = ((DataView)TariffGrid.ItemsSource).Table.Columns.IndexOf("id");

            if (tariffidx == -1) return;

            var tariffid = ((DataRowView)TariffGrid.SelectedItem)?.Row[tariffidx].ToString();

            if (tariffid == null) return;
            try
            {
                DBTariff.DeleteTariff(tariffid);
                MessageBox.Show("Объект успешно удален!");
                CurrentData = DBTariff.GetTariffs();
                TariffGrid.Columns[0].Visibility = Visibility.Hidden;
                TariffGrid.Columns[4].Visibility = Visibility.Hidden;
            }
            catch(System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Невозможно удалить данный объект!");
            }
        }

        private void PriceBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurFilter("Tariff", "Стоимость", $"LIKE '%{PriceBox.Text.ToString()}%'");
        }
        private void CurFilter(string key, string column, string exp)
        {
            var filterIdx = Filter.FindIndex(filter => filter.InnerKey.Equals(key));
            var newFilter = new Filter(key, column, exp);
            if (filterIdx == -1) Filter.Add(newFilter);
            else Filter[filterIdx] = newFilter;
            DataFilter();
        }

        private void DataFilter()
        {
            var displayData = currentData.DefaultView;
            var filters = string.Join(" AND ", Filter.Select(filter => $"{filter.Key} {filter.Expression}"));
            displayData.RowFilter = filters;
            TariffGrid.ItemsSource = displayData;
        }

        private void ServiceCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTariff = e.AddedItems[0].ToString();
            var filterValue = selectedTariff != "Все" ? selectedTariff : string.Empty;
            CurFilter("Tariff", "[Услуга]", $"LIKE '%{filterValue}%'");
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            const string key = "Дата начала";

            if (e.AddedItems.Count <= 0)
            {
                RemoveFilter(key);
                return;
            }

            DateTime selectedDate;
            DateTime.TryParse(e.AddedItems[0].ToString(), out selectedDate);
            CurFilter(key, "[Дата начала]", $">= #{selectedDate:MM/dd/yyyy}#");
        }
    }
}
