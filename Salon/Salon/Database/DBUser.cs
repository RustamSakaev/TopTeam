using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBUser
    {
        public static bool CreateUser(string login, string pass, string role)
        {
            DataTable dt = GetUser(login);
            if (dt.Rows[0][0].ToString() == "")
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = $@"USE master create login " + login + " with password = '" + pass + "', check_policy = off; USE[Salon] create user " + login + " from login " + login + " exec sp_addrolemember 'db_"+role+"', '" + login + "'; exec sp_addsrvrolemember '" + login + "', 'sysadmin'"
                };
                DBCore.ExecuteCommand(command);
                return true;
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return false;
            }
        }
        public static bool Connection(string server, string login, string pass)
        {
            bool IsLog=DBCore.InitLogPass(server,login, pass);
            return IsLog;
        }
        public static void ChangePass(string user, string newpass, string oldpass)
        {
            var command = new SqlCommand
            {
                CommandText = $@"ALTER LOGIN "+user+" WITH PASSWORD = '"+newpass+"' OLD_PASSWORD = '"+oldpass+"'"
            };

            DBCore.ExecuteCommand(command);
        }
        public static string GetRoles(string userName)
        {
            string role="";
            List<string> user = new List<string>();
            //List<string> db = new List<string>();
            string str = "use Salon EXEC sp_helpuser '" + userName + "'";
            DataTable dt = DBCore.GetData(str);
            foreach (DataRow dr in dt.Rows)
            {
                user.Add(dr["RoleName"].ToString());
            }
            if (user.Contains("db_master"))
                role = "Master";
            if (user.Contains("db_admin"))
                role = "Admin";
            return role;
            //string str1 = "use Salon EXEC sp_helprole";
            //DataTable dt1 = DBCore.GetData(str1);
            //foreach (DataRow dr in dt1.Rows)
            //{
            //    db.Add(dr["RoleName"].ToString());
            //}
            //List<string> NoUserRole = db.Except(user).ToList();
            //return NoUserRole;
        }

        public static DataTable GetUser(string login)
        {
            return DBCore.GetData($@"SELECT suser_id('"+login+"');");
        }

        public static bool GetOldPass(string login,string pass)
        {
            string str = $@"select * from master.dbo.syslogins where name = '" + login + "' and PWDCOMPARE('" + pass + "',password) = 1;";
            DataTable dt = DBCore.GetData(str);
            if (dt.Rows.Count != 0)
                return true;
            else
                return false;

        }

        public static DataTable GetUsers()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Worker as id,
                    CONCAT_WS(' ', Surname, Name, Patronymic) as ФИО,
                    Surname as Фамилия, 
                    Name as Имя, 
                    Patronymic as Отчество,
                    (CASE WHEN Gender <> 0 THEN 'Мужской' ELSE 'Женский' END) as Пол,
                    Login as Логин
                FROM Worker;"
            );
        }
    }
}
