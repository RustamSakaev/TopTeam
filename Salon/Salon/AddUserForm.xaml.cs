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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для AddUserForm.xaml
    /// </summary>
    public partial class AddUserForm : Window
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SurnameBox.Validate(true) || !NameBox.Validate(true) || !PatronymicBox.Validate(true) ||
                !DBirthDatePicker.Validate(true) || !ExpBox.Validate(true) || !GenderCmbBox.Validate(true) ||
                !LoginBox.Validate(true) || !PassBox.Validate(true))
                return;
            else
            {
                DBUser.CreateUser(LoginBox.Text, PassBox.Text);
                DBWorker.AddWorker(SurnameBox.Text, 
                    NameBox.Text, 
                    PatronymicBox.Text, 
                    DBirthDatePicker.DisplayDate.ToString(), 
                    LoginBox.Text, 
                    ((ComboBoxItem)GenderCmbBox.SelectedValue).Content.ToString() == "Мужской" ? "1" : "0", 
                    ExpBox.Text);
                MessageBox.Show("Пользователь создан");
                this.Close();
            }         
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
