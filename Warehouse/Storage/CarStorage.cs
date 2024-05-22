using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

        public void UpdateCar(long id, string mark, string scrutiny)
        {
            database.Update($"update car set mark = N'{mark}', scrutiny = N'{scrutiny}' where car_id = '{id}'");
        }

        public void DeleteCar(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM car Where car_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadCar(DataGrid grid)
        {
            database.Select(selectCar, grid);
        }

        public void ReadCarNumberToComboBox(ComboBox box)
        {
            database.ComboBoxToTable(selectCarNum, box);
        }

        public bool CountCarNum(string carNum)
        {
            database.Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM car WHERE car_number ='{carNum}'", database.getSqlConnection());
            int count = (int)command.ExecuteScalar();
            database.Connection();
            return count == 0;
        }

        public bool IsCarIdUsedInDriver(long carId)
        {
             database.Connection();
             SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM driver WHERE car_id ='{carId}'", database.getSqlConnection());
             int count = (int)command.ExecuteScalar();
             database.Connection();
             return count > 0;
        }
    }
}
