using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
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

namespace Salon
{
    public class ComboData
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public partial class ChooseEmployeeForm : System.Windows.Window
    {
        int mode;

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

        public ChooseEmployeeForm()
        {
            InitializeComponent();
            if (mode == 0)
            { this.Title = "Доходы по сотруднику"; }
            if (mode == 1)
            { this.Title = "Количество обслуженных клиентов по сотруднику"; }
            string connectionStr = Connection();
            SqlConnection con = null;
            SqlCommand com = null;           
            if (userRole=="Master")
            {
                try
                {
                    con = new SqlConnection(connectionStr);
                    con.Open();
                    string sql = @"SELECT ID_Worker, Surname, Name FROM Worker where ID_Worker="+userId;

                    if (con != null)
                    {
                        DataTable dt = new DataTable();
                        com = con.CreateCommand();
                        com.CommandText = sql;
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                        List<ComboData> list = new List<ComboData>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            list.Add(new ComboData { Id = Convert.ToInt32(dt.Rows[i][0]), Value = Convert.ToString(dt.Rows[i][1]) + " " + Convert.ToString(dt.Rows[i][2]) });
                        }
                        FioCmbBox.DisplayMemberPath = "Value";
                        FioCmbBox.SelectedValuePath = "Id";
                        FioCmbBox.ItemsSource = list;
                        FioCmbBox.IsEnabled = false;
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
            else
            {
                try
                {
                    con = new SqlConnection(connectionStr);
                    con.Open();
                    string sql = @"SELECT ID_Worker, Surname, Name FROM Worker";
                    if (con != null)
                    {
                        DataTable dt = new DataTable();
                        com = con.CreateCommand();
                        com.CommandText = sql;
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                        List<ComboData> list = new List<ComboData>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            list.Add(new ComboData { Id = Convert.ToInt32(dt.Rows[i][0]), Value = Convert.ToString(dt.Rows[i][1]) + " " + Convert.ToString(dt.Rows[i][2]) });
                        }
                        FioCmbBox.DisplayMemberPath = "Value";
                        FioCmbBox.SelectedValuePath = "Id";
                        FioCmbBox.ItemsSource = list;
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

        public string Connection()
        {
            string con = @"Data Source=DESKTOP-H5176PR\MSSQLSERVER01;Initial Catalog=Salon;Integrated Security=True";
            return con;
        }

        public ChooseEmployeeForm(int mode)
        {
            InitializeComponent();
            this.mode = mode;
            if (mode == 0) 
            { this.Title = "Доходы по сотруднику"; }
            if (mode == 1)
            { this.Title = "Количество обслуженных клиентов по сотруднику"; }

            string connectionStr = Connection();
            SqlConnection con = null;
            SqlCommand com = null;
            if (userRole == "Master")
            {
                try
                {
                    con = new SqlConnection(connectionStr);
                    con.Open();
                    string sql = @"SELECT ID_Worker, Surname FROM Worker WHERE ID_Worker="+userId;
                    if (con != null)
                    {
                        DataTable dt = new DataTable();
                        com = con.CreateCommand();
                        com.CommandText = sql;
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                        FioCmbBox.DisplayMemberPath = "Surname";
                        FioCmbBox.SelectedValuePath = "ID_Worker";
                        List<DataRow> list = dt.AsEnumerable().ToList();
                        FioCmbBox.ItemsSource = list;
                        FioCmbBox.IsEnabled = false;
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
            else
            {
                try
                {
                    con = new SqlConnection(connectionStr);
                    con.Open();
                    string sql = @"SELECT ID_Worker, Surname FROM Worker";
                    if (con != null)
                    {
                        DataTable dt = new DataTable();
                        com = con.CreateCommand();
                        com.CommandText = sql;
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                        List<DataRow> list = dt.AsEnumerable().ToList();
                        FioCmbBox.ItemsSource = dt.DefaultView;
                        FioCmbBox.DisplayMemberPath = "Surname";
                        FioCmbBox.SelectedValuePath = "ID_Worker";
                        
                       
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

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if(mode == 0)
            {
                EmployeeProfit();
            }
            else
            {
                EmployeeClient();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }
        
        public void EmployeeProfit()
        {
            string connectionStr = Connection();
            SqlConnection con = null;
            SqlCommand com = null;
            if (userRole=="Master")
            {
                try
                {
                    Excel.Application app = new Excel.Application();
                    app.Visible = false;
                    string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    app.Workbooks.Add(path.Substring(0, path.LastIndexOf('\\')) + "\\Доходы по сотруднику.xlsx");
                    Excel.Workbook wb = app.Workbooks[1];
                    Excel.Worksheet ws = app.Worksheets[1];
                    con = new SqlConnection(connectionStr);
                    con.Open();
                    ComboData name = new ComboData(); 
                    name.Id = Convert.ToInt32(FioCmbBox.SelectedItem);
                    name.Value = Convert.ToString(FioCmbBox.SelectedItem);
                    string sql = @"SELECT Surname as Фамилия, Name as Имя, SUM(BillAmount) as Сумма FROM Worker, Bill, Visit 
                                WHERE Visit.ID_Visit=Bill.Visit_ID AND Worker.ID_Worker=Visit.Worker_ID AND Worker.ID_Worker=" + userId +
                                "AND Bill.Date BETWEEN '" + FromPicker.SelectedDate.Value.ToString("s") + "' AND '" + ToPicker.SelectedDate.Value.ToString("s") + "'" +
                                "GROUP BY surname,name";
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
                            ws.Cells[i + 2, 3].Value2 = dt.Rows[i][2];                          
                        }
                        ws.Cells[1, 5].Value2 = FromPicker.SelectedDate.Value.ToString() + "-" + ToPicker.SelectedDate.Value.ToString();
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
            else
            {
                try
                {
                    Excel.Application app = new Excel.Application();
                    app.Visible = false;
                    string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    app.Workbooks.Add(path.Substring(0, path.LastIndexOf('\\')) + "\\Доходы по сотруднику.xlsx");
                    Excel.Workbook wb = app.Workbooks[1];
                    Excel.Worksheet ws = app.Worksheets[1];
                    con = new SqlConnection(connectionStr);
                    con.Open();
                    //ComboData name = (ComboData)FioCmbBox.SelectedItem;
                    ComboData name = new ComboData();
                    name.Id = Convert.ToInt32(FioCmbBox.SelectedValue);
                    name.Value = Convert.ToString(FioCmbBox.Text);
                    string sql = @"SELECT Surname as Фамилия, Name as Имя, SUM(BillAmount) as Сумма FROM Worker, Bill, Visit 
                                WHERE Visit.ID_Visit=Bill.Visit_ID AND Worker.ID_Worker=Visit.Worker_ID
                                AND Surname = '" + name.Value.Split(' ')[0] + @"'                                
                                AND Bill.Date BETWEEN '" + FromPicker.SelectedDate.Value.ToString("s") + "' AND '" + ToPicker.SelectedDate.Value.ToString("s") + "'" +
                                "GROUP BY surname,name";
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
                            ws.Cells[i + 2, 3].Value2 = dt.Rows[i][2];                           
                        }
                        ws.Cells[1, 5].Value2 = FromPicker.SelectedDate.Value.ToString() + "-" + ToPicker.SelectedDate.Value.ToString();
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
           
        }
        public void EmployeeClient()
        {
            string connectionStr = Connection();
            SqlConnection con = null;
            SqlCommand com = null;
            if (userRole=="Master")
            {
                try
                {
                    Excel.Application app = new Excel.Application();
                    app.Visible = false;
                    string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    app.Workbooks.Add(path.Substring(0, path.LastIndexOf('\\')) + "\\Количество обслуженных клиентов по сотруднику.xlsx");
                    Excel.Workbook wb = app.Workbooks[1];
                    Excel.Worksheet ws = app.Worksheets[1];
                    con = new SqlConnection(connectionStr);
                    con.Open();
                    ComboData name = (ComboData)FioCmbBox.SelectedItem;
                    string sql = @"SELECT Surname as Фамилия, Name as Имя, COUNT(Client_ID) as Количество FROM Worker, Visit
                                WHERE Worker.ID_Worker = Visit.Worker_ID AND Worker.ID_Worker="+userId+                               
                                "AND Date BETWEEN '" + FromPicker.SelectedDate.Value.ToString("s") + "' AND '" + ToPicker.SelectedDate.Value.ToString("s") + @"'
                                GROUP BY Surname, Name";
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
                            ws.Cells[i + 2, 4].Value2 = dt.Rows[i][2];                           
                        }
                        ws.Cells[1, 5].Value2 = FromPicker.SelectedDate.Value.ToString() + "-" + ToPicker.SelectedDate.Value.ToString();
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
            else
            {
                try
                {
                    Excel.Application app = new Excel.Application();
                    app.Visible = false;
                    string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    app.Workbooks.Add(path.Substring(0, path.LastIndexOf('\\')) + "\\Количество обслуженных клиентов по сотруднику.xlsx");
                    Excel.Workbook wb = app.Workbooks[1];
                    Excel.Worksheet ws = app.Worksheets[1];
                    con = new SqlConnection(connectionStr);
                    con.Open();
                    ComboData name = (ComboData)FioCmbBox.SelectedItem;
                    string sql = @"SELECT Surname as Фамилия, Name as Имя, COUNT(Client_ID) as Количество FROM Worker, Visit
                                WHERE Worker.ID_Worker = Visit.Worker_ID 
                                AND Surname = '" + name.Value.Split(' ')[0] + @"' 
                                AND Name = '" + name.Value.Split(' ')[1] + @"'
                                AND Date BETWEEN '" + FromPicker.SelectedDate.Value.ToString("s") + "' AND '" + ToPicker.SelectedDate.Value.ToString("s") + @"'
                                GROUP BY Surname, Name";
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
                            ws.Cells[i + 2, 4].Value2 = dt.Rows[i][2];                           
                        }
                        ws.Cells[1, 5].Value2 = FromPicker.SelectedDate.Value.ToString() + "-" + ToPicker.SelectedDate.Value.ToString();
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
            

        }
    }
}
