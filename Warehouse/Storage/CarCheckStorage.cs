using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using Warehouse.DTO;

namespace Warehouse.Storage
{
    internal class CarCheckStorage
    {
        private Database database = new Database();

        private string selectCarCheck = $"SELECT car_check.car_check_id, " +
            $"car.car_number, " +
            $"account.surname, " +
            $"car_check.car_check_date, " +
            $"car_check.arrival_date, " +
            $"car_check.admission_car, " +
            $"car_check.car_temperature " +
            $"FROM car_check JOIN car ON car_check.car_id = car.car_id " +
            $"JOIN account ON car_check.account_id = account.account_id;";

        public void CreateCarCheck(string carCheckDate, string arrivalDate, string admissionCar, string carTemperature, ComboBoxDTO carNum)
        {
            database.Update($"insert into car_check (car_id, account_id, car_check_date, arrival_date, admission_car, car_temperature) " +
                $"values ('{carNum.id}','{GetAccountId()}','{carCheckDate}', '{arrivalDate}', N'{admissionCar}', '{carTemperature}')");
        }

        public void DeleteCarCheck(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM car_check Where car_check_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadCarCheck(DataGrid grid)
        {
            database.Select(selectCarCheck, grid);
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
