using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal class DBClient
    {
        public static DataTable GetClients()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Client as id,
                    Surname as Фамилия, 
                    Name as Имя, 
                    Patronymic as Отчество,
                    FORMAT(DBirth, 'dd/MM/yyyy', 'en-us') as [Дата рождения],
                    Phone as Телефон,
                    (CASE WHEN Gender <> 0 THEN 'Мужской' ELSE 'Женский' END) as Пол,
                    Discount as Скидка
                FROM Client"
            );
        }

        public static DataTable GetClient(string id)
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Client as id,
                    Surname as Фамилия, 
                    Name as Имя, 
                    Patronymic as Отчество,
                    FORMAT(DBirth, 'dd/MM/yyyy', 'en-us') as [Дата рождения],
                    Phone as Телефон,
                    (CASE WHEN Gender <> 0 THEN 'Мужской' ELSE 'Женский' END) as Пол,
                    Discount as Скидка
                FROM Client
                WHERE ID_Client = {id};"
            );
        }

        public static void DeleteClient(string id)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    DELETE FROM Client
                    WHERE ID_Client = @id;"
            };

            command.Parameters.AddWithValue("@id", id);

            DBCore.ExecuteCommand(command);
        }

        public static void EditClient(string id, string surname, string name, string patronymic, string dbirth, string phone, string gender, string discount)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    UPDATE Client
                    SET Surname = @surname, Name = @name, Patronymic = @patronymic, DBirth = @dbirth, Phone = @phone, Gender = @gender, Discount = @discount
                    WHERE ID_Client = @id;"
            };

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@patronymic", patronymic);
            command.Parameters.AddWithValue("@dbirth", dbirth);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@discount", discount);

            DBCore.ExecuteCommand(command);
        }

        public static void AddClient(string surname, string name, string patronymic, string dbirth, string phone, string gender, string discount)
        {
            var command = new SqlCommand
            {
                CommandText = $@"
                    INSERT INTO Client (Surname, Name, Patronymic, DBirth, Phone, Gender, Discount)
                    VALUES (@surname, @name, @patronymic, @dbirth, @phone, @gender, @discount);"
            };

            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@patronymic", patronymic);
            command.Parameters.AddWithValue("@dbirth", dbirth);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@discount", discount);

            DBCore.ExecuteCommand(command);
        }
    }
}