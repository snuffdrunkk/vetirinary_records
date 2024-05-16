using System.Windows.Controls;

namespace Warehouse.Storage
{

    internal class ProductTypeStore
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
    }
}
