using System.Data;
using System.Data.SqlClient;

namespace Salon
{
    internal static class DBWorker
    {
        public static DataTable GetWorkers()
        {
            return DBCore.GetData($@"
                SELECT 
                    ID_Worker as id,
                    Surname as ФИО,
                    Surname as Фамилия, 
                    Name as Имя, 
                    Patronymic as Отчество,
                    FORMAT(DBirth, 'dd/MM/yyyy', 'en-us') as [Дата рождения],
                    (CASE WHEN Gender <> 0 THEN 'Мужской' ELSE 'Женский' END) as Пол,
                    Exp as Опыт,
                    About as [О сотруднике]
                FROM Worker"
            );
        }
    }
}
