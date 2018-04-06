using System;
using System.Data;
using System.Windows;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для AddTypeMaster.xaml
    /// </summary>
    public partial class AddEditTypeMaster : Window
    {
        private readonly DataTable _currentDataItem;
        private readonly FormState _state;
        private readonly Action _callback;
        public AddEditTypeMaster(Action cb, FormState state, string editId = null)
        {
            InitializeComponent();
            _callback = cb;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderInner.Content = "Редактирование типа мастера";
                    _currentDataItem = Salon.Database.DBTypeMaster.GetTypeMaster(editId);
                    NameBox.Text = _currentDataItem.Rows[0]["Наименование"].ToString();
                    break;
                case FormState.Add:
                    HeaderInner.Content = "Добавление типа мастера";
                    break;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NameBox.Validate(true)) return;

            switch (_state)
            {
                case FormState.Edit:
                    Salon.Database.DBTypeMaster.EditTypeMaster(_currentDataItem.Rows[0]["id"].ToString(), NameBox.Text);
                    break;
                case FormState.Add:
                    Salon.Database.DBTypeMaster.AddTypeService(NameBox.Text);
                    break;
            }

            _callback();
            this.Close();
        }
    }
}
