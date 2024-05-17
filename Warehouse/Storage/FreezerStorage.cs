using System.Data;
using System.Windows.Controls;

namespace Warehouse.Storage
{
    internal class FreezerStorage
    {
        private Database database = new Database();

        private string selectFreezer = $"select freezer_id, freezer_name, freezer_description, freezer_volume from freezer";
        private string selectFreezerName = $"select freezer_id, freezer_name from freezer";

        public void CreateCar(string freezerName, string freezerDescr, string freezerVol)
        {
            database.Update($"insert into freezer (freezer_name, freezer_description, freezer_volume) values (N'{freezerName}', N'{freezerDescr}', N'{freezerVol}')");
        }

        public void UpdateCar(long id, string number, string mark, string scrutiny)
        {
            database.Update($"update freezer set car_number = N'{number}', mark = '{mark}', scrutiny = N'{scrutiny}' where car_id = '{id}'");
        }

        public void DeleteCar(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM freezer Where freezer_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadCar(DataGrid grid)
        {
            database.Select(selectFreezer, grid);
        }

        public void ReadCarNumberToComboBox(ComboBox box)
        {
            database.ComboBoxToTable(selectFreezerName, box);
        }
    }
}
