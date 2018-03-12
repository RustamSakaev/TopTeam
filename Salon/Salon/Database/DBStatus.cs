using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBStatus
    {
        public static DataTable GetStatuses()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Status as id,
                    Name as [Статус посещения] 
                FROM Status;"
            );
        }

        public static DataTable GetStatus(string id)
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Status as id,
                    Name as [Статус посещения] 
                FROM Status
                WHERE ID_Status = {id};"
            );
        }

        public static void DeleteStatus(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM Status
                    WHERE ID_Status = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditStatus(string id, string name)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE Status
                    SET Name = @name
                    WHERE ID_Status = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);

            DBCore.ExecuteCommand(command);
        }

        public static void AddStatus(string name)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO Status (Name)
                    VALUES (@name);"
            };

            command.Parameters.AddWithValue("@name", name);

            DBCore.ExecuteCommand(command);
        }
    }
}