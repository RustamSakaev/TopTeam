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
using System.Data;


namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        public Users()
        {
            InitializeComponent();
            UserCmbBox.ItemsSource = DBUser.GetUsers().DefaultView;
            UserCmbBox.DisplayMemberPath = "ФИО";
            UserCmbBox.SelectedValuePath = "Логин";         
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
        }

        public void AddRole(string login, string role)
        {
            string sql = "use [Salon] EXEC sp_addrolemember '"+role+"', '"+ login + "'; ";
            DBCore.ExecuteSql(sql);
        }

        public void DelRole(string login)
        {
            string sql = "ALTER ROLE db_admin DROP MEMBER "+ login;
            DBCore.ExecuteSql(sql);
            sql = "ALTER ROLE db_master DROP MEMBER " + login;
            DBCore.ExecuteSql(sql);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string login=UserCmbBox.SelectedValue.ToString();
            string role = RoleCmbBox.Text == "Мастер" ? "db_master" : "db_admin";
            DelRole(login);
            AddRole(login, role);
        }
        private void PostFormButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            UserRoleActionForm main = new UserRoleActionForm();
            main.Owner = this;
            main.ShowDialog();
            main.Closed += (x, y) => { this.Show(); };
        }
    }
}
