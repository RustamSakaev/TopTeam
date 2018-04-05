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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var IsLog = DBUser.Connection(server, LoginBox.Text.Trim(), PassBox.Password.Trim());
            if (IsLog == true)
            {
                LogIn();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
                PassBox.Clear();
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            DBCore.Init(server);
            AddUser();
        }
        private void AddUser()
        {
            this.Hide();
            LoginBox.Clear();
            PassBox.Clear();
            RegistrationForm users = new RegistrationForm();
            users.Show();            
            users.Closed += (x, y) => { this.Show();};
        }
        private void LogIn()
        {
            this.Hide();
            PassBox.Clear();
            Users main = new Users();
            //main.UserId = DBUser.GetUserId(LoginBox.Text.Trim());
            //main.UserRole = DBUser.GetRoles(LoginBox.Text.Trim());
            //main.UserName = LoginBox.Text.Trim();
            main.Show();
            main.Closed += (x, y) => { this.Show();};
        }

        private void FormClosed(object sender, EventArgs e)
        {
            DBCore.Destroy();
            this.Close();
        }
    }
}
