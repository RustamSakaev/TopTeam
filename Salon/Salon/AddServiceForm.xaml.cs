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

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для AddServiceForm.xaml
    /// </summary>
    public partial class AddServiceForm : Window
    {
        public AddServiceForm()
        {
            InitializeComponent();
        }
        public string Connection()
        {

            string conn = @"Data Source=LENOVO-PC;Initial Catalog=Salon;Integrated Security=True";
            return conn;
        }
        public void OnLoad(object sender, RoutedEventArgs e)
        {
            //комбобоксы
            //string str = "select * from TypeService";
            //DataTable dt = DataTool(str);
            //TypeService_IDCmbBox.DataContext = dt.DefaultView;
            //TypeService_IDCmbBox.DisplayMemberPath = "Name";
            //TypeService_IDCmbBox.SelectedValuePath = "ID_TypeService";
            //TypeService_IDCmbBox.SelectedIndex = 1;
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}