using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using Warehouse.DTO;


namespace Warehouse.Storage
{
    internal class StorekeeperCheckStorage
    {
        private Database database = new Database();

        private string selectStorekeeperCheck = $"select storekeeper_check_id, " +
            $"storekeeper.surname_storekeeper, " +
            $"account.surname, " +
            $"storekeeper_check_date, " +
            $"sertificate_start_date, " +
            $"sertificate_end_date, " +
            $"admission_storekeeper " +
            $"from storekeeper_check, storekeeper, account " +
            $"Where storekeeper_check.storekeeper_id = storekeeper.storekeeper_id " +
            $"And storekeeper_check.account_id = account.account_id";

        public void CreateStorekeeperCheck(string storekeeperCheckDate, string startDate, string endDate, string admissionStorekeeper, ComboBoxDTO strName)
        {
            database.Update($"insert into storekeeper_check (storekeeper_id, account_id, storekeeper_check_date, sertificate_start_date, sertificate_end_date, admission_storekeeper) " +
                $"values ('{strName.id}','{GetAccountId()}', '{storekeeperCheckDate}', '{startDate}', '{endDate}', N'{admissionStorekeeper}')");
        }

        public void UpdateStorekeeperCheck(long id, string driverCheckDate, string startDate, string endDate, string admissionDriver)
        {
            database.Update($"update storekeeper_check " +
                $"set storekeeper_check_date = '{driverCheckDate}', sertificate_start_date = '{startDate}', sertificate_end_date = '{endDate}', admission_storekeeper = N'{admissionDriver}' " +
                $"where storekeeper_check_id = '{id}'");
        }

        public void DeleteStorekeeperCheck(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM storekeeper_check Where storekeeper_check_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadStorekeeperCheck(DataGrid grid)
        {
            database.Select(selectStorekeeperCheck, grid);
        }
        public long GetAccountId()
        {
            database.Connection();
            SqlCommand command = new SqlCommand($"Select account_id from account where username = '{AuthManager.CurrentUsername}'", database.getSqlConnection());

            long id = Convert.ToInt64(command.ExecuteScalar());

            database.Connection();

            return id;
        }
    }
}
