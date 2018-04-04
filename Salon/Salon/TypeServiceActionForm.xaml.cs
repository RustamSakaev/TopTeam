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
        private DataTable curData = new DataTable();
        private readonly DataTable MNcurrentData;
        private readonly Action Back;
        private string type_ID;
        private bool form_state_add;
        private  DataTable _MNcurrentData;
        private DataTable CurrentData
        {
            get { return currentData; }
            set
            {
                curData = value;
                TypeService_KindServiceGrid.ItemsSource = curData.DefaultView;
                TypeService_KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }
        public TypeServiceActionForm(Action b=null, FormState state=FormState.View,string type_id=null)
        {
            InitializeComponent();
            Back = b;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderLabel.Content = "Редактирование типа услуги";
                    currentData = DBTypeService.GetTypeService(type_id);
                    type_ID = type_id;
                    form_state_add = false;
                    NameBox.Text = currentData.Rows[0]["Наименование"].ToString();
                    GroupServiceCmbBox.ItemsSource = DBGroupService.GetGroupServices().DefaultView;
                    GroupServiceCmbBox.DisplayMemberPath = "Наименование";
                    GroupServiceCmbBox.SelectedValuePath = "id";
                    GroupServiceCmbBox.SelectedValue = currentData.Rows[0]["group_id"].ToString();
                    MNcurrentData = DBTypeService_KindService.GetKinds(type_id);
                    TypeService_KindServiceGrid.ItemsSource = MNcurrentData.DefaultView;
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление типа услуги";
                    form_state_add = true;
                    GroupServiceCmbBox.ItemsSource = DBGroupService.GetGroupServices().DefaultView;
                    GroupServiceCmbBox.DisplayMemberPath = "Наименование";
                    GroupServiceCmbBox.SelectedValuePath = "id";
                    AddButton.Visibility = Visibility.Hidden;
                    DeleteButton.Visibility = Visibility.Hidden;
                    TypeService_KindServiceGrid.Visibility = Visibility.Hidden;
                    KindLabel.Visibility = Visibility.Hidden;
                    break;
                

            }
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            if(type_ID!=null)
            {
                CurrentData = DBTypeService_KindService.GetKinds(type_ID);
                TypeService_KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(form_state_add)
            {
                //var type_name = NameBox.Text;
                //if (type_name == string.Empty) { MessageBox.Show("Заполните наименование!"); return; }

                //var group = GroupServiceCmbBox.SelectedValue;
                //if (group.ToString() == string.Empty) { MessageBox.Show("Заполните группу услуги!"); return; }

                //DBTypeService.AddTypeService(NameBox.Text, GroupServiceCmbBox.SelectedValue.ToString());
                
            }
            else
            {
                var form = new KindService(() => { CurrentData = DBTypeService_KindService.GetKinds(type_ID); TypeService_KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;  },type_ID);
                form.ShowDialog();
            }
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (form_state_add)
            {
              
                
            }
            else
            {
                var kind_col = ((DataView)TypeService_KindServiceGrid.ItemsSource).Table.Columns.IndexOf("kind_id");
                if (kind_col == -1) return;
                var kindid = ((DataRowView)TypeService_KindServiceGrid.SelectedItem)?.Row[kind_col].ToString();
                if (kindid == null) return;
                DBTypeService_KindService.DeleteKind(kindid);
                _MNcurrentData = DBTypeService_KindService.GetKinds(type_ID);
                TypeService_KindServiceGrid.ItemsSource = _MNcurrentData.DefaultView;
                TypeService_KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }
    }
}
