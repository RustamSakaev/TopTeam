using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Interaction logic for PaymentMethodActionForm.xaml
    /// </summary>
    public partial class PaymentMethodActionForm : Window
    {
        private readonly DataTable _currentDataItem;
        private readonly FormState _state;
        private readonly Action _callback;

        public PaymentMethodActionForm(Action cb, FormState state, string editId = null)
        {
            InitializeComponent();

            _callback = cb;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderInner.Content = "Редактирование способа оплаты";
                    _currentDataItem = DBPaymentMethod.GetPaymentMethod(editId);
                    NameBox.Text = _currentDataItem.Rows[0]["Способ оплаты"].ToString();
                    break;
                case FormState.Add:
                    HeaderInner.Content = "Добавление способа оплаты";
                    break;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NameBox.Validate(true)) return;

            switch (_state)
            {
                case FormState.Edit:
                    DBPaymentMethod.EditPaymentMethod(_currentDataItem.Rows[0]["id"].ToString(), NameBox.Text);
                    break;
                case FormState.Add:
                    DBPaymentMethod.AddPaymentMethod(NameBox.Text);
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
