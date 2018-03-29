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
namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для GroupServiceActionForm.xaml
    /// </summary>
    public partial class GroupServiceActionForm : Window
    {
        private readonly FormState _state;
        private readonly DataTable _currentDataItem;
        public GroupServiceActionForm(FormState state, string group_id=null)
        {
            InitializeComponent();
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderLabel.Content = "Редактирование группы услуг";
                    _currentDataItem = DBGroupService.GetGroupService(group_id);
                    NameBox.Text = _currentDataItem.Rows[0]["Наименование"].ToString();
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление группы услуг";
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
