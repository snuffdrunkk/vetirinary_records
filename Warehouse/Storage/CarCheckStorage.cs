using System.Data;
using System.Windows.Controls;
using Warehouse.DTO;

namespace Warehouse.Storage
{
    internal class CarCheckStorage
    {
        private Database database = new Database();

        private string selectCarCheck = $"SELECT car_check.car_check_id, " +
            $"CONCAT(driver.surname_driver, ' (', car.car_number, ')') AS surname_driver, " +
            $"account.surname, " +
            $"car_check.car_check_date, " +
            $"car_check.arrival_date, " +
            $"car_check.admission_car, " +
            $"car_check.car_temperature " +
            $"FROM car_check JOIN driver ON car_check.driver_id = driver.driver_id " +
            $"JOIN account ON car_check.account_id = account.account_id " +
            $"JOIN car ON driver.car_id = car.car_id;";

        public void CreateCarCheck(string driverCheckDate, string arrivalDate, string admissionDriver, ComboBoxDTO drvName, ComboBoxDTO accName)
        {
            database.Update($"insert into driver_check (driver_id, account_id, driver_check_date, arrival_date, admission_driver) " +
                $"values ('{drvName.id}','{accName.id}','{driverCheckDate}', '{arrivalDate}', N'{admissionDriver}')");
        }

        public void DeleteCarCheck(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM car_check Where car_check_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadCarCheck(DataGrid grid)
        {
            database.Select(selectCarCheck, grid);
        }
    }
}
