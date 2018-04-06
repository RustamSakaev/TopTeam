using System;
using System.Data.SqlClient;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

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
            //DBCore.Init("DESKTOP-D3KKSHS\\SQLEXPRESS");


            
            //проверка роли залогиненного пользователя
            

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
                app.Workbooks.Add(path.Substring(0, path.LastIndexOf('\\')) + "\\По сотрудникам.xlsx"); //создайте у себя на компе в папке,где лежит этот проект
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
                System.Windows.MessageBox.Show(ex.Message);
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
            employee.ShowDialog();
        }

        private void TypeService_Click(object sender, RoutedEventArgs e)
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
                string sql = @"SELECT Name as Наименование, COUNT(Service_ID) as Количество FROM Service, ProvidingServices
                                WHERE Service.ID_Service = ProvidingServices.Service_ID
                                GROUP BY Name ORDER BY COUNT(Service_ID)";
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
                    app.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        private void EmployeeProfit_Click(object sender, RoutedEventArgs e)
        {
            ChooseEmployeeForm employee = new ChooseEmployeeForm(0);
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
            kabinet.UserName = userName;
            kabinet.ShowDialog();                
        }

        private void UserRoleForm_Click(object sender, RoutedEventArgs e)
        {
            Users userroles = new Users();
            userroles.ShowDialog();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {            
            Authorization auth = (Authorization)System.Windows.Application.Current.Windows[0];
            this.Hide();
            auth.Visibility = Visibility.Visible;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            ////////////РАНДОМНЕНЬКО
            DataGridView dgv = new DataGridView();
            wfh.Child = dgv;

            Random r = new Random();
            primarySetupDgv(dgv);
            setColorsDgv(dgv);

            DataTable dt = getDTTestData("Бурмин", r);

            fillDgvSchedule(dt, dgv);

            DataTable dt1 = getDTTestData("Сакаев", r);

            fillDgvSchedule(dt1, dgv);

            DataTable dt2 = getDTTestData("Холодняк", r);

            fillDgvSchedule(dt2, dgv);


        }
        private void primarySetupDgv(DataGridView dgv)
        {
            dgv.Rows.Clear();
            dgv.Rows.Clear();

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;

            dgv.RowHeadersVisible = false;

            dgv.ColumnHeadersVisible = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Time", HeaderText = "Time", Width = 100 });
            dgv.Rows.Add("Times");

           

            TimeSpan timeFrom = new TimeSpan(Convert.ToInt32("08"), Convert.ToInt32("00"), 0);
            TimeSpan timeBefore = new TimeSpan(Convert.ToInt32("20"), Convert.ToInt32("00"), 0);
            TimeSpan timeSpan = timeBefore - timeFrom;
            int sectionsCount = 0;
            if (timeSpan.TotalMinutes % 5 == 0) { sectionsCount = Convert.ToInt32(timeSpan.TotalMinutes / 15); }

            for (int z = 0; z < sectionsCount; z++)
            {

                dgv.Rows.Add(Convert.ToDateTime("04.04.2018 0:00:00").AddHours(Convert.ToInt32("08") + Convert.ToInt32(z / 4)).AddMinutes(Convert.ToInt32("00") + Convert.ToInt32(z % 4) * 15).ToShortTimeString());

            }

        }
        private void fillDgvSchedule(DataTable dt, DataGridView dgv)
        {
            dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = dgv.Columns.Count.ToString(), HeaderText = "", Width = 70 });
            //dgv.Columns.Add(new DataGridViewTextBoxColumn() { Name = "1", HeaderText = "Бурмин", Width = 70 });
            
            for (int i = 1; i < dgv.Rows.Count; i++)
            {
                bool flag = false;
                for (int z = 0; z < dt.Rows.Count; z++)
                {
                    if (Convert.ToString(dgv.Rows[i].Cells[0].Value) == Convert.ToString(dt.Rows[z].Field<string>("TStart")))
                    {
                        Console.WriteLine(Convert.ToString(dt.Rows[z].Field<string>("Busy")));
                        if (Convert.ToString(dt.Rows[z].Field<string>("Busy")) == "0")
                        {
                            dgv.Rows[i].Cells[dgv.Columns.Count - 1].Style.BackColor = Color.Green;
                            dgv.Rows[i].Cells[dgv.Columns.Count - 1].Value = "";
                            flag = true;
                            dgv.Rows[0].Cells[dgv.Columns.Count - 1].Value = dt.Rows[z].Field<string>("Worker_ID");
                            break;
                        }
                        if (Convert.ToString(dt.Rows[z].Field<string>("Busy")) == "1")
                        {
                            dgv.Rows[i].Cells[dgv.Columns.Count - 1].Style.BackColor = Color.Red;
                            dgv.Rows[i].Cells[dgv.Columns.Count - 1].Value = "";
                            flag = true;
                            dgv.Rows[0].Cells[dgv.Columns.Count - 1].Value = dt.Rows[z].Field<string>("Worker_ID");
                            break;
                        }
                        //dgv.Columns[dgv.Columns.Count - 1].HeaderText = dt.Rows[z].Field<string>("Worker_ID");
                        
                        //dgv.Rows[i].Cells[dgv.Columns.Count - 1].Style.BackColor = Color.FromArgb(84, 96, 122);
                    }

                }
                if (!flag)
                {
                    
                    dgv.Rows[i].Cells[dgv.Columns.Count - 1].Style.BackColor = Color.FromArgb(84, 96, 122);
                }
                
            }
            dgv.Rows[0].Cells[dgv.Columns.Count - 1].Style.BackColor = Color.FromArgb(43, 48, 67);
            dgv.ClearSelection();

        }
        private DataTable getDTTestData(string name, Random r)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Worker_ID");
            dt.Columns.Add("Date");
            dt.Columns.Add("TStart");
            dt.Columns.Add("TEnd");
            dt.Columns.Add("Busy");

            TimeSpan timeFrom = new TimeSpan(Convert.ToInt32("08"), Convert.ToInt32("00"), 0);
            TimeSpan timeBefore = new TimeSpan(Convert.ToInt32("20"), Convert.ToInt32("00"), 0);
            TimeSpan timeSpan = timeBefore - timeFrom;
            int sectionsCount = 0;
            if (timeSpan.TotalMinutes % 5 == 0) { sectionsCount = Convert.ToInt32(timeSpan.TotalMinutes / 15); }

            
            
            int ot = r.Next(4, 9);
            
            int doo = r.Next(37, 46);
            
            int ii = 0;
            for (int z = 0; z < sectionsCount; z++)
            {

                if (z > ot && z < doo)
                {
                    dt.Rows.Add();
                    dt.Rows[ii]["Worker_ID"] = name;
                    //Console.WriteLine(dt.Rows[ii]["Worker_ID"]);
                    dt.Rows[ii]["Date"] = "";
                    dt.Rows[ii]["TStart"] = Convert.ToDateTime("04.04.2018 0:00:00").AddHours(Convert.ToInt32("08") + Convert.ToInt32(z / 4)).AddMinutes(Convert.ToInt32("00") + Convert.ToInt32(z % 4) * 15).ToShortTimeString().ToString();
                    Console.WriteLine(Convert.ToDateTime("04.04.2018 0:00:00").AddHours(Convert.ToInt32("08") + Convert.ToInt32(z / 4)).AddMinutes(Convert.ToInt32("00") + Convert.ToInt32(z % 4) * 15).ToShortTimeString().ToString());
                    int aaa = r.Next(0, 2);
                    if (aaa == 1)
                    {
                        dt.Rows[ii]["Busy"] = "1";
                    }
                    else
                    {
                        dt.Rows[ii]["Busy"] = "0";
                    }
                    //Console.WriteLine(dt.Rows[ii]["Busy"]);

                    ii++;
                    

                }


            }
            return dt;

        }
        private void setColorsDgv(DataGridView dgv)
        {
            dgv.ForeColor = Color.White;
            dgv.BackgroundColor = Color.FromArgb(72, 76, 101);
            
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                for (int j = 0; j < dgv.Rows.Count; j++)
                {
                    if (j == 0) //Цвет "Заголовков" столбцов
                    {
                        dgv.Rows[0].Cells[i].Style.BackColor = Color.FromArgb(43, 48, 67);
                        continue;
                    }

                    if (Convert.ToString(dgv.Rows[j].Cells[i].Value) == "+")
                    {
                        dgv.Rows[j].Cells[i].Style.BackColor = Color.Green;
                        dgv.Rows[j].Cells[i].Value = "";
                        continue;
                    }
                    if (Convert.ToString(dgv.Rows[j].Cells[i].Value) == "-")
                    {
                        dgv.Rows[j].Cells[i].Style.BackColor = Color.Red;
                        dgv.Rows[j].Cells[i].Value = "";
                        continue;
                    }
                    if (Convert.ToString(dgv.Rows[j].Cells[i].Value) == " ")
                    {
                        //dgv.Rows[j].Cells[i].Style.BackColor = Color.White;
                        //continue;
                    }
                    dgv.Rows[j].Cells[i].Style.BackColor = Color.FromArgb(84, 96, 122);
                }

            }
        }

        private void Calendar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
        }

        private void Calendar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ////////////РАНДОМНЕНЬКО
            DataGridView dgv = new DataGridView();
            wfh.Child = dgv;

            Random r = new Random();
            primarySetupDgv(dgv);
            setColorsDgv(dgv);

            DataTable dt = getDTTestData("Бурмин", r);

            fillDgvSchedule(dt, dgv);

            DataTable dt1 = getDTTestData("Сакаев", r);

            fillDgvSchedule(dt1, dgv);

            DataTable dt2 = getDTTestData("Холодняк", r);

            fillDgvSchedule(dt2, dgv);


        }
    }
}
