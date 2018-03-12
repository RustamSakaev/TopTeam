using System;
using System.Data;
using System.Windows;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Interaction logic for GiftCardActionForm.xaml
    /// </summary>
    public partial class GiftCardActionForm : Window
    {
        private readonly DataTable _currentDataItem;
        private readonly FormState _state;
        private readonly Action _callback;

        public GiftCardActionForm(Action cb, FormState state, string editId = null)
        {
            InitializeComponent();

            _callback = cb;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderInner.Content = "Редактирование подарочной карты";
                    _currentDataItem = DBGiftCard.GetGiftCard(editId);
                    ClientCmbBox.Items.Add(_currentDataItem.Rows[0]["Клиент"].ToString());
                    ClientCmbBox.SelectedItem = _currentDataItem.Rows[0]["Клиент"].ToString();
                    ClientCmbBox.IsReadOnly = true;

                    WorkerCmbBox.Items.Add(_currentDataItem.Rows[0]["Сотрудник"].ToString());
                    WorkerCmbBox.SelectedItem = _currentDataItem.Rows[0]["Сотрудник"].ToString();
                    WorkerCmbBox.IsReadOnly = true;

                    NumberBox.Text = _currentDataItem.Rows[0]["Номер"].ToString();
                    NominalBox.Text = _currentDataItem.Rows[0]["Номинал"].ToString();
                    GivingDatePicker.SelectedDate = DateTime.Parse(_currentDataItem.Rows[0]["Дата выдачи"].ToString());

                    break;
                case FormState.Add:
                    HeaderInner.Content = "Добавление подарочной карты";
                    ClientCmbBox.ItemsSource = DBClient.GetClients().DefaultView;
                    ClientCmbBox.DisplayMemberPath = "ФИО";
                    ClientCmbBox.SelectedValuePath = "id";

                    WorkerCmbBox.ItemsSource = DBWorker.GetWorkers().DefaultView;
                    WorkerCmbBox.DisplayMemberPath = "ФИО";
                    WorkerCmbBox.SelectedValuePath = "id";
                    break;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NumberBox.Validate(true) || !GivingDatePicker.Validate(true) || !NominalBox.Validate(true) ||
                !ClientCmbBox.Validate(true) || !WorkerCmbBox.Validate(true))
            {
                return;
            }

            switch (_state)
            {
                case FormState.Edit:
                    DBGiftCard.EditGiftCard(
                        _currentDataItem.Rows[0]["id"].ToString(),
                        NumberBox.Text, 
                        GivingDatePicker.DisplayDate.ToString(), 
                        NominalBox.Text,
                        _currentDataItem.Rows[0]["clientid"].ToString(),
                        _currentDataItem.Rows[0]["workerid"].ToString()
                    );
                    break;
                case FormState.Add:
                    DBGiftCard.AddGiftCard(
                        NumberBox.Text,
                        GivingDatePicker.DisplayDate.ToString(),
                        NominalBox.Text,
                        ClientCmbBox.SelectedValue.ToString(),
                        WorkerCmbBox.SelectedValue.ToString()
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
