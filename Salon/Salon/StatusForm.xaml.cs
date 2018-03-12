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
    /// Логика взаимодействия для StatusForm.xaml
    /// </summary>
    public partial class StatusForm : Window
    {
        private readonly string[] _hiddenFields = { "id" };
        private DataTable _currentFormData = new DataTable();

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; StatusGrid.DataContext = _currentFormData; }
        }

        public StatusForm()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBStatus.GetStatuses();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new StatusActionForm(new Action(() => { CurrentFormData = DBStatus.GetStatuses(); }), FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataTable)StatusGrid.DataContext).Columns.IndexOf("id");
            var id = ((DataRowView)StatusGrid.SelectedItem)?.Row[idx].ToString();

            if (id is null) return;

            var form = new StatusActionForm(new Action(() => { CurrentFormData = DBStatus.GetStatuses(); ; }), FormState.Edit, id);
            form.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataTable)StatusGrid.DataContext).Columns.IndexOf("id");

            foreach (DataRowView selectedItem in StatusGrid.SelectedItems)
            {
                DBStatus.DeleteStatus(selectedItem.Row[idx].ToString());
            }

            CurrentFormData = DBStatus.GetStatuses();
        }

        private void StatusGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }
    }
}
