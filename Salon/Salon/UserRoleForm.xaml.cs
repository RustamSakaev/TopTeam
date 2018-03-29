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
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            DBCore.Init(@"DESKTOP-H5176PR\MSSQLSERVER01");
            string str = "select name from sys.database_principals where [type] <> 'r' and [name] not in ( 'dbo', 'sys', 'INFORMATION_SCHEMA') order by name";
            DataTable dt = DBCore.GetData(str);
            foreach (DataRow dr in dt.Rows)
            {
                UserslistBox.Items.Add(dr["name"].ToString());
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoleslistBox.Items.Clear();
            if (UserslistBox.Items.Count != 0)
            {
                string str = "EXEC sp_helpuser '"+ UserslistBox.SelectedValue+"'";
                DataTable dt = DBCore.GetData(str);
                foreach (DataRow dr in dt.Rows)
                {
                    RoleslistBox.Items.Add(dr["RoleName"].ToString());
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ShowRole();
            First.Visibility = Visibility.Collapsed;
            Second.Visibility = Visibility.Visible;
        }
        public void ShowRole()
        {
            AllRoleslistBox.Items.Clear();
            List<string> NoUserRole = DBUser.GetRoles(UserslistBox.SelectedValue.ToString());
            foreach (var no in NoUserRole)
            {
                AllRoleslistBox.Items.Add(no.ToString());
            }          
        }

        public void AddRole(string user, string role)
        {
            string sql = "EXEC sp_addrolemember '" + role + "', '" + user + "'; ";
            DBCore.ExecuteSql(sql);
        }

        public void DelRole(string user, string role)
        {
            string sql = "EXEC sp_droprolemember '" + role + "', '" + user + "'; ";
            DBCore.ExecuteSql(sql);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            foreach (string item in AllRoleslistBox.SelectedItems)
            {
                AddRole(UserslistBox.SelectedValue.ToString(), item.ToString());
            }
            RoleslistBox.Items.Clear();
            if (UserslistBox.Items.Count != 0)
            {
                string str = "EXEC sp_helpuser '" + UserslistBox.SelectedValue + "'";
                DataTable dt = DBCore.GetData(str);
                foreach (DataRow dr in dt.Rows)
                {
                    RoleslistBox.Items.Add(dr["RoleName"].ToString());
                }
            }
            First.Visibility = Visibility.Visible;
            Second.Visibility = Visibility.Collapsed;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            foreach (string item in RoleslistBox.SelectedItems)
            {
                DelRole(UserslistBox.SelectedValue.ToString(), item.ToString());
            }
            RoleslistBox.Items.Clear();
            if (UserslistBox.Items.Count != 0)
            {
                string str = "EXEC sp_helpuser '" + UserslistBox.SelectedValue + "'";
                DataTable dt = DBCore.GetData(str);
                foreach (DataRow dr in dt.Rows)
                {
                    RoleslistBox.Items.Add(dr["RoleName"].ToString());
                }
            }
        }
    }
}
