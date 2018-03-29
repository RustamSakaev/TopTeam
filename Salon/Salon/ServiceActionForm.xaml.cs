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
using Salon.Database;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ServiceActionForm.xaml
    /// </summary>
    public partial class ServiceActionForm : Window
    {
        private readonly FormState _state;
        private readonly DataTable _currentDataItem;
        private readonly DataTable _CurTypeServiceDataItem;
        private readonly DataTable _CurKindServiceDataItem;
        public ServiceActionForm(FormState state, string serv_id=null, string type_id=null, string kind_id=null)
        {
            InitializeComponent();
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderLabel.Content = "Редактирование услуги";
                    _currentDataItem = DBService.GetService(serv_id);
                    NameBox.Text = _currentDataItem.Rows[0]["Наименование"].ToString();
                    _CurTypeServiceDataItem = DBTypeService.GetTypeService(type_id);

                    foreach (DataRow type in DBTypeService.GetTypeServices().Rows)
                    {
                        TypeServiceCmbBox.Items.Add(type["Наименование"]);
                    }
                    TypeServiceCmbBox.SelectedValue = _CurTypeServiceDataItem.Rows[0]["Наименование"].ToString();

                    _CurKindServiceDataItem = DBKindService.GetKindService(kind_id);
                    foreach (DataRow kind in DBKindService.GetKindServices().Rows)
                    {
                        KindServiceCmbBox.Items.Add(kind["Наименование"]);
                    }
                    KindServiceCmbBox.SelectedValue = _CurKindServiceDataItem.Rows[0]["Наименование"].ToString();
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление услуги";
                   
                    foreach (DataRow type in DBTypeService.GetTypeServices().Rows)
                    {
                        TypeServiceCmbBox.Items.Add(type["Наименование"]);
                    }
                   
                    foreach (DataRow kind in DBKindService.GetKindServices().Rows)
                    {
                        KindServiceCmbBox.Items.Add(kind["Наименование"]);
                    }
                    break;
            }
        }
        public string Connection()
        {
            string conn = @"Data Source=LENOVO-PC;Initial Catalog=Salon;Integrated Security=True";
            return conn;
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

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TypeServiceFormButton_Click(object sender, RoutedEventArgs e)
        {
            TypeServiceForm type = new TypeServiceForm();
            type.ShowDialog();
        }

        private void KindServiceFormButton_Click(object sender, RoutedEventArgs e)
        {
            KindService kind = new KindService();
            kind.ShowDialog();
        }

       
    }
}
