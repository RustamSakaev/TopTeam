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

        public string Connection()
        {
            /*DATA SOURCE менять самому*/
            string con = @"Data Source=DESKTOP-H5176PR;Initial Catalog=Task3;Integrated Security=True";
            return con;
        }
        public string Connection(string login, string pass)
        {
            /*DATA SOURCE менять самому*/
            string con = @"Data Source=DESKTOP-H5176PR;Initial Catalog=Task3;User Id=" + login + ";  Password=" + pass + ";";
            return con;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Authorization_();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Registration();
        }

        public void Authorization_()
        {
            string connectionStr = Connection(LoginBox.Text, PassBox.Text);
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                if (con != null)
                { MessageBox.Show("Prohodi STALKER"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        public void Registration()
        {
            string connectionStr = Connection();
            SqlConnection con = null;
            SqlCommand com = null;
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                string sql = @"USE master "+
"create login "+LoginBox.Text+@" with password = '"+PassBox.Text+ @"', check_policy = off;
USE[Task3]
create user " + LoginBox.Text + @" from login " + LoginBox.Text + @";
exec sp_addrolemember 'db_datareader', '" + LoginBox.Text + "'";                
                if (con != null)
                {
                    com = con.CreateCommand();
                    com.CommandText = sql;
                    com.ExecuteNonQuery();
                    MessageBox.Show("Gotovo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
    }
}
