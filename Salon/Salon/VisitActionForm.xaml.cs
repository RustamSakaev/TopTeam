using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
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

                    UpdateClients();
                    UpdateWorkers();
                    UpdateStatuses();

                    ClientCmbBox.SelectedValue = _currentDataItem.Rows[0]["clientid"].ToString();
                    ClientCmbBox.IsReadOnly = true;

                    WorkerCmbBox.SelectedValue = _currentDataItem.Rows[0]["workerid"].ToString();
                    WorkerCmbBox.IsReadOnly = true;

                    StatusCmbBox.SelectedValue = _currentDataItem.Rows[0]["statusid"].ToString();
                    StatusCmbBox.IsReadOnly = true;

                    //Rofl 
                    DateDatePicker.SelectedDate = DateTime.Parse(_currentDataItem.Rows[0]["Дата посещения"].ToString());

                    TimeSBox.Text = _currentDataItem.Rows[0]["Начальное время"].ToString();
                    TimeEBox.Text = _currentDataItem.Rows[0]["Конечное время"].ToString();

                    break;
                case FormState.Add:
                    HeaderInner.Content = "Добавление посещения";
                    UpdateClients();
                    UpdateWorkers();
                    UpdateStatuses();
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


        private void StatusForm_Click(object sender, RoutedEventArgs e)
        {
            var status = new StatusForm(SetStatus, UpdateStatuses, FormOpenAs.Secondary);
            status.ShowDialog();
        }

        private void SetStatus(string id)
        {
            StatusCmbBox.SelectedValue = id;
        }

        private void UpdateStatuses()
        {
            var selectedStatus = StatusCmbBox.SelectedValue;

            StatusCmbBox.ItemsSource = DBStatus.GetStatuses().DefaultView;
            StatusCmbBox.DisplayMemberPath = "Статус посещения";
            StatusCmbBox.SelectedValuePath = "id";

            if (selectedStatus == null) return;

            try
            {
                StatusCmbBox.SelectedValue = selectedStatus;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void WorkerForm_Click(object sender, RoutedEventArgs e)
        {
            var worker = new WorkerForm(SetWorker, UpdateWorkers, FormOpenAs.Secondary);
            worker.ShowDialog();
        }

        private void SetWorker(string id)
        {
            WorkerCmbBox.SelectedValue = id;
        }

        private void UpdateWorkers()
        {
            var selectedWorker = WorkerCmbBox.SelectedValue;

            WorkerCmbBox.ItemsSource = DBWorker.GetWorkers().DefaultView;
            WorkerCmbBox.DisplayMemberPath = "ФИО";
            WorkerCmbBox.SelectedValuePath = "id";

            if (selectedWorker == null) return;

            try
            {
                WorkerCmbBox.SelectedValue = selectedWorker;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ClientForm_Click(object sender, RoutedEventArgs e)
        {
            var client = new ClientForm(SetClient, UpdateClients, FormOpenAs.Secondary);
            client.ShowDialog();
        }

        private void SetClient(string id)
        {
            ClientCmbBox.SelectedValue = id;
        }

        private void UpdateClients()
        {
            var selectedClient = ClientCmbBox.SelectedValue;

            ClientCmbBox.ItemsSource = DBClient.GetClients().DefaultView;
            ClientCmbBox.DisplayMemberPath = "ФИО";
            ClientCmbBox.SelectedValuePath = "id";

            if (selectedClient == null) return;

            try
            {
                ClientCmbBox.SelectedValue = selectedClient;
            }
            catch (Exception)
            {
                // ignored
            }
        }

    }
}
