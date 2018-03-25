using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для StatusForm.xaml
    /// </summary>
    public partial class StatusForm : Window
    {
        private readonly string[] _hiddenFields = { "id" };
        private DataTable _currentFormData = new DataTable();
        private readonly FormOpenAs _openAs;
        private readonly Action<string> _callback;
        private readonly Action _onUpdate;

        private DataTable CurrentFormData
        {
            get { return _currentFormData; }
            set { _currentFormData = value; StatusGrid.DataContext = _currentFormData; }
        }

        public StatusForm(Action<string> cb = null, Action onUpdate = null, FormOpenAs openAs = FormOpenAs.Default)
        {
            InitializeComponent();

            _callback = cb;
            _onUpdate = onUpdate;
            _openAs = openAs;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentFormData = DBStatus.GetStatuses();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new StatusActionForm(() =>
            {
                CurrentFormData = DBStatus.GetStatuses();
                _onUpdate();
            }, FormState.Add);
            form.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataTable)StatusGrid.DataContext).Columns.IndexOf("id");
            if (idx == -1) return;

            var id = ((DataRowView)StatusGrid.SelectedItem)?.Row[idx].ToString();
            if (id == null) return;

            var form = new StatusActionForm(() =>
            {
                CurrentFormData = DBStatus.GetStatuses();
                _onUpdate();
            }, FormState.Edit, id);
            form.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var idx = ((DataTable)StatusGrid.DataContext).Columns.IndexOf("id");

            foreach (DataRowView selectedItem in StatusGrid.SelectedItems)
            {
                DBStatus.DeleteStatus(selectedItem.Row[idx].ToString());
            }

            CurrentFormData = DBStatus.GetStatuses();
            _onUpdate();
        }

        private void StatusGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (!_hiddenFields?.Contains(e.PropertyName) == true) return;

            e.Cancel = true;
        }

        private void StatusGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_openAs == FormOpenAs.Secondary)
            {
                var idx = ((DataTable)StatusGrid.DataContext).Columns.IndexOf("id");
                if (idx == -1) return;

                var id = ((DataRowView)StatusGrid.SelectedItem)?.Row[idx].ToString();
                if (id == null) return;

                _callback(id);
                this.Close();
            }
        }
    }
}
