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
    /// <summary>
    /// Логика взаимодействия для UserRoleActionForm.xaml
    /// </summary>
    public partial class UserRoleActionForm : Window
    {
        public UserRoleActionForm()
        {
            InitializeComponent();
            CurrentFormData = DBUser.GetUsers();
        }
        private DataTable _currentFormData = new DataTable();
        private string _currentFilter = "";
        private readonly string[] _hiddenFields = { "id" };

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; UserGrid.DataContext = _currentFormData;}
        }

        private string CurrentFilter
        {
            get { return _currentFilter; }
            set { _currentFilter = value; DisplayDataWithFilter(); }
        }

        private void DisplayDataWithFilter()
        {
            var displayData = _currentFormData.DefaultView;
            displayData.RowFilter = $"ФИО LIKE '%{_currentFilter}%'";
            UserGrid.DataContext = displayData;
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentFilter = SearchBox.Text;
        }

        private void UserGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }

        public void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //DataGrid sender = new DataGrid;
            DataGridRow row = sender as DataGridRow;
            DataRowView dataRowView = (DataRowView)row.Item;
            Users main = this.Owner as Users;
            if (main != null)
            {
                main.UserCmbBox.SelectedValue = dataRowView[2];
                this.Close();
            }
        }
    }
}
