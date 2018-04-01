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
using System.Data.SqlClient;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для Kabinet.xaml
    /// </summary>
    public partial class Kabinet : Window
    {
        public Kabinet()
        {
            InitializeComponent();
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (OldPassBox.Password == "" || NewPassBox.Password == "" || ConfirmPassBox.Password == "")
                MessageBox.Show("Заполните все поля!");
            {
                //должна быть проверка на текущий пароль
                if (OldPassBox.Password != "")
                {
                    MessageBox.Show("Текущий пароль неверен!");
                    OldPassBox.Clear();
                    NewPassBox.Clear();
                    ConfirmPassBox.Clear();
                }
                else
                {
                    if (NewPassBox.Password == OldPassBox.Password)
                    {
                        MessageBox.Show("Новый и старый пароли не должны совпадать!");
                        NewPassBox.Clear();
                        ConfirmPassBox.Clear();
                    }
                    else
                    {
                        if (NewPassBox.Password != ConfirmPassBox.Password)
                        {
                            MessageBox.Show("Введенные пароли не совпадают!");
                            NewPassBox.Clear();
                            ConfirmPassBox.Clear();
                        }
                        else
                        {
                            DBUser.ChangePass(userName, NewPassBox.Password, OldPassBox.Password);
                            MessageBox.Show("Пароль успешно изменен!");
                            this.Close();
                        }
                    }
                }
            }         
        }
    }
}
