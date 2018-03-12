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
    /// Логика взаимодействия для AddTypeMasterService1.xaml
    /// </summary>
    public partial class AddTypeMasterService1 : Window
    {
        public AddTypeMasterService1()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MasterFormButton_Click(object sender, RoutedEventArgs e)
        {
            TypeMasterForm tm = new TypeMasterForm();
            tm.ShowDialog();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ServiceFormButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceForm serv = new ServiceForm();
            serv.ShowDialog();
        }
    }
}
