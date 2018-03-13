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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {

        public Authorization()
        {
            InitializeComponent();           
        }
        private string server;
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            DBUser.Connection(server, LoginBox.Text, PassBox.Text);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DBUser.CreateUser(LoginBox.Text, PassBox.Text);
        }
    }
}
