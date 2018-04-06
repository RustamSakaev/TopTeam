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
    /// Логика взаимодействия для TypeServiceForm.xaml
    /// </summary>
    public partial class TypeServiceForm : Window
    {
        private DataTable currentData = new DataTable();
        private readonly List<Filter> Filter = new List<Filter>();
        private readonly Action Back;
        private string kind_ID;
        public TypeServiceForm(Action b = null, string kind_id = null)
        {
            InitializeComponent();
            Back = b;
            kind_ID = kind_id;
        }
        private DataTable CurrentData
        {
            get { return currentData; }
            set { currentData = value; TypeServiceGrid.ItemsSource = currentData.DefaultView;
            TypeServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            TypeServiceGrid.Columns[3].Visibility = Visibility.Hidden;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new TypeServiceActionForm(() => { CurrentData = DBTypeService.GetTypeServices(); }, FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var type_col = ((DataView)TypeServiceGrid.ItemsSource).Table.Columns.IndexOf("id");
            if (type_col == -1) return;
            var typeid = ((DataRowView)TypeServiceGrid.SelectedItem)?.Row[type_col].ToString();
            if (typeid == null) return;
            var form = new TypeServiceActionForm(() => { CurrentData = DBTypeService.GetTypeServices(); }, FormState.Edit, typeid);
            form.ShowDialog();
        }

        
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            CurrentData = DBTypeService.GetTypeServices();
            TypeServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            TypeServiceGrid.Columns[3].Visibility = Visibility.Hidden;
            GroupServiceCmbBox.Items.Add("Все");
            GroupServiceCmbBox.SelectedItem = "Все";
            foreach (DataRow kind in DBGroupService.GetGroupServices().Rows)
            {
                GroupServiceCmbBox.Items.Add(kind["Наименование"]);
            }
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
            TypeServiceGrid.ItemsSource = displayData;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurFilter("TypeService", "Наименование", $"LIKE '%{NameBox.Text}%'");
        }

        private void GroupServiceCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedService = e.AddedItems[0].ToString();
            var filterValue = selectedService != "Все" ? selectedService : string.Empty;
            CurFilter("Service", "[Группа услуги]", $"LIKE '%{filterValue}%'");
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Clear();
            GroupServiceCmbBox.SelectedValue = "Все";
        }

        private void TypeServiceGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var type_col = ((DataView)TypeServiceGrid.ItemsSource).Table.Columns.IndexOf("id");
            if (type_col == -1) return;
            var typeid = ((DataRowView)TypeServiceGrid.SelectedItem)?.Row[type_col].ToString();
            if (typeid == null) return;
            DBTypeService_KindService.AddTypeKind(typeid,kind_ID);
            Back();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var type_col = ((DataView)TypeServiceGrid.ItemsSource).Table.Columns.IndexOf("id");
            if (type_col == -1) return;
            var typeid = ((DataRowView)TypeServiceGrid.SelectedItem)?.Row[type_col].ToString();
            if (typeid == null) return;
            try
            {
                DBTypeService.DeleteTypeService(typeid);
                MessageBox.Show("Объект успешно удален!");
                CurrentData = DBTypeService.GetTypeServices();
                TypeServiceGrid.Columns[0].Visibility = Visibility.Hidden;
                TypeServiceGrid.Columns[3].Visibility = Visibility.Hidden;
            }catch(System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Невозможно удалить данный объект!");
            }
            Back();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Back();
        }
    }
}
