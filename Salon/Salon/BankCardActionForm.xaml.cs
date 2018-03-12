using System;
using System.Data;
using System.Windows;

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
                    this.Title = "Редактирование банковской карты";
                    _currentDataItem = DBPaymentMethod.GetPaymentMethod(editId);
                    NameBox.Text = _currentDataItem.Rows[0]["Способ оплаты"].ToString();
                    break;
                case FormState.Add:
                    this.Title = "Добавление банковской карты";
                    break;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
