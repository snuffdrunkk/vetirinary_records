using System.Data;
using System.Windows.Controls;

namespace Warehouse.Storage
{
    internal class CarStorage
    {
        private Database database = new Database();

        private string selectCar = $"select car_id, car_number, mark, scrutiny from car";
        private string selectCarNum = $"select car_id, car_number from car";

        public void CreateCar(string number, string mark, string scrutiny)
        {
            database.Update($"insert into car (car_number, mark, scrutiny) values (N'{number}', N'{mark}', N'{scrutiny}')");
        }

        public void UpdateCar(long id, string number, string mark, string scrutiny)
        {
            database.Update($"update Car set car_number = N'{number}', mark = '{mark}', scrutiny = N'{scrutiny}' where car_id = '{id}'");
        }

        public void DeleteCar(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM Car Where car_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadCar(DataGrid grid)
        {
            database.Select(selectCar, grid);
        }

        public void ReadCarNumberToComboBox(ComboBox box)
        {
            database.ComboBoxToTable(selectCarNum, box);
        }

    }
}
