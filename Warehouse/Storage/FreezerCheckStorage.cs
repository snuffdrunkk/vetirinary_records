using System;
using System.Data;
using System.Data.SqlClient;
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

        public void CreateFreezerCheck(string freezerCheckDate, string washingMethod, string detergent, string detergentQuantity, string disinfectionMethod, string disinfectant, string disinfectantQuantity, ComboBoxDTO frzName)
        {
            database.Update($"insert into freezer_check (freezer_id, account_id, freezer_check_date, washing_method, detergent, detergent_quantity, disinfection_method, disinfectant, disinfectant_quantity) " +
                $"values ('{frzName.id}','{GetAccountId()}','{freezerCheckDate}', N'{washingMethod}', N'{detergent}', '{detergentQuantity}', N'{disinfectionMethod}', N'{disinfectant}', '{disinfectantQuantity}')");
        }

        public void UpdateFreezerCheck(long id, string washingMethod, string detergent, string detergentQuantity, string disinfectionMethod, string disinfectant, string disinfectantQuantity)
        {
            database.Update($"update freezer_check " +
                $"set washing_method = N'{washingMethod}', detergent = N'{detergent}', detergent_quantity = '{detergentQuantity}', disinfection_method = N'{disinfectionMethod}', disinfectant = N'{disinfectant}', disinfectant_quantity = '{disinfectantQuantity}' " +
                $"where freezerCheck_id = '{id}'");
        }

        public void DeleteFreezerCheck(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM freezer_check Where freezer_check_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadFreezerCheck(DataGrid grid)
        {
            database.Select(selectFreezerCheck, grid);
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
