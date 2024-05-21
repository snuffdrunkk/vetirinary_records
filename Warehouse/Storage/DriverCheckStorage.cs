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
            $"arrival_date, " +
            $"admission_driver " +
            $"from driver_check, driver, account " +
            $"Where driver_check.driver_id = driver.driver_id " +
            $"And driver_check.account_id = account.account_id";

        public void CreateDriverCheck(string driverCheckDate, string arrivalDate, string admissionDriver, ComboBoxDTO drvName)
        {
            database.Update($"insert into driver_check (driver_id, account_id, driver_check_date, arrival_date, admission_driver) " +
                $"values ('{drvName.id}','{GetAccountId()}', '{driverCheckDate}', '{arrivalDate}', N'{admissionDriver}')");
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
