using System;
using System.Data;
using System.Windows;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для WorkerForm.xaml
    /// </summary>
    public partial class WorkerForm : Window
    {
        private DataTable _currentFormData = new DataTable();
        private readonly FormOpenAs _openAs;
        private readonly Action<string> _callback;
        private readonly Action _onUpdate;

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; WorkersGrid.DataContext = _currentFormData.DefaultView; }
        }

        public WorkerForm(Action<string> cb = null, Action onUpdate = null, FormOpenAs openAs = FormOpenAs.Default)
        {
            InitializeComponent();

            _callback = cb;
            _onUpdate = onUpdate;
            _openAs = openAs;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBWorker.GetWorkers();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm reg = new RegistrationForm();
            reg.ShowDialog();
            reg.Title = "Добавление сотрудника";
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

        private void WorkersGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_openAs == FormOpenAs.Secondary)
            {
                var idx = ((DataView)WorkersGrid.DataContext).Table.Columns.IndexOf("id");
                if (idx == -1) return;

                var id = ((DataRowView)WorkersGrid.SelectedItem)?.Row[idx].ToString();
                if (id == null) return;

                _callback(id);
                this.Close();
            }
        }
    }
}
