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
    /// Логика взаимодействия для KindService.xaml
    /// </summary>
    public partial class KindService : Window
    {
        private DataTable currentData = new DataTable();
        private readonly List<Filter> Filter = new List<Filter>();
        private readonly Action Back;
        private string type_ID;
        TypeServiceActionForm typekind;
        public KindService(Action b=null, string type_id = null)
        {
            InitializeComponent();
            Back = b;
            type_ID = type_id;
        }
        private DataTable CurrentData
        {
            get { return currentData; }
            set { currentData = value; KindServiceGrid.ItemsSource = currentData.DefaultView;
            KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new KindServiceActionForm(() => { CurrentData = DBKindService.GetKindServices(); }, FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var kind_col = ((DataView)KindServiceGrid.ItemsSource).Table.Columns.IndexOf("id");
            if (kind_col == -1) return;

            var kindid = ((DataRowView)KindServiceGrid.SelectedItem)?.Row[kind_col].ToString();
            if (kindid == null) return;
            var form = new KindServiceActionForm(() => { CurrentData = DBKindService.GetKindServices(); }, FormState.Edit, kindid);
            form.ShowDialog();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            CurrentData = DBKindService.GetKindServices();
            KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;
           


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
            KindServiceGrid.ItemsSource = displayData;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurFilter("KindService", "Наименование", $"LIKE '%{NameBox.Text}%'");
        }

        private void TypeServiceCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NameBox.Clear();
        }

        private void KindServiceGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var kind_col = ((DataView)KindServiceGrid.ItemsSource).Table.Columns.IndexOf("id");
            if (kind_col == -1) return;
            var kindid = ((DataRowView)KindServiceGrid.SelectedItem)?.Row[kind_col].ToString();
            if (kindid == null) return;
            DBTypeService_KindService.AddTypeKind(type_ID, kindid);
            Back();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var kind_col = ((DataView)KindServiceGrid.ItemsSource).Table.Columns.IndexOf("id");
            if (kind_col == -1) return;
            var kindid = ((DataRowView)KindServiceGrid.SelectedItem)?.Row[kind_col].ToString();
            if (kindid == null) return;
            try
            {
                DBKindService.DeleteKindService(kindid);
                MessageBox.Show("Объект успешно удален!");
                CurrentData = DBKindService.GetKindServices();
                KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            }catch(System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Невозможно удалить данный объект!");
            }
        }
    }
}
