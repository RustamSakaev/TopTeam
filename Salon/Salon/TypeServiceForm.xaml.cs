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

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для TypeServiceForm.xaml
    /// </summary>
    public partial class TypeServiceForm : Window
    {
        public TypeServiceForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TypeServiceActionForm typeserv = new TypeServiceActionForm(FormState.Add);
            typeserv.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TypeServiceActionForm typeserv = new TypeServiceActionForm(FormState.Edit);
            typeserv.ShowDialog();
        }

        private void AddButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
