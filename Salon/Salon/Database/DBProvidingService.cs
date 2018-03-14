using System.Data;

namespace Salon
{
    internal static class DBProvidingService
    {
        public static DataTable GetProvidingServices(string visitid)
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_ProvidingServices as id,
                    Service.Name as [Наименование услуги],
                    Tariff.Cost as [Цена]
                FROM ProvidingServices
                INNER JOIN Service
                    ON Service.ID_Service = ProvidingServices.Service_ID
                INNER JOIN Tariff
                    ON Tariff.ID_Tariff = ProvidingServices.Tariff_ID
                WHERE Visit_ID = {visitid}"
            );
        }
    }
}