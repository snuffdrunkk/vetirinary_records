using System.Data.SqlClient;
using System.Windows;
using Warehouse.Service;

namespace Warehouse.Storage
{
    internal class Authentif
    {
        private Database database = new Database();

        public bool Check(string username, string password)
        {
            database.Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}' AND Password='{PasswordEncoder.GetSHA256Hash(password)}'", database.getSqlConnection());
            int result = (int)command.ExecuteScalar();
            database.Connection();

            return result > 0;
        }
    }
}
