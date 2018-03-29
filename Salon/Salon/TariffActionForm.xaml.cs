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
    /// Логика взаимодействия для TariffActionForm.xaml
    /// </summary>
    public partial class TariffActionForm : Window
    {
        private readonly FormState _state;
        private readonly DataTable _currentDataItem;
        private readonly DataTable _CurServiceDataItem;
        public TariffActionForm(FormState state, string id=null, string serv_id=null)
        {
            InitializeComponent();
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderLabel.Content = "Редактирование тарифа";
                    _currentDataItem = DBTariff.GetTariff(id);
                    PriceBox.Text = _currentDataItem.Rows[0]["Стоимость"].ToString();
                    StartDatePicker.SelectedDate = Convert.ToDateTime(_currentDataItem.Rows[0]["Дата начала"]);
                    _CurServiceDataItem = DBService.GetService(serv_id);
                    foreach (DataRow serv in DBService.GetServices().Rows)
                    {
                        ServiceCmbBox.Items.Add(serv["Наименование"]);
                    }
                    ServiceCmbBox.SelectedValue = _CurServiceDataItem.Rows[0]["Наименование"].ToString();
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление тарифа";
                    foreach (DataRow serv in DBService.GetServices().Rows)
                    {
                        ServiceCmbBox.Items.Add(serv["Наименование"]);
                    }
                    break;
            }
        }

        private void ServiceForm_Click(object sender, RoutedEventArgs e)
        {
            ServiceForm service = new ServiceForm();
            service.ShowDialog();
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
    }
}
