using System.Data;
using System.Windows.Controls;

namespace Warehouse.Storage
{
    internal class StorekeeperStorage
    {
        private Database database = new Database();

        private string selectStorekeeper = $"select storekeeper_id, surname_storekeeper, first_name, middle_name, phone_number, address, medical_certificate from storekeeper";
        private string selectStorekeeperSurname = $"select storekeeper_id, surname_storekeeper from storekeeper";

        public void CreateStorekeeper(string surname_storekeeper, string first_name, string middle_name, string phone_number, string address, string medical_certificate)
        {
            database.Update($"insert into storekeeper (surname_storekeeper, first_name, middle_name, phone_number, address, medical_certificate) values (N'{surname_storekeeper}', N'{first_name}', N'{middle_name}', N'{phone_number}', N'{address}', N'{medical_certificate}')");
        }

        public void UpdateStorekeeper(long id, string surname_storekeeper, string first_name, string middle_name, string phone_number, string address, string medical_certificate)
        {
            database.Update($"update storekeeper set surname_storekeeper = N'{surname_storekeeper}', first_name = N'{first_name}', middle_name = N'{middle_name}', phone_number = N'{phone_number}', address = N'{address}', medical_certificate = N'{medical_certificate}' where storekeeper_id = '{id}'");
        }

        public void DeleteStorekeeper(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM storekeeper Where storekeeper_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadStorekeeper(DataGrid grid)
        {
            database.Select(selectStorekeeper, grid);
        }

        public void ReadStorekeeperSurnameToComboBox(ComboBox box)
        {
            database.ComboBoxToTable(selectStorekeeperSurname, box);
        }
    }
}
