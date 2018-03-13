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
using Excel = Microsoft.Office.Interop.Excel;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для WorkerForm.xaml
    /// </summary>
    public partial class WorkerForm : Window
    {
        private DataTable _currentFormData = new DataTable();

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; WorkersGrid.DataContext = _currentFormData.DefaultView; }
        }
        public WorkerForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBWorker.GetWorkers();



        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
        }


        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentFormData == null || _currentFormData.Columns.Count == 0)
            { MessageBox.Show("База пустая");
                return;
            }

            Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
            for (var i = 0; i < _currentFormData.Columns.Count; i++)
            {
                ObjWorkSheet.Cells[1, i + 1] = _currentFormData.Columns[i].ColumnName;
            }
            for (var i = 0; i < _currentFormData.Rows.Count; i++)
            {
                
                for (var j = 0; j < _currentFormData.Columns.Count; j++)
                {
                    ObjWorkSheet.Cells[i + 2, j + 1] = _currentFormData.Rows[i][j];
                }
            }
            ObjExcel.Visible = true;
            ObjExcel.UserControl = true;
        }
    }
}
