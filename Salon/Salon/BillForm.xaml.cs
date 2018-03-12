using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace Salon
{
    /// <summary>
    /// Interaction logic for BillForm.xaml
    /// </summary>
    public partial class BillForm : Window
    {
        private readonly string[] _hiddenFields = {"id"};
        private DataTable _currentFormData = new DataTable();
        private string _currentFilter = "";

        private DataTable CurrentFormData
        {
            get => _currentFormData;
            set { _currentFormData = value; BillGrid.DataContext = _currentFormData;}
        }

        private string CurrentFilter
        {
            get => _currentFilter;
            set { _currentFilter = value; DisplayDataWithFilter(); }
        }

        private void DisplayDataWithFilter()
        {
            var displayData = _currentFormData.DefaultView;
            displayData.RowFilter = $"ФИО LIKE '%{_currentFilter}%'";

            BillGrid.DataContext = displayData;
        }

        public BillForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBBill.GetBills();
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CurrentFilter = SearchBox.Text;
        }

        private void PaidButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataView) BillGrid.DataContext).Table.Columns.IndexOf("id");

            foreach (DataRowView selectedItem in BillGrid.SelectedItems)
            {
                DBBill.CompleteBill(selectedItem.Row[idx].ToString());
            }

            CurrentFormData = DBBill.GetBills();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataView)BillGrid.DataContext).Table.Columns.IndexOf("id");

            foreach (DataRowView selectedItem in BillGrid.SelectedItems)
            {
                DBBill.DeleteBill(selectedItem.Row[idx].ToString());
            }

            CurrentFormData = DBBill.GetBills();
        }

        private void BillGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }

        private void BillGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DisplayDataWithFilter();
        }
    }
}
