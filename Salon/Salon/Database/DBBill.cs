using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBBill
    {
        public static DataTable GetBills() =>
            DBCore.GetData($@"
                SELECT 
                    ID_Bill as id,
                    CONCAT_WS(' ', Client.Surname, Client.Name, Client.Patronymic) as ФИО,
                    Bill.Date as Дата,
                    Number as Номер,
                    BillAmount as Сумма,
                    Paid as Заплачено,
                    PaymentMethod.Name as [Способ оплаты]
                FROM Bill
                INNER JOIN PaymentMethod 
                    ON PaymentMethod.ID_PaymentMethod = Bill.PaymentMethod_ID
                INNER JOIN Visit
                    ON Visit.ID_Visit = Bill.Visit_ID
                INNER JOIN Client
                    ON Client.ID_Client = Visit.Client_ID;"
            );

        public static void DeleteBill(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM Bill
                    WHERE ID_Bill = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void CompleteBill(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE Bill
                    SET Paid = 1
                    WHERE ID_Bill = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }
    }
}