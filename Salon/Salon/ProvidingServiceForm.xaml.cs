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
    /// Логика взаимодействия для ProvidingServiceForm.xaml
    /// </summary>
    public partial class ProvidingServiceForm : Window
    {
        public ProvidingServiceForm()
        {
            InitializeComponent();
        }
        private void FullSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchStack.Visibility = Visibility.Collapsed;
            FullSearchGroup.Visibility = Visibility.Visible;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SearchStack.Visibility = Visibility.Visible;
            FullSearchGroup.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
