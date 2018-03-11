﻿using System;
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
        /// Dlya Select'a s navorotami
        /// </summary>
        /// <param name="sqlCommand"></param>
        public static DataTable GetDataWithCommand(SqlCommand sqlCommand)
        {
            var dtable = new DataTable();

            sqlCommand.Connection = _connection;
            
            using (var adapter = new SqlDataAdapter(sqlCommand.CommandText, _connection))
            {
                adapter.Fill(dtable);
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
        public static void Destroy()
        {
            _connection.Close();
        }
        private static SqlConnection _connection;
    }
}