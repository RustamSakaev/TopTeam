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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _workers = DBWorker.GetWorkers();
            _typesMasters = DBWorker.GetTypesOfMasters();
            workerCmbBox.DataContext = _workers.DefaultView;
            typeMasterCmbBox.DataContext = _typesMasters.DefaultView;
            if (_workers.Rows.Count > 0)
            {
                workerCmbBox.SelectedIndex = 0;
            }
            if (_typesMasters.Rows.Count > 0)
            {
                typeMasterCmbBox.SelectedIndex = 0;
            }
        }

        private void ComboBox_LayoutUpdated(object sender, EventArgs e)
        {

        }

        private void workerChkBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
