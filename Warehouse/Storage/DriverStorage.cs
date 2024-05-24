using System.Data;
using System.Windows.Controls;
using Warehouse.DTO;


namespace Warehouse.Storage
{
    internal class DriverStorage
    {
        private Database database = new Database();

        private string selectDriver = $"select driver.driver_id, driver.address, driver.phone_number, driver.surname_driver, driver.first_name, driver.middle_name from driver";
        private string selectDriverSurname = $"select driver.driver_id, driver.surname_driver from driver";

        public void CreateDriver(string address, string phoneNumber, string surname, string firstName, string middleName)
        {
            database.Update($"insert into driver (address, phone_number, surname_driver, first_name, middle_name) values (N'{address}', N'{phoneNumber}', N'{surname}', N'{firstName}', N'{middleName}')");
        }

        public void UpdateDriver(long id, string address, string phoneNumber, string surname, string firstName, string middleName)
        {
            database.Update($"update driver set address = N'{address}', phone_number = N'{phoneNumber}', surname_driver = N'{surname}', first_name = N'{firstName}', middle_name = N'{middleName}' where driver_id = '{id}'");
        }

        public void DeleteDriver(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM Driver Where driver_id = {selectedRow.Row.ItemArray[0]}");
        }
        public void ReadDriver(DataGrid grid)
        {
            database.Select(selectDriver, grid);
        }

        public void ReadDriverSurnameToComboBox(ComboBox box)
        {
            database.ComboBoxToTable(selectDriverSurname, box);
        }
    }
}
