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
    /// Логика взаимодействия для KindServiceActionForm.xaml
    /// </summary>
    public partial class KindServiceActionForm : Window
    {
        private readonly FormState _state;
        public KindServiceActionForm(FormState state)
        {
            InitializeComponent();
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderLabel.Content = "Редактирование вида услуги";
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление вида услуги";
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
