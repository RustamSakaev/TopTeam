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
        private readonly string[] hiddenFields = { "id" };
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
            var form = new PaymentMethodActionForm(() => { CurrentFormData = DBPaymentMethod.GetPaymentMethods();});
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PaymentMethodGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }
    }
}
