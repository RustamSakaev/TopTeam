using System.Data;

namespace Salon
{
    internal static class DBBankCard
    {
        public static DataTable GetBankCards()
        {
            return DBCore.GetData($@"
                SELECT 
                    CONCAT_WS(' ', Client.Surname, Client.Name, Client.Patronymic) as ФИО,
                    Number as Номер
                FROM BankCard
                INNER JOIN Client
                    ON Client.ID_Client = BankCard.Client_ID;"
            );
        }
    }
}