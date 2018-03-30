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
    /// Логика взаимодействия для ServiceActionForm.xaml
    /// </summary>
    public partial class ServiceActionForm : Window
    {
        private readonly FormState _state;
        private readonly DataTable currentData;
        private readonly Action Back;
        public ServiceActionForm(Action b, FormState state, string serv_id=null)
        {
            InitializeComponent();
            Back = b;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderLabel.Content = "Редактирование услуги";
                    currentData = DBService.GetService(serv_id);
                    NameBox.Text = currentData.Rows[0]["Наименование"].ToString();
                  
                    TypeServiceCmbBox.ItemsSource = DBTypeService.GetTypeServices().DefaultView;
                    TypeServiceCmbBox.DisplayMemberPath = "Наименование";
                    TypeServiceCmbBox.SelectedValuePath = "id";
                    TypeServiceCmbBox.SelectedValue = currentData.Rows[0]["type_id"].ToString();

                    KindServiceCmbBox.ItemsSource = DBKindService.GetKindServices().DefaultView;
                    KindServiceCmbBox.DisplayMemberPath = "Наименование";
                    KindServiceCmbBox.SelectedValuePath = "id";
                    KindServiceCmbBox.SelectedValue = currentData.Rows[0]["kind_id"].ToString();
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление услуги";

                    TypeServiceCmbBox.ItemsSource = DBTypeService.GetTypeServices().DefaultView;
                    TypeServiceCmbBox.DisplayMemberPath = "Наименование";
                    TypeServiceCmbBox.SelectedValuePath = "id";

                    KindServiceCmbBox.ItemsSource = DBKindService.GetKindServices().DefaultView;
                    KindServiceCmbBox.DisplayMemberPath = "Наименование";
                    KindServiceCmbBox.SelectedValuePath = "id";
                    break;
            }
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
                    DBService.EditService(
                        currentData.Rows[0]["id"].ToString(),
                        NameBox.Text,
                        TypeServiceCmbBox.SelectedValue.ToString(),
                        KindServiceCmbBox.SelectedValue.ToString()
                    );
                    break;
                case FormState.Add:
                    DBService.AddService(
                        NameBox.Text,
                        TypeServiceCmbBox.SelectedValue.ToString(),
                        KindServiceCmbBox.SelectedValue.ToString()
                    );
                    break;
            }

            Back();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TypeServiceFormButton_Click(object sender, RoutedEventArgs e)
        {
            TypeServiceForm type = new TypeServiceForm();
            type.ShowDialog();
        }

        private void KindServiceFormButton_Click(object sender, RoutedEventArgs e)
        {
            KindService kind = new KindService();
            kind.ShowDialog();
        }

       
    }
}
