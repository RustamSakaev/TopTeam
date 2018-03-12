using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBPaymentMethod
    {
        public static DataTable GetPaymentMethods()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_PaymentMethod as id,
                    Name as [Способ оплаты] 
                FROM PaymentMethod;"
            );
        }

        public static DataTable GetPaymentMethod(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    SELECT 
                        ID_PaymentMethod as id,
                        Name as [Способ оплаты] 
                    FROM PaymentMethod
                    WHERE ID_PaymentMethod = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            return DBCore.GetDataWithCommand(command);
        }
            

        public static void DeletePaymentMethod(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM PaymentMethod
                    WHERE ID_PaymentMethod = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditPaymentMethod(string id, string name)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE PaymentMethod
                    SET Name = @name
                    WHERE ID_PaymentMethod = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);

            DBCore.ExecuteCommand(command);
        }
    }
}