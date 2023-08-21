using MySql.Data.MySqlClient;

namespace User
{
    public class DBConfig
    {
        public static MySqlConnection GetConnection()
        {
            string connectionString = "server=localhost;database=auditor;user=root;password=;";
            MySqlConnection  connection = new MySqlConnection(connectionString);
            return connection;
        }
    }
}
