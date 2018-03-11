using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
