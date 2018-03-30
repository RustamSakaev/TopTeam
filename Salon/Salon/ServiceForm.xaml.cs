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
    /// Логика взаимодействия для ServiceForm.xaml
    /// </summary>
    public partial class ServiceForm : Window
    {
        private DataTable _currentFormData = new DataTable();
        private readonly Action<string> _callback;
        private readonly FormOpenAs _openAs;
        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; ServiceGrid.ItemsSource = _currentFormData.DefaultView; }
        }
        public ServiceForm(Action<string> cb = null, FormOpenAs openAs = FormOpenAs.Default)
        {
            InitializeComponent();
            _callback = cb;
            _openAs = openAs;
        }
        public void OnLoad( object sender, RoutedEventArgs e)
        { 
            CurrentFormData = DBService.GetServices();
            ServiceGrid.Columns[5].Visibility = Visibility.Hidden;
            ServiceGrid.Columns[6].Visibility = Visibility.Hidden;
            ServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            foreach (DataRow type in DBTypeService.GetTypeServices().Rows)
            {
                TypeServiceCmbBox.Items.Add(type["Наименование"]);
            }
            foreach (DataRow kind in DBKindService.GetKindServices().Rows)
            {
                KindServiceCmbBox.Items.Add(kind["Наименование"]);
            }
        }
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new ServiceActionForm(() =>
            {
               CurrentFormData = DBService.GetServices();
            }, FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var servidx = ((DataView)ServiceGrid.ItemsSource).Table.Columns.IndexOf("id");

            if (servidx == -1) return;

            var servid = ((DataRowView)ServiceGrid.SelectedItem)?.Row[servidx].ToString();

            if (servid == null) return;

            var typeidx = ((DataView)ServiceGrid.ItemsSource).Table.Columns.IndexOf("type_id");

            if (typeidx == -1) return;

            var typeid = ((DataRowView)ServiceGrid.SelectedItem)?.Row[typeidx].ToString();

            if (typeid == null) return;

            var kindidx = ((DataView)ServiceGrid.ItemsSource).Table.Columns.IndexOf("kind_id");

            if (kindidx == -1) return;

            var kindid = ((DataRowView)ServiceGrid.SelectedItem)?.Row[kindidx].ToString();

            if (kindid == null) return;

            var form = new ServiceActionForm(() =>
            {
                CurrentFormData = DBService.GetServices();
            }, FormState.Edit, servid, typeid, kindid);
            form.ShowDialog();

           
        }

      
    }
}
