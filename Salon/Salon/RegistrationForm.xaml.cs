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
using System.Text.RegularExpressions;

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
            Regex exp = new Regex(@"^\d{0,2}(\.[0,5])?$");
            Regex FSL = new Regex(@"^[А-Я][a-я]+$");
            if (!SurnameBox.Validate(true) || !NameBox.Validate(true) || !PatronymicBox.Validate(true) ||
                !DBirthDatePicker.Validate(true) || !ExpBox.Validate(true) || !GenderCmbBox.Validate(true) ||
                !LoginBox.Validate(true) || PassBox.Password == "" || !LoginBox.Validate(true) || !exp.IsMatch(ExpBox.Text)
                || !FSL.IsMatch(NameBox.Text) || !FSL.IsMatch(SurnameBox.Text) || !FSL.IsMatch(PatronymicBox.Text))
                MessageBox.Show("Заполните правильно все поля!");
            //return;
            else
            {
                bool add = DBUser.CreateUser(LoginBox.Text, PassBox.Password, ((ComboBoxItem)RoleCmbBox.SelectedValue).Content.ToString() == "Мастер" ? "master" : "admin");
                if (add)
                {
                    DBWorker.AddWorker(SurnameBox.Text.Trim(),
                        NameBox.Text.Trim(),
                        PatronymicBox.Text.Trim(),
                        DBirthDatePicker.DisplayDate.ToString(),
                        LoginBox.Text.Trim(),
                        ((ComboBoxItem)GenderCmbBox.SelectedValue).Content.ToString() == "Мужской" ? "1" : "0",
                        ExpBox.Text.Trim());
                    MessageBox.Show("Пользователь успешно создан!");
                    DBCore.Destroy();
                    this.Close();
                }
            }         
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DBCore.Destroy();
            this.Close();
        }
    }
}
