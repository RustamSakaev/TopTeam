using System;
using System.Data;
using System.Windows;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Interaction logic for BankCardActionForm.xaml
    /// </summary>
    public partial class BankCardActionForm : Window
    {
        private readonly DataTable _currentDataItem;
        private readonly FormState _state;
        private readonly Action _callback;

        public BankCardActionForm(Action cb, FormState state, string editId = null)
        {
            InitializeComponent();

            _callback = cb;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderInner.Content = "Редактирование банковской карты";
                    _currentDataItem = DBBankCard.GetBankCard(editId);
                    ClientCmbBox.Items.Add(_currentDataItem.Rows[0]["ФИО"].ToString());
                    ClientCmbBox.SelectedItem = _currentDataItem.Rows[0]["ФИО"].ToString();
                    ClientCmbBox.IsReadOnly = true;
                    NumberBox.Text = _currentDataItem.Rows[0]["Номер"].ToString();
                    break;
                case FormState.Add:
                    HeaderInner.Content = "Добавление банковской карты";
                    ClientCmbBox.ItemsSource = DBClient.GetClients().DefaultView;
                    ClientCmbBox.DisplayMemberPath = "ФИО";
                    ClientCmbBox.SelectedValuePath = "id";
                    break;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ClientCmbBox.Validate(true) || !NumberBox.Validate(true)) return;

            switch (_state)
            {
                case FormState.Edit:
                    DBBankCard.EditBankCard(
                        _currentDataItem.Rows[0]["id"].ToString(),
                        NumberBox.Text
                    );
                    break;
                case FormState.Add:
                    DBBankCard.AddBankCard(
                        ClientCmbBox.SelectedValue.ToString(),
                        NumberBox.Text
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
