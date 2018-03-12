using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Salon
{
    /// <summary>
    /// Interaction logic for BankCardForm.xaml
    /// </summary>
    public partial class BankCardForm : Window
    {
        private readonly string[] _hiddenFields = { "id" };
        private DataTable _currentFormData = new DataTable();
        private string _currentFilter = "";

        private DataTable CurrentFormData
        {
            get => _currentFormData;
            set { _currentFormData = value; BankCardGrid.DataContext = _currentFormData; }
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

            BankCardGrid.DataContext = displayData;
        }

        public BankCardForm(string cliendId = null)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBBankCard.GetBankCards();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentFilter = SearchBox.Text;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BankCardGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }

        private void BankCardGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DisplayDataWithFilter();
        }
    }
}
