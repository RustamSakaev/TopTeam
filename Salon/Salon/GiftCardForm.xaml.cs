using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Salon
{
    /// <summary>
    /// Interaction logic for GiftCardForm.xaml
    /// </summary>
    public partial class GiftCardForm : Window
    {
        private readonly string[] _hiddenFields = { "id", "clientid", "workerid" };
        private DataTable _currentFormData = new DataTable();
        private string _currentFilter = "";

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; GiftCardGrid.DataContext = _currentFormData; }
        }

        private string CurrentFilter
        {
            get { return _currentFilter; }
            set { _currentFilter = value; DisplayDataWithFilter(); }
        }

        private void DisplayDataWithFilter()
        {
            var displayData = _currentFormData.DefaultView;
            displayData.RowFilter = $"Клиент LIKE '%{_currentFilter}%' OR Сотрудник LIKE '%{_currentFilter}%'";

            GiftCardGrid.DataContext = displayData;
        }

        public GiftCardForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBGiftCard.GetGiftCards();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentFilter = SearchBox.Text;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new GiftCardActionForm(new Action(() => { CurrentFormData = DBGiftCard.GetGiftCards(); }), FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataView)GiftCardGrid.DataContext).Table.Columns.IndexOf("id");

            if (idx == -1) return;

            var id = ((DataRowView)GiftCardGrid.SelectedItem)?.Row[idx].ToString();

            if (id is null) return;

            var form = new GiftCardActionForm(new Action(() => { CurrentFormData = DBGiftCard.GetGiftCards(); }), FormState.Edit, id);
            form.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataView)GiftCardGrid.DataContext).Table.Columns.IndexOf("id");

            foreach (DataRowView selectedItem in GiftCardGrid.SelectedItems)
            {
                DBGiftCard.DeleteGiftCard(selectedItem.Row[idx].ToString());
            }

            CurrentFormData = DBGiftCard.GetGiftCards();
        }

        private void GiftCardGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }

        private void GiftCardGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DisplayDataWithFilter();
        }
    }
}
