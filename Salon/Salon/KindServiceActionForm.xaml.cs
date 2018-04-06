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
        private DataTable curData = new DataTable();
        private readonly Action Back;
        private readonly DataTable MNcurrentData;
        private string kind_ID;
        private bool form_state_add;
        private DataTable _MNcurrentData;

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
        public KindServiceActionForm(Action b, FormState state, string kind_id = null)
        {
            InitializeComponent();
            Back = b;
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    kind_ID = kind_id;
                    form_state_add = false;
                    HeaderLabel.Content = "Редактирование вида услуги";
                    currentData = DBKindService.GetKindService(kind_id);
                    NameBox.Text = currentData.Rows[0]["Наименование"].ToString();
                    MNcurrentData = DBTypeService_KindService.GetTypes(kind_id);
                    TypeService_KindServiceGrid.ItemsSource = MNcurrentData.DefaultView;
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление вида услуги";
                    break;
            }
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            if (kind_ID != null)
            {
                CurrentData = DBTypeService_KindService.GetTypes(kind_ID);
                TypeService_KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (form_state_add)
            {

            }
            else
            {
                var form = new TypeServiceForm(() => { CurrentData = DBTypeService_KindService.GetTypes(kind_ID); TypeService_KindServiceGrid.Columns[0].Visibility = Visibility.Hidden; }, kind_ID);
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
                var type_col = ((DataView)TypeService_KindServiceGrid.ItemsSource).Table.Columns.IndexOf("type_id");
                if (type_col == -1) return;
                var typeid = ((DataRowView)TypeService_KindServiceGrid.SelectedItem)?.Row[type_col].ToString();
                if (typeid == null) return;
                DBTypeService_KindService.DeleteType(typeid);
                _MNcurrentData = DBTypeService_KindService.GetTypes(kind_ID);
                TypeService_KindServiceGrid.ItemsSource = _MNcurrentData.DefaultView;
                TypeService_KindServiceGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }
    }
}
