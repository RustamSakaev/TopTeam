using System.Data.SqlClient;
using System.Data;

namespace Salon.Database
{
    class DBTypeMaster
    {
        public static DataTable GetTypeMasters()
        {
            return DBCore.GetData($@"
                SELECT 
                    MasterType.ID_MasterType as id,
                    MasterType.Name as Наименование                    
                FROM MasterType;"
            );
        }
        public static DataTable GetTypeMaster(string id)
        {
            return DBCore.GetData($@"
                SELECT 
                    MasterType.ID_MasterType as id,
                    MasterType.Name as Наименование    
                FROM MasterType
                WHERE MasterType.ID_MasterType = {id};"
            );
        }
        public static void DeleteTypeMaster(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM MasterType
                    WHERE ID_MasterType = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }
        public static void EditTypeMaster(string id, string name)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE MasterType
                    SET Name = @name
                    WHERE ID_MasterType = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);

            DBCore.ExecuteCommand(command);
        }
        public static void AddTypeService(string name)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO MasterType (Name)
                    VALUES (@name);"
            };

            command.Parameters.AddWithValue("@name", name);

            DBCore.ExecuteCommand(command);
        }
    }
}
