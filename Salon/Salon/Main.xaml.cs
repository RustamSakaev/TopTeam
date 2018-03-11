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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();

            DBCore.Init("DESKTOP-D3KKSHS\\SQLEXPRESS");

            var result = DBBill.GetBills();
        }

        private void Prototype_Click(object sender, RoutedEventArgs e)
        {
            MasterPage master = new MasterPage();
            master.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DBCore.Destroy();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new BillForm();
            form.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var form = new PaymentMethodForm();
            form.ShowDialog();
        }
    }
}
