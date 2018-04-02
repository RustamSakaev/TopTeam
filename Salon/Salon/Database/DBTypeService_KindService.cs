using System.Data;
using System.Data.SqlClient;


namespace Salon.Database
{
    class DBTypeService_KindService
    {
        public static DataTable GetAllTypeKind()
        {
            return DBCore.GetData($@"
                SELECT 
                    TypeService.Name as [Тип услуги],
                    KindService.Name as [Вид услуги], 
                    TypeService_KindService.TypeService_ID as type_id,
                    TypeService_KindService.KindService_ID as kind_id
                FROM TypeService_KindService inner join TypeService on TypeService_KindService.TypeService_ID = TypeService.ID_TypeService
                inner join KindService on TypeService_KindService.KindService_ID = KindService.ID_KindService;"
            );
        }

        public static DataTable GetTypes(string kind_id)
        {
            return DBCore.GetData($@"
                SELECT 
                    TypeService.Name as [Тип услуги],
                    TypeService_KindService.TypeService_ID as type_id
                FROM TypeService_KindService inner join TypeService on TypeService_KindService.TypeService_ID = TypeService.ID_TypeService
                inner join KindService on TypeService_KindService.KindService_ID = KindService.ID_KindService
                WHERE TypeService_KindService.KindService_ID = {kind_id};"
            );
        }
        public static DataTable GetKinds(string type_id)
        {
            return DBCore.GetData($@"
                SELECT 
                    KindService.Name as [Вид услуги],
                    TypeService_KindService.KindService_ID as kind_id
                FROM TypeService_KindService inner join TypeService on TypeService_KindService.TypeService_ID = TypeService.ID_TypeService
                inner join KindService on TypeService_KindService.KindService_ID = KindService.ID_KindService
                WHERE TypeService_KindService.TypeService_ID = {type_id};"
            );
        }

        public static void DeleteType(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM TypeService_KindService
                    WHERE TypeService_KindService.KindService_ID = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }
        public static void DeleteKind(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM TypeService_KindService
                    WHERE TypeService_KindService.TypeService_ID = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void AddTypeKind(string type_id, string kind_id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO TypeService_KindService (TypeService_ID, KindService_ID)
                    VALUES (@type, @kind);"
            };

            command.Parameters.AddWithValue("@type", type_id);
            command.Parameters.AddWithValue("@kind", kind_id);

            DBCore.ExecuteCommand(command);
        }
    }
}
