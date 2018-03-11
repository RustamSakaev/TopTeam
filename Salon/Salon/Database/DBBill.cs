using System.Data;

namespace Salon
{
    internal static class DBBill
    {
        public static DataTable GetBills() => DBCore.GetData($@"SELECT * FROM Bill");
    }
}