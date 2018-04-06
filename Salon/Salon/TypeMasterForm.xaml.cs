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
using System.Data;
using System.Data.SqlClient;
using Salon.Misc;
using Salon.Database;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для TypeMasterForm.xaml
    /// </summary>
    public partial class TypeMasterForm : Window
    {
        private DataTable currentData = new DataTable();
        private readonly List<Filter> Filter = new List<Filter>();
        private readonly Action<string> Back;
        public TypeMasterForm(Action<string> b = null)
        {
            InitializeComponent();
            DBCore.Init(@"ALISKINSSON\SQLEXPRESS01");
            Back = b;
        }
        private DataTable CurrentData
        {
            get { return currentData; }
            set
            {
                currentData = value;
                TypeMasterGrid.ItemsSource = currentData.DefaultView;
                TypeMasterGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new AddEditTypeMaster(() =>
            {
                CurrentData = DBStatus.GetStatuses();
            }, FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataTable)TypeMasterGrid.DataContext).Columns.IndexOf("id");
            if (idx == -1) return;

            var id = ((DataRowView)TypeMasterGrid.SelectedItem)?.Row[idx].ToString();
            if (id == null) return;

            var form = new StatusActionForm(() =>
            {
                CurrentData = DBStatus.GetStatuses();
            }, FormState.Edit, id);
            form.ShowDialog();
        }

        private void Onload(object sender, RoutedEventArgs e)
        {
            CurrentData = DBTypeMaster.GetTypeMasters();
            TypeMasterGrid.Columns[0].Visibility = Visibility.Hidden;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataTable)TypeMasterGrid.DataContext).Columns.IndexOf("id");

            foreach (DataRowView selectedItem in TypeMasterGrid.SelectedItems)
            {
                DBTypeMaster.DeleteTypeMaster(selectedItem.Row[idx].ToString());
            }

            CurrentData = DBTypeMaster.GetTypeMasters();
            //_onUpdate();
        }
    }
}
