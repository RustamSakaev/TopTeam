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
    /// Логика взаимодействия для KindService.xaml
    /// </summary>
    public partial class KindService : Window
    {
        private DataTable _currentFormData = new DataTable();
        public KindService()
        {
            InitializeComponent();
        }
        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; KindServiceGrid.ItemsSource = _currentFormData.DefaultView; }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            KindServiceActionForm kindserv = new KindServiceActionForm(FormState.Add);
            kindserv.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var kindidx = ((DataView)KindServiceGrid.ItemsSource).Table.Columns.IndexOf("id");

            if (kindidx == -1) return;

            var kindid = ((DataRowView)KindServiceGrid.SelectedItem)?.Row[kindidx].ToString();

            if (kindid == null) return;
            KindServiceActionForm kindserv = new KindServiceActionForm(FormState.Edit,kindid);
            kindserv.ShowDialog();
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBKindService.GetKindServices();
            KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}
