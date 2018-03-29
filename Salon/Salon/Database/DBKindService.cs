using System.Data;
using System.Data.SqlClient;

namespace Salon.Database
{
    class DBKindService
    {
        public static DataTable GetKindServices()
        {
            return DBCore.GetData($@"
                SELECT 
                    KindService.ID_KindService as id,
                    KindService.Name as Наименование 
                FROM KindService;"
            );
        }

        public static DataTable GetKindService(string id)
        {
            return DBCore.GetData($@"
               SELECT 
                    KindService.ID_KindService as id,
                    KindService.Name as Наименование 
                FROM KindService
                WHERE KindService.ID_KindService = {id};"
            );
        }

        public static void DeleteKindService(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM KindService
                    WHERE ID_KindService = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditKindService(string id, string name)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE KindService
                    SET Name = @name
                    WHERE ID_KindService = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            

            DBCore.ExecuteCommand(command);
        }

        public static void AddKindService(string name)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO KindService (Name)
                    VALUES (@name);"
            };

            command.Parameters.AddWithValue("@name", name);

            DBCore.ExecuteCommand(command);
        }
    }
}
