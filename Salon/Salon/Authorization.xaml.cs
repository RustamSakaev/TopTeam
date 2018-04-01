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
using System.Data;

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
        private string server = @"DESKTOP-H5176PR\MSSQLSERVER01";

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var IsLog = DBUser.Connection(server, LoginBox.Text, PassBox.Text);
            if (IsLog == true)
            {
                LogIn();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DBCore.Init(server);
            AddUser();
        }
        private void AddUser()
        {
            this.Hide();
            RegistrationForm users = new RegistrationForm();
            users.Show();
            users.Title = "Регистрация";
            users.Closed += (x, y) => { this.Show(); PassBox.Text = ""; };
        }
        private void LogIn()
        {
            this.Hide();
            Main main = new Main();
            main.Show();
            main.Closed += (x, y) => { this.Show(); PassBox.Text = ""; };
        }
    }
}
