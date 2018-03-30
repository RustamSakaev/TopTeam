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
using System.Data;
using System.Data.SqlClient;
using Salon.Misc;
using Salon.Database;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ServiceForm.xaml
    /// </summary>
    public partial class ServiceForm : Window
    {
        private DataTable currentData = new DataTable();
        private readonly List<Filter> Filter = new List<Filter>();
        private readonly Action<string> Back;
        private DataTable CurrentData
        {
            get { return currentData; }
            set { currentData = value; ServiceGrid.ItemsSource = currentData.DefaultView;
                ServiceGrid.Columns[5].Visibility = Visibility.Hidden;
                ServiceGrid.Columns[6].Visibility = Visibility.Hidden;
                ServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }
        public ServiceForm(Action<string> b = null)
        {
            InitializeComponent();
            Back = b;
        }
        public void OnLoad( object sender, RoutedEventArgs e)
        { 
            CurrentData = DBService.GetServices();
            TypeServiceCmbBox.Items.Add("Все");
            TypeServiceCmbBox.SelectedItem = "Все";
            foreach (DataRow type in DBTypeService.GetTypeServices().Rows)
            {
                TypeServiceCmbBox.Items.Add(type["Наименование"]);
            }
            KindServiceCmbBox.Items.Add("Все");
            KindServiceCmbBox.SelectedItem = "Все";
            foreach (DataRow kind in DBKindService.GetKindServices().Rows)
            {
                KindServiceCmbBox.Items.Add(kind["Наименование"]);
            }
        }
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new ServiceActionForm(() => {CurrentData = DBService.GetServices();}, FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var serv_col = ((DataView)ServiceGrid.ItemsSource).Table.Columns.IndexOf("id");
            if (serv_col == -1) return;

            var servid = ((DataRowView)ServiceGrid.SelectedItem)?.Row[serv_col].ToString();
            if (servid == null) return;

            var form = new ServiceActionForm(() =>{CurrentData = DBService.GetServices();}, FormState.Edit, servid);
            form.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurFilter("Name", "Наименование", $"LIKE '%{NameBox.Text}%'");
        }

        private void TypeServiceCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedService = e.AddedItems[0].ToString();
            var filterValue = selectedService != "Все" ? selectedService : string.Empty;
            CurFilter("Service", "[Тип услуги]", $"LIKE '%{filterValue}%'");
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
            ServiceGrid.ItemsSource = displayData;
        }

        private void KindServiceCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedService = e.AddedItems[0].ToString();
            var filterValue = selectedService != "Все" ? selectedService : string.Empty;
            CurFilter("Service", "[Вид услуги]", $"LIKE '%{filterValue}%'");
        }
    }
}
