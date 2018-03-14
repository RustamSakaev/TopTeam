using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBUser
    {
        public static void CreateUser(string login, string pass)
        {
            var command = new SqlCommand
            {
                CommandText = $@"USE master
                    create login @login with password = '@pass', check_policy = off;
                    USE[Task3]
                    create user @login from login @login
                    exec sp_addrolemember 'db_datareader', '@login'"
            };       
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@pass", pass);
            DBCore.ExecuteCommand(command);
        }
        public static void Connection(string server, string login, string pass)
        {
            DBCore.InitLogPass(server,login, pass);
        }
        public static void ChangePass(string user, string newpass, string oldpass)
        {
            var command = new SqlCommand
            {
                CommandText = $@"ALTER LOGIN @login WITH PASSWORD = '@newpass' OLD_PASSWORD = '@oldpass'"
            };

            command.Parameters.AddWithValue("@login", user);
            command.Parameters.AddWithValue("@newpass", newpass);
            command.Parameters.AddWithValue("@oldpass", oldpass);

            DBCore.ExecuteCommand(command);
        }
        public static List<string> GetRoles(string userName)
        {
            List<string> user = new List<string>();
            List<string> db = new List<string>();
            string str = "use Task3 EXEC sp_helpuser '" + userName + "'";
            DataTable dt = DBCore.GetData(str);
            foreach (DataRow dr in dt.Rows)
            {
                user.Add(dr["RoleName"].ToString());
            }
            string str1 = "use Task3 EXEC sp_helprole";
            DataTable dt1 = DBCore.GetData(str1);
            foreach (DataRow dr in dt1.Rows)
            {
                db.Add(dr["RoleName"].ToString());
            }
            List<string> NoUserRole = db.Except(user).ToList();
            return NoUserRole;
        }
    }
}
