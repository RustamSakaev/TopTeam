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
        private readonly DataTable _currentItemData;
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
                    this.Title = "Редактирование способа оплаты";
                    _currentItemData = DBPaymentMethod.GetPaymentMethod(editId);
                    NameBox.Text = _currentItemData.Rows[0]["Способ оплаты"].ToString();
                    break;
                case FormState.Add:
                    this.Title = "Добавление способа оплаты";
                    break;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Validate(true))
            {
                switch (_state)
                {
                    case FormState.Edit:
                        DBPaymentMethod.EditPaymentMethod(_currentItemData.Rows[0]["id"].ToString(), NameBox.Text);
                        break;
                    case FormState.Add:
                        DBPaymentMethod.AddPaymentMethod(NameBox.Text);
                        break;
                }    
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
