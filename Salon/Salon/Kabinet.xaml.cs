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
using System.Data.SqlClient;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для Kabinet.xaml
    /// </summary>
    public partial class Kabinet : Window
    {
        public Kabinet()
        {
            InitializeComponent();
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Connection()
        {
            /*DATA SOURCE менять самому*/
            string con = @"Data Source=DESKTOP-H5176PR;Initial Catalog=Task3;Integrated Security=True";
            return con;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ChangePass(OldPassBox.Text,NewPassBox.Text);
        }

        public void ChangePass(string oldP,string newP)
        {
            string connectionStr = Connection();
            SqlConnection con = null;
            SqlCommand com = null;
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                string sql = "ALTER LOGIN "+userName+" WITH PASSWORD = '"+newP+"' OLD_PASSWORD = '"+oldP+"'";
                if (con != null)
                {
                    com = con.CreateCommand();
                    com.CommandText = sql;
                    com.ExecuteNonQuery();
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
