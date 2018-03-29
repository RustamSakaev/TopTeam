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
using Salon.Misc;
using Salon.Database;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ServiceForm.xaml
    /// </summary>
    public partial class ServiceForm : Window
    {
        private DataTable _currentFormData = new DataTable();
        public ServiceForm()
        {
            InitializeComponent();
        }
        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; ServiceGrid.ItemsSource = _currentFormData.DefaultView; }
        }
        public string Connection()
        {

            string conn = @"Data Source=LENOVO-PC;Initial Catalog=Salon;Integrated Security=True";
            return conn;
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            //string str = "select Service.name as [Наименование], TypeService.Name as [Тип услуги], KindService.Name as [Вид услуги] from Service inner join KindService on Service.KindService_ID = KindService.ID_KindService inner join TypeService on Service.TypeService_ID = TypeService.ID_TypeService";
           // DataTable dt = DataTool(str);
           // ServiceGrid.ItemsSource = dt.DefaultView;

            CurrentFormData = DBService.GetServices();
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
            ServiceActionForm serv = new ServiceActionForm(FormState.Add);
            serv.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var servidx = ((DataView)ServiceGrid.ItemsSource).Table.Columns.IndexOf("id");

            if (servidx == -1) return;

            var servid = ((DataRowView)ServiceGrid.SelectedItem)?.Row[servidx].ToString();

            if (servid == null) return;

            var typeidx = ((DataView)ServiceGrid.ItemsSource).Table.Columns.IndexOf("type_id");

            //if (typeidx == -1) return;

            var typeid = ((DataRowView)ServiceGrid.SelectedItem)?.Row[typeidx].ToString();

            //if (typeid == null) return;

            var kindidx = ((DataView)ServiceGrid.ItemsSource).Table.Columns.IndexOf("kind_id");

            //if (kindidx == -1) return;

            var kindid = ((DataRowView)ServiceGrid.SelectedItem)?.Row[kindidx].ToString();

           // if (kindid == null) return;

            ServiceActionForm serv_edit = new ServiceActionForm(FormState.Edit, servid, typeid, kindid);
            serv_edit.ShowDialog();
        }

      
    }
}
