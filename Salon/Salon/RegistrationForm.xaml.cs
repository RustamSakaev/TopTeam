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
    public partial class RegistrationForm : Window
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SurnameBox.Validate(true) || !NameBox.Validate(true) || !PatronymicBox.Validate(true) ||
                !DBirthDatePicker.Validate(true) || !ExpBox.Validate(true) || !GenderCmbBox.Validate(true) ||
                !LoginBox.Validate(true))
                return;
            else
            {
                // должна быть проверка на пользователя с таким же логином

                DBUser.CreateUser(LoginBox.Text, PassBox.Password);
                DBWorker.AddWorker(SurnameBox.Text, 
                    NameBox.Text, 
                    PatronymicBox.Text, 
                    DBirthDatePicker.DisplayDate.ToString(), 
                    LoginBox.Text, 
                    ((ComboBoxItem)GenderCmbBox.SelectedValue).Content.ToString() == "Мужской" ? "1" : "0", 
                    ExpBox.Text);
                MessageBox.Show("Пользователь успешно создан!");
                this.Close();
            }         
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
