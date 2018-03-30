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
    /// Логика взаимодействия для TypeServiceActionForm.xaml
    /// </summary>
    public partial class TypeServiceActionForm : Window
    {
        private readonly FormState _state;
        private readonly DataTable currentData;
        private readonly Action Back;
        public TypeServiceActionForm(Action b, FormState state,string type_id=null)
        {
            InitializeComponent();
            Back = b;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderLabel.Content = "Редактирование типа услуги";
                    currentData = DBTypeService.GetTypeService(type_id);
                    NameBox.Text = currentData.Rows[0]["Наименование"].ToString();
                    GroupServiceCmbBox.ItemsSource = DBGroupService.GetGroupServices().DefaultView;
                    GroupServiceCmbBox.DisplayMemberPath = "Наименование";
                    GroupServiceCmbBox.SelectedValuePath = "id";
                    GroupServiceCmbBox.SelectedValue = currentData.Rows[0]["group_id"].ToString();
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление типа услуги";
                    GroupServiceCmbBox.ItemsSource = DBGroupService.GetGroupServices().DefaultView;
                    GroupServiceCmbBox.DisplayMemberPath = "Наименование";
                    GroupServiceCmbBox.SelectedValuePath = "id";
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
                    DBTypeService.EditTypeService(
                        currentData.Rows[0]["id"].ToString(),
                        NameBox.Text,
                        GroupServiceCmbBox.SelectedValue.ToString()
                    );
                    break;
                case FormState.Add:
                    DBTypeService.AddTypeService(
                        NameBox.Text,
                        GroupServiceCmbBox.SelectedValue.ToString()
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

        private void GroupServiceFormButton_Click(object sender, RoutedEventArgs e)
        {
            GroupServiceForm group_service = new GroupServiceForm();
            group_service.ShowDialog();
        }
    }
}
