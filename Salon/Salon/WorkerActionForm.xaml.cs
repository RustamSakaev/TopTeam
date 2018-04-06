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
    /// Interaction logic for WorkerActionForm.xaml
    /// </summary>
    public partial class WorkerActionForm : Window
    {
        string _editId;
        WorkerForm _wrk;
        public WorkerActionForm(string secondName, string name, string otche, string birthday, string staj, string gender, string about, WorkerForm wrk, string editId = null)
        {
            InitializeComponent();
            _editId = editId;
            _wrk = wrk;
            SurnameBox.Text = secondName;
            NameBox.Text = name;
            PatronymicBox.Text = otche;
            DBirthDatePicker.SelectedDate = Convert.ToDateTime(birthday);
            ExpBox.Text = staj;
            About.Text = about;
            if (gender == "Мужской")
            {
                GenderCmbBox.SelectedIndex = 1;
            }
            else
            {
                GenderCmbBox.SelectedIndex = 0;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            int Gender = 0;
            if (GenderCmbBox.SelectedIndex == 1)
            {
                Gender = 0;
            }
            else
            {
                Gender = 1;
            }
            DBCore.ExecuteSql("UPDATE Worker SET Name = '" + NameBox.Text + "', Surname = '" + SurnameBox.Text + "', Patronymic = '" + PatronymicBox.Text + "', DBirth = '" + DBirthDatePicker.SelectedDate + "', Gender = " + Gender + ", Exp = " + ExpBox.Text.Replace(',', '.') + ", About = '" + About.Text + "' where ID_Worker = '" + _editId + "';");
            _wrk.UpdateDgv();
            this.Close();
        }
        //ТТААААААК КРЧ ПРИ ДОБАВЛЕНИИ МОЖЕТЕ ИСПОЛЬЗОВАТЬ ФОРМУ RegistrationForm.xaml 
    }
}
