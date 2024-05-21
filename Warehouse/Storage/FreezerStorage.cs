﻿using System.Data;
using System.Windows.Controls;

namespace Warehouse.Storage
{
    internal class FreezerStorage
    {
        private Database database = new Database();

        private string selectFreezer = $"select freezer_id, freezer_name, freezer_description, freezer_volume from freezer";
        private string selectFreezerName = $"select freezer_id, freezer_name from freezer";

        public void CreateFreezer(string freezerName, string freezerDescr, string freezerVol)
        {
            database.Update($"insert into freezer (freezer_name, freezer_description, freezer_volume) values (N'{freezerName}', N'{freezerDescr}', '{freezerVol}')");
        }

        public void UpdateFreezer(long id, string freezerName, string freezerDescr, string freezerVol)
        {
            database.Update($"update freezer set freezer_name = N'{freezerName}', freezer_description = N'{freezerDescr}', freezer_volume = '{freezerVol}' where freezer_id = '{id}'");
        }

        public void DeleteFreezer(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM freezer Where freezer_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadFreezer(DataGrid grid)
        {
            database.Select(selectFreezer, grid);
        }

        public void ReadFreezerNameToComboBox(ComboBox box)
        {
            database.ComboBoxToTable(selectFreezerName, box);
        }
    }
}
