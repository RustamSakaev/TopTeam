using System.Data;

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
                    ON Client.ID_Client = Visit.ID_Visit"
            );
    }
}