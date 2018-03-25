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
    /// Логика взаимодействия для VisitForm.xaml
    /// </summary>
    public partial class VisitForm : Window
    {
        private readonly string[] _hiddenFields = { "id", "clientid", "workerid", "statusid" };
        private readonly List<Filter> _currentFilters = new List<Filter>();
        private DataTable _currentFormData = new DataTable();

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; VisitGrid.DataContext = _currentFormData; }
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

            VisitGrid.DataContext = displayData;
        }

        public VisitForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBVisit.GetVisits();

            ClientCmbBox.Items.Add("Все");
            ClientCmbBox.SelectedItem = "Все";
            foreach (DataRow client in DBClient.GetClients().Rows)
            {
                ClientCmbBox.Items.Add(client["ФИО"]);
            }

            WorkerCmbBox.Items.Add("Все");
            WorkerCmbBox.SelectedItem = "Все";
            foreach (DataRow worker in DBWorker.GetWorkers().Rows)
            {
                WorkerCmbBox.Items.Add(worker["ФИО"]);
            }
        }

        private void ClientCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedClient = e.AddedItems[0].ToString();
            var filterValue = selectedClient != "Все" ? selectedClient : string.Empty;

            SetFilter("Client", "Клиент", $"LIKE '%{filterValue}%'");
        }

        private void WorkerCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedWorker = e.AddedItems[0].ToString();
            var filterValue = selectedWorker != "Все" ? selectedWorker : string.Empty;

            SetFilter("Worker", "Сотрудник", $"LIKE '%{filterValue}%'");
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

            SetFilter(key, "[Дата посещения]", $">= #{selectedDate:MM/dd/yyyy}#");
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

            SetFilter(key, "[Дата посещения]", $"<= #{selectedDate:MM/dd/yyyy}#");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new VisitActionForm(new Action(() => { CurrentFormData = DBVisit.GetVisits(); }), FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataView)VisitGrid.DataContext).Table.Columns.IndexOf("id");

            if (idx == -1) return;

            var id = ((DataRowView)VisitGrid.SelectedItem)?.Row[idx].ToString();

            if (id == null) return;

            var form = new VisitActionForm(new Action(() => { CurrentFormData = DBVisit.GetVisits(); }), FormState.Edit, id);
            form.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataView)VisitGrid.DataContext).Table.Columns.IndexOf("id");

            foreach (DataRowView selectedItem in VisitGrid.SelectedItems)
            {
                DBVisit.DeleteVisit(selectedItem.Row[idx].ToString());
            }

            CurrentFormData = DBVisit.GetVisits();
        }

        private void VisitGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }

        private void VisitGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DisplayDataWithFilters();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClientCmbBox.SelectedItem = "Все";
            WorkerCmbBox.SelectedItem = "Все";
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
        }
    }
}
