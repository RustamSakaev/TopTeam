using System.Data;
using System.Data.SqlClient;

namespace Salon.Database
{
    class DBTypeService
    {
        public static DataTable GetTypeServices()
        {
            return DBCore.GetData($@"
                SELECT 
                    TypeService.ID_TypeService as id,
                    TypeService.Name as Наименование, 
                    GroupService.Name as [Группа услуги],
                    TypeService.GroupService_ID as [group_id]
                FROM TypeService inner join GroupService on TypeService.GroupService_ID = GroupService.ID_GroupService;"
            );
        }

        public static DataTable GetTypeService(string id)
        {
            return DBCore.GetData($@"
                SELECT 
                    TypeService.ID_TypeService as id,
                    TypeService.Name as Наименование, 
                    GroupService.Name as [Группа услуги],
                    TypeService.GroupService_ID as [group_id]
                FROM TypeService inner join GroupService on TypeService.GroupService_ID = GroupService.ID_GroupService
                WHERE TypeService.ID_TypeService = {id};"
            );
        }

        public static void DeleteTypeService(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM TypeService
                    WHERE ID_TypeService = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditTypeService(string id, string name, string group_service)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE TypeService
                    SET Name = @name, GroupService_ID = @group_service
                    WHERE ID_TypeService = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@group_service", group_service);

            DBCore.ExecuteCommand(command);
        }

        public static void AddTypeService(string name, string group_service)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO TypeService (Name, GroupService_ID)
                    VALUES (@name, @group_service);"
            };

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@group_service", group_service);
           
            DBCore.ExecuteCommand(command);
        }
    }
}
