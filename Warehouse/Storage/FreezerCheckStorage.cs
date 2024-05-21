using System.Data;
using System.Windows.Controls;
using Warehouse.DTO;

namespace Warehouse.Storage
{
    internal class FreezerCheckStorage
    {
        private Database database = new Database();

        private string selectFreezerCheck = $"select freezer_check_id, freezer.freezer_name, account.surname, freezer_check_date, washing_method, detergent, detergent_quantity, disinfection_method, disinfectant, disinfectant_quantity " +
            $"from freezer_check, freezer, account " +
            $"Where freezer_check.freezer_id = freezer.freezer_id " +
            $"And freezer_check.account_id = account.account_id";

        public void CreateFreezerCheck(string freezerCheckDate, string washingMethod, string detergent, string detergentQuantity, string disinfectionMethod, string disinfectant, string disinfectantQuantity, ComboBoxDTO frzName, ComboBoxDTO accName)
        {
            database.Update($"insert into freezer_check (freezer_id, account_id, freezer_check_date, washing_method, detergent, detergent_quantity, disinfection_method, disinfectant, disinfectant_quantity) " +
                $"values ('{frzName.id}','{accName.id}','{freezerCheckDate}', N'{washingMethod}', '{detergentQuantity}', N'{disinfectionMethod}', N'{disinfectant}', '{disinfectantQuantity}')");
        }

        public void UpdateFreezerCheck(long idFrz, long idAcc, string freezerName, string accountName, string freezerCheckDate, string washingMethod, string detergent, string detergentQuantity, string disinfectionMethod, string disinfectant, string disinfectantQuantity)
        {
            database.Update($"update freezer_check " +
                $"set freezer_name = N'{freezerName}', surname = N'{accountName}', freezer_check_date = '{freezerCheckDate}', washing_method = N'{washingMethod}', detergent = N'{detergent}', detergent_quantity = '{detergentQuantity}', disinfection_method = N'{disinfectionMethod}', disinfectant = N'{disinfectant}', disinfectant_quantity = '{disinfectantQuantity}' " +
                $"where freezer_id = '{idFrz}'" +
                $"and account_id = '{idAcc}'");
        }

        public void DeleteFreezerCheck(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM freezer_check Where freezer_check_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadFreezerCheck(DataGrid grid)
        {
            database.Select(selectFreezerCheck, grid);
        }

    }
}
