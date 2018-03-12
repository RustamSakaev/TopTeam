using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Interaction logic for ClientActionForm.xaml
    /// </summary>
    public partial class ClientActionForm : Window
    {
        private readonly DataTable _currentDataItem;
        private readonly FormState _state;
        private readonly Action _callback;

        public ClientActionForm(Action cb, FormState state, string editId = null)
        {
            InitializeComponent();

            _callback = cb;
            _state = state;

            switch (_state)
            {
                case FormState.Edit:
                    HeaderInner.Content = "Редактирование клиента";
                    _currentDataItem = DBClient.GetClient(editId);
                    SurnameBox.Text = _currentDataItem.Rows[0]["Фамилия"].ToString();
                    NameBox.Text = _currentDataItem.Rows[0]["Имя"].ToString();
                    PatronymicBox.Text = _currentDataItem.Rows[0]["Отчество"].ToString();
                    DBirthDatePicker.SelectedDate = DateTime.Parse(_currentDataItem.Rows[0]["Дата рождения"].ToString());
                    PhoneBox.Text = _currentDataItem.Rows[0]["Телефон"].ToString();
                    GenderCmbBox.SelectedIndex = _currentDataItem.Rows[0]["Пол"].ToString() == "Мужской" ? 0 : 1;
                    DiscountBox.Text = _currentDataItem.Rows[0]["Скидка"].ToString();
                    break;
                case FormState.Add:
                    HeaderInner.Content = "Добавление клиента";
                    break;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!SurnameBox.Validate(true) || !NameBox.Validate(true) || !PatronymicBox.Validate(true) ||
                !DBirthDatePicker.Validate(true) || !PhoneBox.Validate(true) || !GenderCmbBox.Validate(true) ||
                !DiscountBox.Validate(false))
            {
                return;
            }

            switch (_state)
            {
                case FormState.Edit:
                    DBClient.EditClient(
                        _currentDataItem.Rows[0]["id"].ToString(),
                        SurnameBox.Text,
                        NameBox.Text,
                        PatronymicBox.Text,
                        DBirthDatePicker.DisplayDate.ToString(),
                        PhoneBox.Text,
                        ((ComboBoxItem) GenderCmbBox.SelectedValue).Content.ToString() == "Мужской" ? "1" : "0",
                        DiscountBox.Text
                    );
                    break;
                case FormState.Add:
                    DBClient.AddClient(
                        SurnameBox.Text,
                        NameBox.Text,
                        PatronymicBox.Text,
                        DBirthDatePicker.DisplayDate.ToString(),
                        PhoneBox.Text,
                        ((ComboBoxItem) GenderCmbBox.SelectedValue).Content.ToString() == "Мужской" ? "1" : "0",
                        DiscountBox.Text
                    );
                    break;
            }

            _callback();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
