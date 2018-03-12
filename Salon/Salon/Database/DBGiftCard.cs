using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBGiftCard
    {
        public static DataTable GetGiftCards()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_GiftCard as id,
                    GiftCard.Client_ID as clientid,
                    CONCAT_WS(' ', Client.Surname, Client.Name, Client.Patronymic) as Клиент,
                    GiftCard.Worker_ID as workerid,
                    CONCAT_WS(' ', Worker.Surname, Worker.Name, Worker.Patronymic) as Сотрудник,
                    Number as Номер,
                    GivingDate as [Дата выдачи],
                    Nominal as Номинал
                FROM GiftCard
                INNER JOIN Client
                    ON Client.ID_Client = GiftCard.Client_ID
                INNER JOIN Worker
                    ON Worker.ID_Worker = GiftCard.Worker_ID;"
            );
        }

        public static DataTable GetGiftCard(string id)
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_GiftCard as id,
                    GiftCard.Client_ID as clientid,
                    CONCAT_WS(' ', Client.Surname, Client.Name, Client.Patronymic) as Клиент,
                    GiftCard.Worker_ID as workerid,
                    CONCAT_WS(' ', Worker.Surname, Worker.Name, Worker.Patronymic) as Сотрудник,
                    Number as Номер,
                    GivingDate as [Дата выдачи],
                    Nominal as Номинал
                FROM GiftCard
                INNER JOIN Client
                    ON Client.ID_Client = GiftCard.Client_ID
                INNER JOIN Worker
                    ON Worker.ID_Worker = GiftCard.Worker_ID
                WHERE GiftCard.ID_GiftCard = {id};"
            );
        }

        public static void DeleteGiftCard(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM GiftCard
                    WHERE ID_GiftCard = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditGiftCard(string id, string number, string givingdate, string nominal, string clientid, string workerid)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE GiftCard
                    SET Number = @number, GivingDate = @givingdate, Nominal = @nominal, Client_ID = @clientid, Worker_ID = @workerid
                    WHERE ID_GiftCard = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@number", number);
            command.Parameters.AddWithValue("@givingdate", givingdate);
            command.Parameters.AddWithValue("@nominal", nominal);
            command.Parameters.AddWithValue("@clientid", clientid);
            command.Parameters.AddWithValue("@workerid", workerid);

            DBCore.ExecuteCommand(command);
        }


        public static void AddGiftCard(string number, string givingdate, string nominal, string clientid, string workerid)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO GiftCard (Number, GivingDate, Nominal, Client_ID, Worker_ID)
                    VALUES (@number, @givingdate, @nominal, @clientid, @workerid);"
            };

            command.Parameters.AddWithValue("@number", number);
            command.Parameters.AddWithValue("@givingdate", givingdate);
            command.Parameters.AddWithValue("@nominal", nominal);
            command.Parameters.AddWithValue("@clientid", clientid);
            command.Parameters.AddWithValue("@workerid", workerid);

            DBCore.ExecuteCommand(command);
        }

    }
}