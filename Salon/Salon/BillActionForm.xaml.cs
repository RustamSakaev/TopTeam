using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Salon
{
    /// <summary>
    /// Interaction logic for BillActionForm.xaml
    /// </summary>
    public partial class BillActionForm : Window
    {
        private readonly string[] _hiddenFields = { "id" };

        public BillActionForm(string editId = null)
        {
            InitializeComponent();

            var currentDataItem = DBBill.GetDetailedBill(editId);
            var currentDetailedDataItem = DBProvidingService.GetProvidingServices(currentDataItem.Rows[0]["visitid"].ToString());

            DateBox.Text = currentDataItem.Rows[0]["Дата"].ToString();
            NumberBox.Text = currentDataItem.Rows[0]["Номер"].ToString();
            BillAmountBox.Text = currentDataItem.Rows[0]["Сумма"].ToString();
            PaidChb.IsChecked = bool.Parse(currentDataItem.Rows[0]["Оплачено"].ToString());
            PaymentMethodBox.Text = currentDataItem.Rows[0]["Способ оплаты"].ToString();

            ServicesGrid.DataContext = currentDetailedDataItem;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ServicesGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }
    }
}
