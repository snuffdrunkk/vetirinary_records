using System.Data;
using System.Windows.Controls;
using Warehouse.DTO;


namespace Warehouse.Storage
{
    internal class DriverStorage
    {
        private Database database = new Database();

        private string selectDriver = $"select driver.driver_id, car.car_number, driver.address, driver.phone_number, driver.surname_driver, driver.first_name, driver.middle_name, driver.medical_certificate from driver, car WHERE driver.car_id = car.car_id";

        public void CreateSupplier(string address, string phoneNumber, string surname, string firstName, string middleName, string medCertificate, ComboBoxDTO dto)
        {
            database.Update($"insert into driver (car_id, address, phone_number, surname_driver, first_name, middle_name, medical_certificate) values ('{dto.id}', N'{address}', '{phoneNumber}', N'{surname}', N'{firstName}', N'{middleName}', N'{medCertificate}')");
        }

        public void UpdateSupplier(long id, string title, string address, string phoneNumber, string surname, string firstName, string middleName)
        {
            database.Update($"update supplier set title = N'{title}', address = N'{address}', phone_number = '{phoneNumber}', surname = N'{surname}', first_name = N'{firstName}', middle_name = N'{middleName}' where supplier_id = '{id}'");
        }

        public void DeleteSupplier(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM Supplier Where supplier_id = {selectedRow.Row.ItemArray[0]}");
        }
        public void ReadSupplier(DataGrid grid)
        {
            database.Select(selectDriver, grid);
        }
    }
}
