using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Salon.Misc;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ClientForm.xaml
    /// </summary>
    public partial class ClientForm : Window
    {
        private readonly string[] _hiddenFields = { "id", "ФИО" };
        private readonly List<Filter> _currentFilters = new List<Filter>();
        private DataTable _currentFormData = new DataTable();

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; ClientGrid.DataContext = _currentFormData.DefaultView; }
        }

        private void SetFilter(string key, string column, string expression)
        {
            var filterIdx = _currentFilters.FindIndex(filter => filter.InnerKey.Equals(key));
            var newFilter = new Filter(key, column, expression);


            if (filterIdx == -1) _currentFilters.Add(newFilter);
            else _currentFilters[filterIdx] = newFilter;

            DisplayDataWithFilters();
        }

        private void RemoveFilter(string key)
        {
            _currentFilters.RemoveAll(filter => filter.InnerKey.Equals(key));

            DisplayDataWithFilters();
        }

        private void DisplayDataWithFilters()
        {
            var displayData = _currentFormData.DefaultView;

            var filters = string.Join(" AND ",
                _currentFilters.Select(filter => $"{filter.Key} {filter.Expression}"));

            displayData.RowFilter = filters;

            ClientGrid.DataContext = displayData;
        }

        public ClientForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBClient.GetClients();

            GenderCmbBox.Items.Add("Все");
            GenderCmbBox.Items.Add("Мужской");
            GenderCmbBox.Items.Add("Женский");

            GenderCmbBox.SelectedItem = "Все";
        }

        private void FioBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilter("FullName", "ФИО", $"LIKE '%{FioBox.Text}%'");
        }

        private void GenderCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGender = e.AddedItems[0].ToString();
            var filterValue = selectedGender != "Все" ? selectedGender : string.Empty;

            SetFilter("Gender", "Пол", $"LIKE '%{filterValue}%'");
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            const string key = "DateFrom";

            if (e.AddedItems.Count <= 0)
            {
                RemoveFilter(key);
                return;
            }

            DateTime.TryParse(e.AddedItems[0].ToString(), out var selectedDate);

            SetFilter(key, "[Дата рождения]", $">= #{selectedDate:MM/dd/yyyy}#");
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            const string key = "DateTo";

            if (e.AddedItems.Count <= 0)
            {
                RemoveFilter(key);
                return;
            }

            DateTime.TryParse(e.AddedItems[0].ToString(), out var selectedDate);

            SetFilter(key, "[Дата рождения]", $">= #{selectedDate:MM/dd/yyyy}#");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new ClientActionForm(new Action(() => { CurrentFormData = DBClient.GetClients(); }), FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataView)ClientGrid.DataContext).Table.Columns.IndexOf("id");

            if (idx == -1) return;

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
            DisplayDataWithFilters();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            FioBox.Text = string.Empty;
            GenderCmbBox.SelectedItem = "Все";
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
        }
    }
}
