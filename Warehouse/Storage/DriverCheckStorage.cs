using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using Warehouse.DTO;

namespace Warehouse.Storage
{
    internal class DriverCheckStorage
    {
        private Database database = new Database();

        private string selectDriverCheck = $"select driver_check_id, " +
            $"driver.surname_driver, " +
            $"account.surname, " +
            $"driver_check_date, " +
            $"sertificate_start_date, " +
            $"sertificate_end_date, " +
            $"admission_driver " +
            $"from driver_check, driver, account " +
            $"Where driver_check.driver_id = driver.driver_id " +
            $"And driver_check.account_id = account.account_id";

        public void CreateDriverCheck(string driverCheckDate, string startDate, string endDate, string admissionDriver, ComboBoxDTO drvName)
        {
            database.Update($"insert into driver_check (driver_id, account_id, driver_check_date, sertificate_start_date, sertificate_end_date, admission_driver) " +
                $"values ('{drvName.id}','{GetAccountId()}', '{driverCheckDate}', '{startDate}', '{endDate}', N'{admissionDriver}')");
        }

        public void UpdateDriverCheck(long id, string driverCheckDate, string startDate, string endDate, string admissionDriver)
        {
            database.Update($"update driver_check " +
                $"set driver_check_date = '{driverCheckDate}', sertificate_start_date = '{startDate}', sertificate_end_date = '{endDate}', admission_driver = N'{admissionDriver}' " +
                $"where driver_check_id = '{id}'");
        }

        public void DeleteDriverCheck(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM driver_check Where driver_check_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadDriverCheck(DataGrid grid)
        {
            database.Select(selectDriverCheck, grid);
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
