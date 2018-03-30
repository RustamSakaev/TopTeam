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
    /// Логика взаимодействия для GroupServiceForm.xaml
    /// </summary>
    public partial class GroupServiceForm : Window
    {
        private DataTable currentData = new DataTable();
        private readonly List<Filter> Filter = new List<Filter>();
        private readonly Action<string> Back;
        public GroupServiceForm(Action<string> b = null)
        {
            InitializeComponent();
            Back = b;
        }
        private DataTable CurrentData
        {
            get { return currentData; }
            set { currentData = value; GroupServiceGrid.ItemsSource = currentData.DefaultView;
            GroupServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new GroupServiceActionForm(() => { CurrentData = DBGroupService.GetGroupServices(); }, FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var group_col = ((DataView)GroupServiceGrid.ItemsSource).Table.Columns.IndexOf("id");
            if (group_col == -1) return;

            var groupid = ((DataRowView)GroupServiceGrid.SelectedItem)?.Row[group_col].ToString();
            if (groupid == null) return;
           
            var form = new GroupServiceActionForm(() => { CurrentData = DBGroupService.GetGroupServices(); }, FormState.Edit, groupid);
            form.ShowDialog();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            CurrentData = DBGroupService.GetGroupServices();
            GroupServiceGrid.Columns[0].Visibility = Visibility.Hidden;
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
            GroupServiceGrid.ItemsSource = displayData;
        }
    }
}
