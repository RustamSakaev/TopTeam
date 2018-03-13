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
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ServiceForm.xaml
    /// </summary>
    public partial class ServiceForm : Window
    {
        public ServiceForm()
        {
            InitializeComponent();
        }
        public string Connection()
        {

            string conn = @"Data Source=LENOVO-PC;Initial Catalog=Salon;Integrated Security=True";
            return conn;
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            string str = "select Service.name as [Наименование], TypeService.Name as [Тип услуги], KindService.Name as [Вид услуги] from Service inner join KindService on Service.KindService_ID = KindService.ID_KindService inner join TypeService on Service.TypeService_ID = TypeService.ID_TypeService";
            DataTable dt = DataTool(str);
            ServiceGrid.ItemsSource = dt.DefaultView;
        }
        public DataTable DataTool(string query)
        {
            string connStr = Connection();
            SqlConnection conn = null;
            SqlCommand comm = null;
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                if (conn != null)
                {
                    comm = conn.CreateCommand();
                    comm.CommandText = query;
                    SqlDataAdapter adapter = new SqlDataAdapter(comm);
                    SqlCommandBuilder bild = new SqlCommandBuilder(adapter);
                    adapter.Fill(dt);
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddServiceForm add_service = new AddServiceForm();
            add_service.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditServiceForm ed = new EditServiceForm();
            ed.ShowDialog();
        }
    }
}
