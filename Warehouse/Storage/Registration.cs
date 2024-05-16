using System.Data.SqlClient;
using Warehouse.Service;


namespace Warehouse.Storage
{
    internal class Registration
    {
        private Database database = new Database();

        public int CountUsersWithLogin(string username)
        {
            database.Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}'", database.getSqlConnection());
            int count = (int)command.ExecuteScalar();
            database.Connection();
            return count;
        }

        public void RegisterAccount(string username, string password, string role, string surname, string firstName, string middleName)
        {
            string query = $"INSERT INTO Account (username, password, role, surname, first_name, middle_name) VALUES ('{username.ToLower()}', '{PasswordEncoder.GetSHA256Hash(password)}', '{role}', N'{surname.ToLower()}', N'{firstName.ToLower()}', N'{middleName.ToLower()}')";
            database.Update(query);
        }
    }
}
