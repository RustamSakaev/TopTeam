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
    /// Логика взаимодействия для TypeServiceForm.xaml
    /// </summary>
    public partial class TypeServiceForm : Window
    {
        private DataTable _currentFormData = new DataTable();
        public TypeServiceForm()
        {
            InitializeComponent();
        }
        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; TypeServiceGrid.ItemsSource = _currentFormData.DefaultView; }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TypeServiceActionForm typeserv = new TypeServiceActionForm(FormState.Add);
            typeserv.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var typeidx = ((DataView)TypeServiceGrid.ItemsSource).Table.Columns.IndexOf("id");

            if (typeidx == -1) return;

            var typeid = ((DataRowView)TypeServiceGrid.SelectedItem)?.Row[typeidx].ToString();

            if (typeid == null) return;

            var groupidx = ((DataView)TypeServiceGrid.ItemsSource).Table.Columns.IndexOf("group_id");

            if (groupidx == -1) return;

            var groupid = ((DataRowView)TypeServiceGrid.SelectedItem)?.Row[groupidx].ToString();

            if (groupid == null) return;


            TypeServiceActionForm typeserv = new TypeServiceActionForm(FormState.Edit,typeid, groupid);
            typeserv.ShowDialog();
        }

        
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBTypeService.GetTypeServices();
            TypeServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            TypeServiceGrid.Columns[3].Visibility = Visibility.Hidden;
            foreach (DataRow type in DBKindService.GetKindServices().Rows)
            {
                KindServiceCmbBox.Items.Add(type["Наименование"]);
            }

            foreach (DataRow kind in DBGroupService.GetGroupServices().Rows)
            {
                GroupServiceCmbBox.Items.Add(kind["Наименование"]);
            }
        }

    }
}
