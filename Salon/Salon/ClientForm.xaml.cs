using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ClientForm.xaml
    /// </summary>
    public partial class ClientForm : Window
    {
        private readonly string[] _hiddenFields = { "id", "ФИО" };
        private DataTable _currentFormData = new DataTable();
        private string _currentFilter = "";

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; ClientGrid.DataContext = _currentFormData.DefaultView; }
        }

        private string CurrentFilter
        {
            get { return _currentFilter; }
            set { _currentFilter = value; DisplayDataWithFilter(); }
        }

        private void DisplayDataWithFilter()
        {
            var displayData = _currentFormData.DefaultView;
            displayData.RowFilter = $"Фамилия LIKE '%{_currentFilter}%' OR Имя LIKE '%{_currentFilter}%' OR Отчество LIKE '%{_currentFilter}%'";

            ClientGrid.DataContext = displayData;
        }

        public ClientForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBClient.GetClients();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           // CurrentFilter = SearchBox.Text;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new ClientActionForm(new Action(() => { CurrentFormData = DBClient.GetClients(); }), FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataView)ClientGrid.DataContext).Table.Columns.IndexOf("id");
            var id = ((DataRowView)ClientGrid.SelectedItem)?.Row[idx].ToString();

            if (id == null) return;

            var form = new ClientActionForm(new Action(() => { CurrentFormData = DBClient.GetClients(); }), FormState.Edit, id);
            form.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataView)ClientGrid.DataContext).Table.Columns.IndexOf("id");

            foreach (DataRowView selectedItem in ClientGrid.SelectedItems)
            {
                DBClient.DeleteClient(selectedItem.Row[idx].ToString());
            }

            CurrentFormData = DBClient.GetClients();
        }

        private void ClientGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }

        private void ClientGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DisplayDataWithFilter();
        }
    }
}
