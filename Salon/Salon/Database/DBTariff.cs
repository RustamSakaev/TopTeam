using System.Data;
using System.Data.SqlClient;

namespace Salon.Database
{
    class DBTariff
    {
        public static DataTable GetTariffs()
        {
            return DBCore.GetData($@"
                SELECT 
                    Tariff.ID_Tariff as id,
                    Service.Name as Услуга,
                    FORMAT(StartDate, 'dd/MM/yyyy', 'en-us')  as [Дата начала],
                    Convert(varchar(10),Tariff.Cost) as Стоимость,
                    Tariff.Service_ID as serv_id
                FROM Tariff inner join Service on Tariff.Service_ID = Service.ID_Service;"
            );
        }

        public static DataTable GetTariff(string id)
        {
            return DBCore.GetData($@"
                SELECT 
                    Tariff.ID_Tariff as id,
                    Service.Name as Услуга,
                    Tariff.StartDate as [Дата начала],
                    Tariff.Cost as Стоимость,
                    Tariff.Service_ID as serv_id
                FROM Tariff inner join Service on Tariff.Service_ID = Service.ID_Service
                WHERE Tariff.ID_Tariff = {id};"
            );
        }

        public static void DeleteTariff(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM Tariff
                    WHERE ID_Tariff = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditTariff(string id, string serv, string startdate, double cost)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE Tariff
                    SET Service_ID = @serv, StartDate = @startdate, Cost = @cost
                    WHERE ID_Tariff = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@serv", serv);
            command.Parameters.AddWithValue("@startdate", startdate);
            command.Parameters.AddWithValue("@cost", cost);


            DBCore.ExecuteCommand(command);
        }

        public static void AddTariff(string serv, string startdate, double cost)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO Tariff (Service_ID, StartDate, Cost)
                    VALUES (@serv, @startdate, @cost);"
            };

            command.Parameters.AddWithValue("@serv", serv);
            command.Parameters.AddWithValue("@startdate", startdate);
            command.Parameters.AddWithValue("@cost", cost);

            DBCore.ExecuteCommand(command);
        }
    }
}
