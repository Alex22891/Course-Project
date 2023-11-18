using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project
{
    class DatabaseClass
    {
        public int id { get; set; }
        public string model { get; set; }
        public int invn { get; set; }
        public string type { get; set; }
        public string owner { get; set; }
        public string date { get; set; }
        public string OldOwner { get; set; }
        public int Class { get; set; }


        MySqlConnectionStringBuilder connectionstring;

        public DatabaseClass()
        {
            connectionstring = new MySqlConnectionStringBuilder();

            connectionstring.Server = "localhost";
            connectionstring.Port = 3307;
            connectionstring.UserID = "root";
            connectionstring.Password = "";
            connectionstring.Database = "database_courseproject";
        }
        public List<DatabaseClass> ReadTechnique()
        {
            MySqlConnection connection = new MySqlConnection(connectionstring.ConnectionString);
            // Создаем список который будет возвращать данный метод
            List<DatabaseClass> Technique = new List<DatabaseClass>();

            // Формируем команду для выборки данных
            string CommandText = $"SELECT * FROM Technique;";

            // оборнем код в обработку исключений
            try
            {
                // Открываем подключение
                connection.Open();
                // Создаем объект класса и передаем в конструктор нашу команду.И  Коннект
                MySqlCommand command = new MySqlCommand(CommandText, connection);

                // Создаем reader 
                // Сохраняем в него результат выполнения команды ExecuteReader
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    // Построчно считываем данные
                    while (reader.Read())
                    {
                        Technique.Add(new DatabaseClass()
                        {
                            id = reader.GetInt32(0),
                            model = reader.GetString(1),
                            invn = reader.GetInt32(2),
                            type = reader.GetString(3),
                            owner = reader.GetString(4),
                            date = reader.GetString(5),
                            Class = reader.GetInt32(6)
                        });
                    }
                }

            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
            }

            //Закрываем подключение
            connection.Close();
            // Возвращаем наш список
            return Technique;
        }
        public List<DatabaseClass> ReadDecommissionedTechnique()
        {
            MySqlConnection connection = new MySqlConnection(connectionstring.ConnectionString);
            // Создаем список который будет возвращать данный метод
            List<DatabaseClass> Technique = new List<DatabaseClass>();

            // Формируем команду для выборки данных
            string CommandText = $"SELECT * FROM DecommissionedTechnique;";

            // оборнем код в обработку исключений
            try
            {
                // Открываем подключение
                connection.Open();
                // Создаем объект класса и передаем в конструктор нашу команду.И  Коннект
                MySqlCommand command = new MySqlCommand(CommandText, connection);

                // Создаем reader 
                // Сохраняем в него результат выполнения команды ExecuteReader
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    // Построчно считываем данные
                    while (reader.Read())
                    {
                        Technique.Add(new DatabaseClass()
                        {
                            id = reader.GetInt32(0),
                            model = reader.GetString(1),
                            type = reader.GetString(2),
                            OldOwner = reader.GetString(3)
                        });
                    }
                }

            }
            catch (Exception error)
            {
                System.Windows.MessageBox.Show(error.Message);
            }

            //Закрываем подключение
            connection.Close();
            // Возвращаем наш список
            return Technique;
        }
        public int AddTehnique(string model, string invn, string type, string date, string owner, string Class)
        {
            var check = -1;
            MySqlConnection connection = new MySqlConnection(connectionstring.ConnectionString);
            connection.Open();
            string cmdText = "INSERT INTO `database_courseproject`.`Technique`(`model`,`invn`,`type`,`date`,`owner`, `Class`)" +
                $"VALUES ('{model}','{invn}','{type}', '{date}', '{owner}', '{Class}');";
            MySqlCommand cmd = new MySqlCommand(cmdText, connection);
            check = cmd.ExecuteNonQuery();
            connection.Close();
            return check;
        }
    }
}
