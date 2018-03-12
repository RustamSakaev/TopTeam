using System;
using System.Data;
using System.Windows;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Interaction logic for StatusActionForm.xaml
    /// </summary>
    public partial class StatusActionForm : Window
    {
        private readonly DataTable _currentDataItem;
        private readonly FormState _state;
        private readonly Action _callback;

        public StatusActionForm(Action cb, FormState state, string editId = null)
        {
            InitializeComponent();

            _callback = cb;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderInner.Content = "Редактирование статуса посещения";
                    _currentDataItem = DBStatus.GetStatus(editId);
                    NameBox.Text = _currentDataItem.Rows[0]["Статус посещения"].ToString();
                    break;
                case FormState.Add:
                    HeaderInner.Content = "Добавление статуса посещения";
                    break;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NameBox.Validate(true)) return;

            switch (_state)
            {
                case FormState.Edit:
                    DBStatus.EditStatus(_currentDataItem.Rows[0]["id"].ToString(), NameBox.Text);
                    break;
                case FormState.Add:
                    DBStatus.AddStatus(NameBox.Text);
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
