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
        public string Connection()
        {
            /*DATA SOURCE менять самому*/
            string con = @"Data Source=DESKTOP-H5176PR;Initial Catalog=Task3;Integrated Security=True";
            return con;
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            string str = "use Task3 select name from sys.database_principals where [type] <> 'r' and [name] not in ( 'dbo', 'sys', 'INFORMATION_SCHEMA') order by name";
            DataTable dt = ZaprosList(str);
            foreach (DataRow dr in dt.Rows)
            {
                listBox.Items.Add(dr["name"].ToString());
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBox1.Items.Clear();
            if (listBox.Items.Count != 0)
            {
                string str = "use Task3 EXEC sp_helpuser '"+listBox.SelectedValue+"'";
                DataTable dt = ZaprosList(str);
                foreach (DataRow dr in dt.Rows)
                {
                    listBox1.Items.Add(dr["RoleName"].ToString());
                }
            }
        }
        public DataTable ZaprosList(string zapros)
        {
            string connectionStr = Connection();
            SqlConnection con = null;
            SqlCommand com = null;
            DataTable dt = new DataTable();
            try
            {
                con = new SqlConnection(connectionStr);
                con.Open();
                if (con != null)
                {
                    com = con.CreateCommand();
                    com.CommandText = zapros;
                    SqlDataAdapter adap = new SqlDataAdapter(com);
                    SqlCommandBuilder bild = new SqlCommandBuilder(adap);
                    adap.Fill(dt);
                    return dt;
                }
                else
                { return dt; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return dt;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //AddRole();
            Osnova.Visibility = Visibility.Hidden;
        }
        public void AddRole1()
        {
            //listBox2.Items.Clear();
            //List<string> user = new List<string>();
            //List<string> db = new List<string>();
            //string str = "use Task3 EXEC sp_helpuser '" + listBox.SelectedValue + "'";
            //DataTable dt = ZaprosList(str);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    user.Add(dr["RoleName"].ToString());
            //}
            //string str1 = "use Task3 EXEC sp_helprole";
            //DataTable dt1 = ZaprosList(str1);
            //foreach (DataRow dr in dt1.Rows)
            //{
            //    db.Add(dr["RoleName"].ToString());
            //}
            //List<string> NoUserRole = db.Except(user).ToList();
            //foreach (var no in NoUserRole)
            //{
            //    listBox2.Items.Add(no.ToString());
            //}
            //listBox2.Visibility = Visibility.Visible;
        }
        public void LoseFocus(object sender, RoutedEventArgs e)
        {
            //listBox2.Visibility = Visibility.Hidden;
        }
    }
}
