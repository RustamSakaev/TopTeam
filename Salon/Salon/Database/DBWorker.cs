using System;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBWorker
    {
        public static DataTable GetWorkers()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Worker as id,
                    Surname as ФИО,
                    Surname as Фамилия, 
                    Name as Имя, 
                    Patronymic as Отчество,
                    FORMAT(DBirth, 'dd/MM/yyyy', 'en-us') as [Дата рождения],
                    (CASE WHEN Gender <> 0 THEN 'Мужской' ELSE 'Женский' END) as Пол,
                    Exp as Опыт,
                    About as [О сотруднике]
                FROM Worker"
            );
        }
        public static DataTable GetTypesOfMasters()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_MasterType as ID,
                    Name as Name
                FROM MasterType"
            );
        }
        public static DataTable GetSchedule(string dateFrom, int id_worker = -9999)
        {
            if (id_worker == -9999)
            {
                return DBCore.GetData($@"
                SELECT Worker.Surname AS Мастер
                    ,Date AS Дата
                    ,TStart AS С
                    ,TEnd AS По
                FROM Schedule, Worker WHERE Worker.ID_Worker = Schedule.Worker_ID AND Schedule.Date >= '" + dateFrom + "'"
            );
            }
            return DBCore.GetData($@"
                SELECT Worker.Surname AS Мастер
                    ,Date AS Дата
                    ,TStart AS С
                    ,TEnd AS По
                FROM Schedule, Worker WHERE Worker.ID_Worker = Schedule.Worker_ID AND Schedule.Date >= '" + dateFrom + "' AND Schedule.Worker_ID = " + id_worker
            );
        }
        public static void AddScedule(string workerID, string date, string tstart, string tend, string busy = "1")
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO Schedule (Worker_ID, Date, TStart, TEnd, Busy)
                    VALUES (@worker_ID, @date, @tStart, @tEnd, @busy);"
            };

            command.Parameters.AddWithValue("@worker_ID", workerID);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@tStart", tstart);
            command.Parameters.AddWithValue("@tEnd", tend);
            command.Parameters.AddWithValue("@busy", busy);
            Console.WriteLine(command.ToString());

            DBCore.ExecuteCommand(command);
        }
        public static void AddWorker(string surname, string name, string patronymic, string dbirth, string login, string gender, string exp)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO Worker (Surname, Name, Patronymic, DBirth, Login, Gender, Exp)
                    VALUES (@surname, @name, @patronymic, @dbirth, @login, @gender, @exp);"
            };

            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@patronymic", patronymic);
            command.Parameters.AddWithValue("@dbirth", dbirth);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@exp", exp);

            DBCore.ExecuteCommand(command);
        }
    }
}
