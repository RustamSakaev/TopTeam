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
            try
            {
                DataTable dt = GetUser(login);
                if (dt.Rows[0][0].ToString() == "")
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = $@"USE master create login " + login + " with password = '" + pass + "', check_policy = off; USE[Salon] create user " + login + " from login " + login + " exec sp_addrolemember 'db_" + role + "', '" + login + "'; exec sp_addsrvrolemember '" + login + "', 'sysadmin'"
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
            {
                var command = new SqlCommand
                {
                    CommandText = $@"ALTER LOGIN " + user + " WITH PASSWORD = '" + newpass + "' OLD_PASSWORD = '" + oldpass + "'"
                };

                DBCore.ExecuteCommand(command);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string GetRoles(string userName)
        {
            string role = "";
            try
            {             
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
                //string str1 = "use Salon EXEC sp_helprole";
                //DataTable dt1 = DBCore.GetData(str1);
                //foreach (DataRow dr in dt1.Rows)
                //{
                //    db.Add(dr["RoleName"].ToString());
                //}
                //List<string> NoUserRole = db.Except(user).ToList();
                //return NoUserRole;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                role="";
            }
            return role;
        }

        public static DataTable GetUser(string login)
        {
            return DBCore.GetData($@"SELECT suser_id('"+login+"');");
        }

        public static bool GetOldPass(string login,string pass)
        {
            try
            {
                string str = $@"select * from master.dbo.syslogins where name = '" + login + "' and PWDCOMPARE('" + pass + "',password) = 1;";
                DataTable dt = DBCore.GetData(str);
                if (dt.Rows.Count != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static DataTable GetUsers()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Worker as id,
                    CONCAT_WS(' ', Surname, Name, Patronymic) as ФИО,
                    Login as Логин
                FROM Worker;"
            );
        }

        public static int GetUserId(string login)
        {
            string str = $@"SELECT ID_Worker FROM Worker where [Login]='"+login+"'";
            DataTable dt = DBCore.GetData(str);
            int id = Convert.ToInt32(dt.Rows[0][0]);
            return id;
        }
    }
}
