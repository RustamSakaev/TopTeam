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
    /// Логика взаимодействия для WorkerForm.xaml
    /// </summary>
    public partial class WorkerForm : Window
    {
        private DataTable _currentFormData = new DataTable();

        private DataTable CurrentFormData
        {
            get => _currentFormData;
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
    }
}
