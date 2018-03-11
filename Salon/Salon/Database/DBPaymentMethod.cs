using System.Data;

namespace Salon
{
    internal static class DBPaymentMethod
    {
        public static DataTable GetPaymentMethods() =>
            DBCore.GetData($@"
                SELECT 
                    ID_PaymentMethod as id,
                    Name as [Способ оплаты] 
                FROM PaymentMethod;"
            );
    }
}