using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для VisitForm.xaml
    /// </summary>
    public partial class VisitForm : Window
    {
        private readonly string[] _hiddenFields = { "id", "clientid", "workerid", "statusid" };
        private DataTable _currentFormData = new DataTable();
        private string _currentFilter = "";

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; VisitGrid.DataContext = _currentFormData; }
        }

        private string CurrentFilter
        {
            get { return _currentFilter; }
            set { _currentFilter = value; DisplayDataWithFilter(); }
        }

        //TODO Implement advanced filters
        private void DisplayDataWithFilter()
        {
            var displayData = _currentFormData.DefaultView;
            displayData.RowFilter = $"Клиент LIKE '%{_currentFilter}%' OR Сотрудник LIKE '%{_currentFilter}%'";

            VisitGrid.DataContext = displayData;
        }

        public VisitForm()
        {
            InitializeComponent();

            ClientCmbBox.ItemsSource = DBClient.GetClients().DefaultView;
            ClientCmbBox.DisplayMemberPath = "ФИО";
            ClientCmbBox.SelectedValuePath = "id";

            WorkerCmbBox.ItemsSource = DBWorker.GetWorkers().DefaultView;
            WorkerCmbBox.DisplayMemberPath = "ФИО";
            WorkerCmbBox.SelectedValuePath = "id";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBVisit.GetVisits();
        }

        private void FullSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchStack.Visibility = Visibility.Collapsed;
            FullSearchGroup.Visibility = Visibility.Visible;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SearchStack.Visibility = Visibility.Visible;
            FullSearchGroup.Visibility = Visibility.Collapsed;
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

            if (id is null) return;

            var form = new VisitActionForm(new Action(() => { CurrentFormData = DBVisit.GetVisits(); ; }), FormState.Edit, id);
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
            DisplayDataWithFilter();
        }
    }
}
