using System.Data;
using System.Data.SqlClient;

namespace Salon.Database
{
    class DBService
    {
        public static DataTable GetServices()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Service as id,
                    Service.Name as Наименование, 
                    TypeService.Name as [Тип услуги], 
                    KindService.Name as [Вид услуги],
                    GroupService.Name as [Группа услуги],
                    Service.TypeService_ID as type_id,
                    Service.KindService_ID as kind_id
                FROM Service inner join TypeService on Service.TypeService_ID = TypeService.ID_TypeService
                inner join KindService on Service.KindService_ID = KindService.ID_KindService
                inner join GroupService on TypeService.GroupService_ID = GroupService.ID_GroupService;"
            );
        }

        public static DataTable GetService(string id)
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Service as id,
                    Service.Name as Наименование, 
                    TypeService.Name as [Тип услуги], 
                    KindService.Name as [Вид услуги],
                    GroupService.Name as [Группа услуги],
                    Service.TypeService_ID as type_id,
                    Service.KindService_ID as kind_id
                FROM Service inner join TypeService on Service.TypeService_ID = TypeService.ID_TypeService
                inner join KindService on Service.KindService_ID = KindService.ID_KindService
                inner join GroupService on TypeService.GroupService_ID = GroupService.ID_GroupService
                WHERE Service.ID_Service = {id};"
            );
        }

        public static void DeleteService(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM Service
                    WHERE ID_Service = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditService(string id, string name, string type_service, string kind_service)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE Service
                    SET Name = @name, TypeService_ID = @type_service, KindService_ID = @kind_service
                    WHERE ID_Service = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@type_service", type_service);
            command.Parameters.AddWithValue("@kind_service", kind_service);

            DBCore.ExecuteCommand(command);
        }

        public static void AddService(string name, string type_service, string kind_service)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO Service (Name, TypeService_ID, KindService_ID)
                    VALUES (@name, @type_service, @kind_service);"
            };

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@type_service", type_service);
            command.Parameters.AddWithValue("@kind_service", kind_service);
            
            DBCore.ExecuteCommand(command);
        }
    }
}
