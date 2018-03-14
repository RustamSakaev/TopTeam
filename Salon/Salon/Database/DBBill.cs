using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBBill
    {
        public static DataTable GetBills()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Bill as id,
                    CONCAT_WS(' ', Client.Surname, Client.Name, Client.Patronymic) as ФИО,
                    Bill.Date as Дата,
                    Bill.Number as Номер,
                    Bill.BillAmount as Сумма,
                    Bill.Paid as Заплачено,
                    PaymentMethod.Name as [Способ оплаты]
                FROM Bill
                INNER JOIN PaymentMethod 
                    ON PaymentMethod.ID_PaymentMethod = Bill.PaymentMethod_ID
                INNER JOIN Visit
                    ON Visit.ID_Visit = Bill.Visit_ID
                INNER JOIN Client
                    ON Client.ID_Client = Visit.Client_ID;"
            );
        }


        public static DataTable GetDetailedBill(string id)
        {
            return DBCore.GetData($@"
                SELECT
                    ID_Bill as id,
                    Bill.Visit_ID as visitid,
                    Bill.Date as Дата,
                    Bill.Number as Номер,
                    Bill.BillAmount as Сумма,
                    Bill.Paid as Оплачено,
                    Bill.PaymentMethod_ID as paymentmethodid,
                    PaymentMethod.Name as [Способ оплаты],
                    Bill.GiftCard_ID as giftcardid,
                    GiftCard.Number as [Номер подарочной карты],
                    Bill.BankCard_ID as bankcardid,
                    BankCard.Number as [Номер банковской карты]
                FROM Bill
                INNER JOIN PaymentMethod
                    ON PaymentMethod.ID_PaymentMethod = Bill.PaymentMethod_ID
                LEFT JOIN GiftCard
                    ON GiftCard.ID_GiftCard = Bill.GiftCard_ID
                LEFT JOIN BankCard
                    ON BankCard.ID_BankCard = Bill.BankCard_ID
                WHERE ID_Bill = {id}"
            );
        }

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