using System;
using System.Collections.Generic;
using System.Data;
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

namespace Salon
{
    public class Schedule
    {
        private string Worker_ID;
        private string Date;
        private string TStart;
        private string TEnd;
        private string Busy;

        public Schedule(string _worker_ID, string _date, string _tStart, string _tEnd, string _busy = "0")
        {
            Worker_ID = _worker_ID;
            Date = _date;
            TStart = _tStart;
            TEnd = _tEnd;
            Busy = _busy;
        }

        public string GetValuePartForSql()
        {
            

            string sql = "(";

            sql += Worker_ID + ", ";
            sql += "'" + Date + "', ";
            sql += "'" + TStart + "', ";
            sql += "'" + TEnd + "', ";
            sql += "0";

            sql += ")";
            return sql;
        }
    }
    /// <summary>
    /// Interaction logic for ScheduleForm.xaml
    /// </summary>
    public partial class ScheduleForm : Window
    {
        public ScheduleForm()
        {
            InitializeComponent();
        }
        DataTable _workers;
        DataTable _typesMasters;

        private DataTable _currentFormData = new DataTable();

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; ScheduleGrid.DataContext = _currentFormData.DefaultView; }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            dateFromTable.SelectedDate = DateTime.Now.Date;
            CurrentFormData = DBWorker.GetSchedule(dateFromTable.SelectedDate.ToString());
            _workers = DBWorker.GetWorkers();
            _typesMasters = DBWorker.GetTypesOfMasters();
            workerCmbBox.DataContext = _workers.DefaultView;
            typeMasterCmbBox.DataContext = _typesMasters.DefaultView;
            if (_workers.Rows.Count > 0)
            {
                workerCmbBox.SelectedIndex = 0;
                CurrentFormData = DBWorker.GetSchedule(dateFromTable.SelectedDate.ToString(), Convert.ToInt32(workerCmbBox.SelectedValue));
            }
            if (_typesMasters.Rows.Count > 0)
            {
                typeMasterCmbBox.SelectedIndex = 0;
            }
            fromDateDatePicker.SelectedDate = DateTime.Now.Date;
            beforeDateDatePicker.SelectedDate = DateTime.Now.Date;
        }

        private void ComboBox_LayoutUpdated(object sender, EventArgs e)
        {

        }

        private void workerChkBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentFormData = DBWorker.GetSchedule(dateFromTable.SelectedDate.ToString(), Convert.ToInt32(workerCmbBox.SelectedValue));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /////////////////////////////////////////////ЕЩЁ НЕ ДОДЕЛАЛ, НО УЖЕ КАЙФОВО


            List<Schedule> scheduleList = new List<Schedule>();

            TimeSpan dateSpan = (TimeSpan)(beforeDateDatePicker.SelectedDate - fromDateDatePicker.SelectedDate);

            TimeSpan timeFrom = new TimeSpan(Convert.ToInt32(sHTB.Text), Convert.ToInt32(sMTB.Text), 0);
            TimeSpan timeBefore = new TimeSpan(Convert.ToInt32(dHTB.Text), Convert.ToInt32(dMTB.Text), 0);

            TimeSpan timeSpan = timeBefore - timeFrom;

            int sectionsCount = 0;

            if (timeSpan.TotalMinutes % 5 == 0) { sectionsCount = Convert.ToInt32(timeSpan.TotalMinutes / 15); }
            else { MessageBox.Show("Промежуток времени должен быть кратен 15"); return; }



            Console.WriteLine("Days: " + dateSpan.TotalDays);
            Console.WriteLine("Time from: " + timeFrom);
            Console.WriteLine("Time before: " + timeBefore);
            Console.WriteLine("Minutes: " + timeSpan.TotalMinutes);
            Console.WriteLine("sectionsCount: " + sectionsCount);



            for (int i = 0; i <= dateSpan.Days; i++)
            {
                
                Console.WriteLine("___NEW DAY___ " + Convert.ToDateTime(fromDateDatePicker.SelectedDate).AddDays(i));
                
                 
                for (int z = 0; z < sectionsCount; z++)
                {
                    scheduleList.Add(
                        new Schedule(
                            workerCmbBox.SelectedValue.ToString(),
                            Convert.ToDateTime(fromDateDatePicker.SelectedDate).AddDays(i).ToShortDateString().ToString(),
                            Convert.ToDateTime(fromDateDatePicker.SelectedDate).AddDays(i).AddHours(Convert.ToInt32(sHTB.Text) + Convert.ToInt32(z / 4)).AddMinutes(Convert.ToInt32(sMTB.Text) + Convert.ToInt32(z % 4) * 15).ToShortTimeString(),
                            Convert.ToDateTime(fromDateDatePicker.SelectedDate).AddDays(i).AddHours(Convert.ToInt32(sHTB.Text) + Convert.ToInt32((z + 1) / 4)).AddMinutes(Convert.ToInt32(sMTB.Text) + Convert.ToInt32((z + 1) % 4) * 15).AddSeconds(-1).ToShortTimeString()));
                    
                }
            }

            //DBWorker.AddScedule(
            //    workerCmbBox.SelectedValue.ToString(),
            //    datePicker.SelectedDate.ToString(),
            //    "" + sHTB.Text + ":" + sMTB.Text + ":00",
            //    "" + dHTB.Text + ":" + dMTB.Text + ":00"
            //    );
            //CurrentFormData = DBWorker.GetSchedule();

            string sqlQuery = @"insert into Schedule(Worker_ID, Date, TStart, TEnd, Busy) values";
            for (int i = 0; i < scheduleList.Count; i++)
            {
                if (i != scheduleList.Count - 1)
                {
                    sqlQuery += scheduleList[i].GetValuePartForSql() + ",";
                }
                else
                {
                    sqlQuery += scheduleList[i].GetValuePartForSql();
                }
            }
            DBCore.ExecuteSql(sqlQuery);
            LoadData();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dateFromTable_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void dateFromTable_CalendarClosed(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBWorker.GetSchedule(dateFromTable.SelectedDate.ToString(), Convert.ToInt32(workerCmbBox.SelectedValue));
        }

        private void sHTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
