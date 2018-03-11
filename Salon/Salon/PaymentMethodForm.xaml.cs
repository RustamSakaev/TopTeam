using System.Windows;

namespace Salon
{
    /// <summary>
    /// Interaction logic for PaymentMethodForm.xaml
    /// </summary>
    public partial class PaymentMethodForm : Window
    {
        public PaymentMethodForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var data = DBPaymentMethod.GetPaymentMethods();

            PaymentMethodGrid.DataContext = data.DefaultView;
        }
    }
}
