using System;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBCore
    {
        /// <summary>
        /// Dlya Select'a
        /// </summary>
        /// <param name="sqlQuery">SQL Zapros</param>
        public static DataTable GetData(string sqlQuery)
        {
            var dtable = new DataTable();

            using (var command = new SqlCommand(sqlQuery, _connection))
            {
                using (var adapter = new SqlDataAdapter(command.CommandText, _connection))
                {
                    adapter.Fill(dtable);
                }
            }

            return dtable;
        }

        /// <summary>
        /// Update, delete, insert
        /// </summary>
        /// <param name="sqlQuery"></param>
        public static void ExecuteSql(string sqlQuery)
        {
            using (var command = new SqlCommand(sqlQuery, _connection))
            {
                command.ExecuteNonQuery();
            }
        }
        public static void ExecuteCommand(SqlCommand sqlCommand)
        {
            sqlCommand.Connection = _connection;
            sqlCommand.ExecuteNonQuery();
        }
        public static void Init(string server)
        {
            _connection = new SqlConnection();           
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,        
                InitialCatalog = "Salon",
                IntegratedSecurity = true,
            };

            _connection.ConnectionString = builder.ConnectionString;
            _connection.Open();
        }
        public static bool InitLogPass(string server, string login,string pass)
        {
            bool rValue = true;
            try
           {
                _connection = new SqlConnection();
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = server,
                    InitialCatalog = "Salon",
                    UserID = login,
                    Password = pass,
                };
                _connection.ConnectionString = builder.ConnectionString;
                _connection.Open();
            }
            catch
            {
                rValue = false;
            }
            return rValue;
        }
        public static void Destroy()
        {
            if (_connection!=null)
            _connection.Close();
        }
        private static SqlConnection _connection;
    }
}