﻿using System;
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
    /// Логика взаимодействия для TypeServiceActionForm.xaml
    /// </summary>
    public partial class TypeServiceActionForm : Window
    {
        private readonly FormState _state;
        private readonly DataTable _currentDataItem;
        private readonly DataTable _CurGroupServiceDataItem;
        public TypeServiceActionForm(FormState state,string type_id=null, string group_id=null)
        {
            InitializeComponent();
            _state = state;
            switch (state)
            {
                case FormState.Edit:
                    HeaderLabel.Content = "Редактирование типа услуги";
                    _currentDataItem = DBTypeService.GetTypeService(type_id);
                    NameBox.Text = _currentDataItem.Rows[0]["Наименование"].ToString();
                    _CurGroupServiceDataItem = DBGroupService.GetGroupService(group_id);
                    foreach (DataRow type in DBGroupService.GetGroupServices().Rows)
                    {
                        GroupServiceCmbBox.Items.Add(type["Наименование"]);
                    }
                    GroupServiceCmbBox.SelectedValue = _CurGroupServiceDataItem.Rows[0]["Наименование"].ToString();
                    break;
                case FormState.Add:
                    HeaderLabel.Content = "Добавление типа услуги";
                    foreach (DataRow type in DBGroupService.GetGroupServices().Rows)
                    {
                        GroupServiceCmbBox.Items.Add(type["Наименование"]);
                    }
                    break;
            }
        }
        public string Connection()
        {
            string conn = @"Data Source=LENOVO-PC;Initial Catalog=Salon;Integrated Security=True";
            return conn;
        }
        public DataTable DataTool(string query)
        {
            string connStr = Connection();
            SqlConnection conn = null;
            SqlCommand comm = null;
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
                if (conn != null)
                {
                    comm = conn.CreateCommand();
                    comm.CommandText = query;
                    SqlDataAdapter adapter = new SqlDataAdapter(comm);
                    SqlCommandBuilder bild = new SqlCommandBuilder(adapter);
                    adapter.Fill(dt);
                    return dt;
                }
                else
                { return dt; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return dt;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GroupServiceFormButton_Click(object sender, RoutedEventArgs e)
        {
            GroupServiceForm group_service = new GroupServiceForm();
            group_service.ShowDialog();
        }
    }
}
