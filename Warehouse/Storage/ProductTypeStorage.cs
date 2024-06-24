using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace Warehouse.Storage
{
    internal class ProductTypeStorage
    {
        private Database database = new Database();


        private string selectProductType = $"select product_type_id, type_name from product_type";

        public void CreateProductType(string title)
        {
            database.Update($"insert into product_type (type_name) values (N'{title.ToLower()}')");
        }

        public void UpdateProductType(long id, string title)
        {
            database.Update($"update product_type set type_name = N'{title}' where product_type_id = '{id}'");
        }

        public void ReadProductType(DataGrid grid)
        {
            database.Select(selectProductType, grid);
        }

        public void DeleteProductType(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM Product_type Where product_type_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadProductTypeToComboBox(ComboBox box)
        {
            database.ComboBoxToTable(selectProductType, box);
        }

        public bool CountTypeName(string typeName)
        {
            database.Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM product_type WHERE type_name ='{typeName}'", database.getSqlConnection());
            int count = (int)command.ExecuteScalar();
            database.Connection();
            return count == 0;
        }
    }
}
