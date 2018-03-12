using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBBankCard
    {
        public static DataTable GetBankCards()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_BankCard as id,
                    CONCAT_WS(' ', Client.Surname, Client.Name, Client.Patronymic) as ФИО,
                    Number as Номер
                FROM BankCard
                INNER JOIN Client
                    ON Client.ID_Client = BankCard.Client_ID;"
            );
        }

        public static DataTable GetBankCard(string id)
        {
            return DBCore.GetData($@"
                SELECT
                    BankCard.ID_BankCard as id,
                    CONCAT_WS(' ', Client.Surname, Client.Name, Client.Patronymic) as ФИО,
                    Number as Номер
                FROM BankCard
                INNER JOIN Client
                    ON Client.ID_Client = BankCard.Client_ID
                WHERE BankCard.ID_BankCard = {id};"
            );
        }

        public static void DeleteBankCard(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM BankCard
                    WHERE ID_BankCard = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditBankCard(string id, string number)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE BankCard
                    SET Number = @number
                    WHERE ID_BankCard = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@number", number);

            DBCore.ExecuteCommand(command);
        }

        public static void AddBankCard(string clientid, string number)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO BankCard (Client_ID, Number)
                    VALUES (@clientid, @number);"
            };

            command.Parameters.AddWithValue("@clientid", clientid);
            command.Parameters.AddWithValue("@number", number);

            DBCore.ExecuteCommand(command);
        }
    }
}