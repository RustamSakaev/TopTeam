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
                CommandText = $@"USE master create login "+login+" with password = '"+pass+ "', check_policy = off; USE[Salon] create user " + login+" from login "+login+" exec sp_addrolemember 'db_datareader', '"+login+"';"
            };       
            /*command.Parameters.AddWithValue("@loginn", login);
            command.Parameters.AddWithValue("@pass", pass);*/
            DBCore.ExecuteCommand(command);
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
        public static List<string> GetRoles(string userName)
        {
            List<string> user = new List<string>();
            List<string> db = new List<string>();
            string str = "use Salon EXEC sp_helpuser '" + userName + "'";
            DataTable dt = DBCore.GetData(str);
            foreach (DataRow dr in dt.Rows)
            {
                user.Add(dr["RoleName"].ToString());
            }
            string str1 = "use Salon EXEC sp_helprole";
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
