using System.Data;
using System.Data.SqlClient;

namespace Salon.Database
{
    class DBGroupService
    {
        public static DataTable GetServices()
        {
            return DBCore.GetData($@"
                SELECT 
                    GroupService.ID_GroupService as id,
                    GroupService.Name as Наименование, 
                FROM GroupService;"
            );
        }

        public static DataTable GetService(string id)
        {
            return DBCore.GetData($@"
               SELECT 
                    GroupService.ID_GroupService as id,
                    GroupService.Name as Наименование, 
                FROM GroupService
                WHERE GroupService.ID_GroupService = {id};"
            );
        }

        public static void DeleteService(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM GroupService
                    WHERE ID_GroupService = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditService(string id, string name)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE GroupService
                    SET Name = @name
                    WHERE ID_GroupService = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);


            DBCore.ExecuteCommand(command);
        }

        public static void AddService(string name)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO GroupService (Name)
                    VALUES (@name);"
            };

            command.Parameters.AddWithValue("@name", name);

            DBCore.ExecuteCommand(command);
        }
    }
}
