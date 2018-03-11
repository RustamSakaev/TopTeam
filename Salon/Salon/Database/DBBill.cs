using System.Data;

namespace Salon
{
    internal static class DBBill
    {
        public static DataTable GetBills() => DBCore.GetData($@"
            SELECT 
                ID_Bill as id, 
                Date as Дата,
                Number as Номер,
                BillAmount as Сумма,
                Paid as Заплачено,
                PaymentMethod.Name as [Тип оплаты]
            FROM Bill
            INNER JOIN PaymentMethod 
                ON PaymentMethod.ID_PaymentMethod = Bill.PaymentMethod_ID"
        );
    }
}