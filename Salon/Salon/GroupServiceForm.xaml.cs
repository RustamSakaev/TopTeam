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
using Salon.Database;
using System.Data;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для GroupServiceForm.xaml
    /// </summary>
    public partial class GroupServiceForm : Window
    {
        private DataTable _currentFormData = new DataTable();
        public GroupServiceForm()
        {
            InitializeComponent();
        }
        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; GroupServiceGrid.ItemsSource = _currentFormData.DefaultView; }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            GroupServiceActionForm grserv = new GroupServiceActionForm(FormState.Add);
            grserv.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var groupidx = ((DataView)GroupServiceGrid.ItemsSource).Table.Columns.IndexOf("id");

            if (groupidx == -1) return;

            var groupid = ((DataRowView)GroupServiceGrid.SelectedItem)?.Row[groupidx].ToString();

            if (groupid == null) return;
            GroupServiceActionForm grserv = new GroupServiceActionForm(FormState.Edit, groupid);
            grserv.ShowDialog();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBGroupService.GetGroupServices();
            GroupServiceGrid.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
