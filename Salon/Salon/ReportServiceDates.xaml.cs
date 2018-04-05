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
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ReportServiceDates.xaml
    /// </summary>
    public partial class ReportServiceDates : Window
    {
        public ReportServiceDates()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionStr = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=Salon;Integrated Security=True";
            SqlConnection con = null;
            SqlCommand com = null;
            try
            {
                Excel.Application app = new Excel.Application();
                app.Visible = false;
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                app.Workbooks.Add(path.Substring(0, path.LastIndexOf('\\')) + "\\По видам услуг.xlsx");
                Excel.Workbook wb = app.Workbooks[1];
                Excel.Worksheet ws = app.Worksheets[1];
                con = new SqlConnection(connectionStr);
                con.Open();
                //ComboData name = (ComboData)FioCmbBox.SelectedItem;
                string sql = @"SELECT Service.Name as Наименование, COUNT(Service_ID) as Количество FROM Service, ProvidingServices, Visit
                                WHERE Service.ID_Service = ProvidingServices.Service_ID AND Visit.ID_Visit=ProvidingServices.Visit_ID
                                AND Visit.Date BETWEEN '" + StartDatePicker.SelectedDate.Value.ToString("s") + "' AND '" + EndDatePicker.SelectedDate.Value.ToString("s") + "'"+
                               "GROUP BY Name ORDER BY COUNT(Service_ID)";
                if (con != null)
                {
                    DataTable dt = new DataTable();
                    com = con.CreateCommand();
                    com.CommandText = sql;
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ws.Cells[i + 2, 1].Value2 = dt.Rows[i][0];
                        ws.Cells[i + 2, 2].Value2 = dt.Rows[i][1];
                    }
                    ws.Cells[1, 4].Value2 = StartDatePicker.SelectedDate.Value.ToString() + "-" + EndDatePicker.SelectedDate.Value.ToString();
                    app.Visible = true;
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
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
