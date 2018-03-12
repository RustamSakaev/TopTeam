using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace Salon
{
    /// <summary>
    /// Interaction logic for PaymentMethodForm.xaml
    /// </summary>
    public partial class PaymentMethodForm : Window
    {
        private readonly string[] _hiddenFields = { "id" };
        private DataTable _currentFormData = new DataTable();

        private DataTable CurrentFormData
        {
            get => _currentFormData;
            set { _currentFormData = value; PaymentMethodGrid.DataContext = _currentFormData; }
        }

        public PaymentMethodForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBPaymentMethod.GetPaymentMethods();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new PaymentMethodActionForm(new Action(() => { CurrentFormData = DBPaymentMethod.GetPaymentMethods();}), FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataTable) PaymentMethodGrid.DataContext).Columns.IndexOf("id");
            var id = ((DataRowView) PaymentMethodGrid.SelectedItem)?.Row[idx].ToString();

            if (id is null) return;

            var form = new PaymentMethodActionForm(new Action(() => { CurrentFormData = DBPaymentMethod.GetPaymentMethods(); }), FormState.Edit, id);
            form.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataTable) PaymentMethodGrid.DataContext).Columns.IndexOf("id");

            foreach (DataRowView selectedItem in PaymentMethodGrid.SelectedItems)
            {
                DBPaymentMethod.DeletePaymentMethod(selectedItem.Row[idx].ToString());
            }

            CurrentFormData = DBPaymentMethod.GetPaymentMethods();
        }

        private void PaymentMethodGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }
    }
}
