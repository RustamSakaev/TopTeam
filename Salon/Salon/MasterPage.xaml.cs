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
    /// Логика взаимодействия для MasterPage.xaml
    /// </summary>
     public class Phone
{
    public string Title { get; set; }
    public string Company { get; set; }
    public int Price { get; set; }
}

public partial class MasterPage : Window
    {
        public MasterPage()
        {
            InitializeComponent();
            List<Phone> phonesList = new List<Phone>
{
    new Phone { Title="iPhone 6S", Company="Apple", Price=54990 },
    new Phone {Title="Lumia 950", Company="Microsoft", Price=39990 },
    new Phone {Title="Nexus 5X", Company="Google", Price=29990 }
};
            ClientGrid.ItemsSource = phonesList;
        }

        private void FullSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchStack.Visibility = Visibility.Collapsed;
            FullSearchGroup.Visibility = Visibility.Visible;
            Header.Height = 125;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SearchStack.Visibility = Visibility.Visible;
            FullSearchGroup.Visibility = Visibility.Collapsed;
            Header.Height = 75;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditMasterPage add = new AddEditMasterPage();
                add.ShowDialog();
        }
    }
}
