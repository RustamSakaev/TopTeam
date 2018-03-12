using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Interaction logic for VisitActionForm.xaml
    /// </summary>
    public partial class VisitActionForm : Window
    {
        private readonly DataTable _currentDataItem;
        private readonly FormState _state;
        private readonly Action _callback;

        public VisitActionForm(Action cb, FormState state, string editId = null)
        {
            InitializeComponent();

            _callback = cb;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderInner.Content = "Редактирование посещения";
                    _currentDataItem = DBVisit.GetVisit(editId);

                    ClientCmbBox.ItemsSource = DBClient.GetClients().DefaultView;
                    ClientCmbBox.DisplayMemberPath = "ФИО";
                    ClientCmbBox.SelectedValuePath = "id";
                    ClientCmbBox.SelectedValue = _currentDataItem.Rows[0]["clientid"].ToString();
                    ClientCmbBox.IsReadOnly = true;

                    WorkerCmbBox.ItemsSource = DBWorker.GetWorkers().DefaultView;
                    WorkerCmbBox.DisplayMemberPath = "ФИО";
                    WorkerCmbBox.SelectedValuePath = "id";
                    WorkerCmbBox.SelectedValue = _currentDataItem.Rows[0]["workerid"].ToString();
                    WorkerCmbBox.IsReadOnly = true;

                    StatusCmbBox.ItemsSource = DBStatus.GetStatuses().DefaultView;
                    StatusCmbBox.DisplayMemberPath = "Статус посещения";
                    StatusCmbBox.SelectedValuePath = "id";
                    StatusCmbBox.SelectedValue = _currentDataItem.Rows[0]["statusid"].ToString();
                    StatusCmbBox.IsReadOnly = true;

                    //Rofl 
                    DateDatePicker.SelectedDate = DateTime.Parse(_currentDataItem.Rows[0]["Дата посещения"].ToString());

                    TimeSBox.Text = _currentDataItem.Rows[0]["Начальное время"].ToString();
                    TimeEBox.Text = _currentDataItem.Rows[0]["Конечное время"].ToString();

                    break;
                case FormState.Add:
                    HeaderInner.Content = "Добавление посещения";
                    ClientCmbBox.ItemsSource = DBClient.GetClients().DefaultView;
                    ClientCmbBox.DisplayMemberPath = "ФИО";
                    ClientCmbBox.SelectedValuePath = "id";

                    WorkerCmbBox.ItemsSource = DBWorker.GetWorkers().DefaultView;
                    WorkerCmbBox.DisplayMemberPath = "ФИО";
                    WorkerCmbBox.SelectedValuePath = "id";

                    StatusCmbBox.ItemsSource = DBStatus.GetStatuses().DefaultView;
                    StatusCmbBox.DisplayMemberPath = "Статус посещения";
                    StatusCmbBox.SelectedValuePath = "id";
                    break;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ClientCmbBox.Validate(true) || !WorkerCmbBox.Validate(true) || !StatusCmbBox.Validate(true) ||
                !DateDatePicker.Validate(true) || !TimeSBox.Validate(true, input => new Regex("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").IsMatch(input)) || 
                !TimeEBox.Validate(true, input => new Regex("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$").IsMatch(input)))
            {
                return;
            }

            switch (_state)
            {
                case FormState.Edit:
                    DBVisit.EditVisit(
                        _currentDataItem.Rows[0]["id"].ToString(),
                        ClientCmbBox.SelectedValue.ToString(),
                        WorkerCmbBox.SelectedValue.ToString(),
                        DateDatePicker.DisplayDate.ToString(),
                        TimeSBox.Text, 
                        TimeEBox.Text,
                        StatusCmbBox.SelectedValue.ToString()
                    );
                    break;
                case FormState.Add:
                    DBVisit.AddVisit(
                        ClientCmbBox.SelectedValue.ToString(),
                        WorkerCmbBox.SelectedValue.ToString(),
                        DateDatePicker.DisplayDate.ToString(),
                        TimeSBox.Text,
                        TimeEBox.Text,
                        StatusCmbBox.SelectedValue.ToString()
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
