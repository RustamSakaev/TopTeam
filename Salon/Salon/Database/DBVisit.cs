using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBVisit
    {
        public static DataTable GetVisits()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Visit as id,
                    Client_ID as clientid,
                    CONCAT_WS(' ', Client.Surname, Client.Name, Client.Patronymic) as Клиент,
                    Worker_ID as workerid,
                    CONCAT_WS(' ', Worker.Surname, Worker.Name, Worker.Patronymic) as Сотрудник,
                    FORMAT(Date, 'dd/MM/yyyy', 'en-us') as [Дата посещения],
                    FORMAT(TimeS, N'hh\:mm') as [Начальное время],
                    FORMAT(TimeE, N'hh\:mm') as [Конечное время],
                    Status_ID as statusid,
                    Status.Name as Статус
                FROM Visit
                INNER JOIN Client
                    ON Client.ID_Client = Visit.Client_ID
                INNER JOIN Worker
                    ON Worker.ID_Worker = Visit.Worker_ID
                INNER JOIN Status
                    ON Status.ID_Status = Visit.Status_ID;"
            );
        }

        public static DataTable GetVisit(string id)
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Visit as id,
                    Client_ID as clientid,
                    CONCAT_WS(' ', Client.Surname, Client.Name, Client.Patronymic) as Клиент,
                    Worker_ID as workerid,
                    CONCAT_WS(' ', Worker.Surname, Worker.Name, Worker.Patronymic) as Сотрудник,
                    FORMAT(Date, 'dd/MM/yyyy', 'en-us') as [Дата посещения],
                    FORMAT(TimeS, N'hh\:mm') as [Начальное время],
                    FORMAT(TimeE, N'hh\:mm') as [Конечное время],
                    Status_ID as statusid,
                    Status.Name as Статус
                FROM Visit
                INNER JOIN Client
                    ON Client.ID_Client = Visit.Client_ID
                INNER JOIN Worker
                    ON Worker.ID_Worker = Visit.Worker_ID
                INNER JOIN Status
                    ON Status.ID_Status = Visit.Status_ID
                WHERE Visit.ID_Visit = {id};"
            );
        }

        public static void DeleteVisit(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM Visit
                    WHERE ID_Visit = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditVisit(string id, string clientid, string workerid, string date, string times, string timee, string statusid)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE Visit
                    SET Client_ID = @clientid, Worker_ID = @workerid, Date = @date, TimeS = @times, TimeE = @timee, Status_ID = @statusid
                    WHERE ID_Visit = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@clientid", clientid);
            command.Parameters.AddWithValue("@workerid", workerid);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@times", times);
            command.Parameters.AddWithValue("@timee", timee);
            command.Parameters.AddWithValue("@statusid", statusid);

            DBCore.ExecuteCommand(command);
        }

        public static void AddVisit(string clientid, string workerid, string date, string times, string timee, string statusid)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO Visit (Client_ID, Worker_ID, Date, TimeS, TimeE, Status_ID)
                    VALUES (@clientid, @workerid, @date, @times, @timee, @statusid);"
            };

            command.Parameters.AddWithValue("@clientid", clientid);
            command.Parameters.AddWithValue("@workerid", workerid);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@times", times);
            command.Parameters.AddWithValue("@timee", timee);
            command.Parameters.AddWithValue("@statusid", statusid);

            DBCore.ExecuteCommand(command);
        }
    }
}