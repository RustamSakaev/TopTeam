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
using System.Data;
using System.Data.SqlClient;
using Salon.Database;
using Salon.Extensions;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для KindServiceActionForm.xaml
    /// </summary>
    public partial class KindServiceActionForm : Window
    {
        private readonly FormState _state;
        private readonly DataTable currentData;
        private readonly Action Back;
        public KindServiceActionForm(Action b, FormState state, string kind_id = null)
        {
            InitializeComponent();
            Back = b;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderLabel.Content = "Редактирование вида услуги";
                    currentData = DBKindService.GetKindService(kind_id);
                    NameBox.Text = currentData.Rows[0]["Наименование"].ToString();
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление вида услуги";
                    break;
            }
        }

     
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NameBox.Validate(true))
            {
                return;
            }
            switch (_state)
            {
                case FormState.Edit:
                    DBKindService.EditKindService(
                        currentData.Rows[0]["id"].ToString(),
                        NameBox.Text
                    );
                    break;
                case FormState.Add:
                    DBKindService.AddKindService(
                        NameBox.Text
                    );
                    break;
            }

            Back();
            this.Close();
        }
    }
}
