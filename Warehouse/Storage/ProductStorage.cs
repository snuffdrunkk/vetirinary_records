using System.Data;
using System.Windows.Controls;
using Warehouse.DTO;

namespace Warehouse.Storage
{
    internal class ProductStorage
    {
        private Database database = new Database();

        private string selectProduct = $"select product.product_id, product_type.type_name, product.presence, product.cost, product.description, product.title, product.suitability from product, product_type WHERE product.product_type_id = product_type.product_type_id";

        public void CreateProduct(string title, double cost, string description, string suitability, ComboBoxDTO dto)
        {
            database.Update($"insert into product (product_type_id, presence, cost, description, title, suitability) values ('{dto.id}', '{0}', '{cost}', N'{description}', N'{title}', N'{suitability}')");
        }

        public void UpdateProduct(long id, string title, double cost, string description, string suitability)
        {
            database.Update($"update product set title = N'{title}', cost = '{cost}', description = N'{description}', suitability = N'{suitability}' where product_id = '{id}'");
        }

        public void DeleteProduct(DataRowView selectedRow)
        {
            database.Update($"DELETE FROM Product Where product_id = {selectedRow.Row.ItemArray[0]}");
        }

        public void ReadProduct(DataGrid grid)
        {
            database.Select(selectProduct, grid);
        }
    }
}
