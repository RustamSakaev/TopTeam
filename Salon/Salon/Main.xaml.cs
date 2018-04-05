using System;
using System.Data.SqlClient;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows;
using System.Windows.Forms;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();

           // DBCore.Init(@"LENOVO-PC"); 
           //DBCore.Init(@"ADMIN\SQLEXPRESS");//просто раскомментируй свою строку а не заменяй чужую


            
            //проверка роли залогиненного пользователя
            if (userRole == "Master")
            {
                ChangeUserItem.Visibility = Visibility.Collapsed;
                Items.Visibility = Visibility.Collapsed;
                WorkersExportItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                Items.Visibility = Visibility.Visible;
                ChangeUserItem.Visibility = Visibility.Visible;
                WorkersExportItem.Visibility = Visibility.Visible;
            }

        }
        private string userRole;
        public string UserRole
        {
            get { return userRole; }
            set { userRole = value; }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private void Prototype_Click(object sender, RoutedEventArgs e)
        {
            MasterPage master = new MasterPage();
            master.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DBCore.Destroy();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var form = new BillForm();
            form.ShowDialog();
        }
        private void Client_Click(object sender, RoutedEventArgs e)
        {
            ClientForm client = new ClientForm();
            client.ShowDialog();
        }

        private void Visit_Click(object sender, RoutedEventArgs e)
        {
            VisitForm visit = new VisitForm();
            visit.ShowDialog();
        }

        private void ProvidingService_Click(object sender, RoutedEventArgs e)
        {
            ProvidingServiceForm services = new ProvidingServiceForm();
            services.ShowDialog();
        }
       

        private void Tariff_Click(object sender, RoutedEventArgs e)
        {
            TariffForm tariff = new TariffForm();
            tariff.ShowDialog();
        }

        private void SpisokUslug_Click(object sender, RoutedEventArgs e)
        {
            ServiceForm serv = new ServiceForm();
            serv.ShowDialog();
        }

           
        private void EmployeeData_Click(object sender, RoutedEventArgs e)
        {
            string connectionStr = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=Salon;Integrated Security=True";
            SqlConnection con = null;
            SqlCommand com = null;
            try
            {
                Excel.Application app = new Excel.Application();
                app.Visible = false;
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                app.Workbooks.Add(path.Substring(0, path.LastIndexOf('\\')) + "\\По сотрудникам.xlsx"); 
                Excel.Workbook wb = app.Workbooks[1];
                Excel.Worksheet ws = app.Worksheets[1];
                con = new SqlConnection(connectionStr);
                con.Open();
                string sql = @"SELECT Surname as Фамилия, Worker.Name as Имя, DBirth as 'Дата рождения', MasterType.Name as 'Тип мастера' FROM MasterType, Worker, Worker_MasterType
                                WHERE Worker.ID_Worker = Worker_MasterType.Worker_ID AND MasterType.ID_MasterType = Worker_MasterType.MasterType_ID
                                ORDER BY MasterType.Name";
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
                        ws.Cells[i + 2, 4].Value2 = dt.Rows[i][3];
                        ws.Cells[i + 2, 3].Value2 = Convert.ToDateTime(dt.Rows[i][2]);
                    }
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

        private void EmployeeClient_Click(object sender, RoutedEventArgs e)
        {
            ChooseEmployeeForm employee = new ChooseEmployeeForm(1);
            employee.UserId = userId;
            employee.UserRole = userRole;
            employee.ShowDialog();
        }

        private void TypeService_Click(object sender, RoutedEventArgs e)
        {
            ReportServiceDates rep = new ReportServiceDates();
            rep.ShowDialog();            
        }

        private void EmployeeProfit_Click(object sender, RoutedEventArgs e)
        {
            ChooseEmployeeForm employee = new ChooseEmployeeForm(0);
            employee.UserId = userId;
            employee.UserRole = userRole;            
            employee.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var form = new BankCardForm();
            form.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            WorkerForm formWorker = new WorkerForm();
            formWorker.ShowDialog();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var form = new GiftCardForm();
            form.ShowDialog();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            ScheduleForm formSchedule = new ScheduleForm();
            formSchedule.ShowDialog();
        }

        private void Visits_Click_5(object sender, RoutedEventArgs e)
        {
            var form = new VisitForm();
            form.ShowDialog();
        }

        private void CabinetForm_Click(object sender, RoutedEventArgs e)
        {
            Kabinet kabinet = new Kabinet();
            kabinet.ShowDialog();                
        }

        private void UserRoleForm_Click(object sender, RoutedEventArgs e)
        {
            Users userroles = new Users();
            userroles.ShowDialog();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {            
            Authorization auth = (Authorization)Application.Current.Windows[0];
            this.Hide();
            auth.Visibility = Visibility.Visible;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void masteraButton_Click(object sender, RoutedEventArgs e)
        {
            WorkerForm formWorker = new WorkerForm();
            formWorker.ShowDialog();
        }

        private void tipMasteraButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void clientiButton_Click(object sender, RoutedEventArgs e)
        {
            ClientForm client = new ClientForm();
            client.ShowDialog();
        }

        private void tipUslugiButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceForm serv = new ServiceForm();
            serv.ShowDialog();
        }

        private void vidUslugiButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uslugiButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridView dgv = new DataGridView();

        }
    }
}
