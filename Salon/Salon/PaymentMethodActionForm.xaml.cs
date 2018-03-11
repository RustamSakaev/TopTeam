using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Interaction logic for PaymentMethodActionForm.xaml
    /// </summary>
    public partial class PaymentMethodActionForm : Window
    {
        private Action callback;

        public PaymentMethodActionForm(Action cb, FormState state)
        {
            InitializeComponent();

            callback = cb;

            switch (state)
            {
                case FormState.Edit:
                    this.Title = "Редактирование способа оплаты";
                    break;
                case FormState.Add:
                    this.Title = "Добавление способа оплаты";
                    break;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            callback();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
