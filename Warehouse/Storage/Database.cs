using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Warehouse.DTO;
using Warehouse.Model;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse
{
    internal class Database
    {
        private string connectionString;
        private SqlConnection sqlConnection;
        public Database()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            this.sqlConnection = new SqlConnection(this.connectionString);
        }

        public SqlConnection getSqlConnection()
        {
            return this.sqlConnection;
        }

        private string selectOrderAll = $"select order_id, order_id from ord";
        private string selectProductAll = $"select product_id, title from product";
        private string selectDriverAll = $"select driver_check_id, driver.surname_driver " +
                                         $"from driver, driver_check " +
                                         $"where driver.driver_id = driver_check.driver_id";
        private string selectCarAll = $"select car_check_id, car.car_number " +
                                      $"from car, car_check " +
                                      $"where car.car_id = car_check.car_id";
        private string selectStorekeeperAll = $"select storekeeper_check_id, storekeeper.surname_storekeeper from storekeeper, storekeeper_check where storekeeper.storekeeper_id = storekeeper_check.storekeeper_id";

        public void Connection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            else if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void Update(string query)
        {
            if (sqlConnection.State == ConnectionState.Closed)
                Connection();
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.ExecuteNonQuery();
            Connection();
        }

        public DataTable Select(string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

            DataTable data = new DataTable();
                adapter.Fill(data);
            return data;
        }

        public void Select(string query, DataGrid dataGrid)
        {
            Connection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
            Connection();
        }

        public int ReadProductCostById(long id)
        {
            Connection();

            SqlCommand command = new SqlCommand($"Select cost from product where product_id = {id}", sqlConnection);

            int cost = Convert.ToInt32(command.ExecuteScalar());

            Connection();

            return cost;
        }

        public int CountUsersWithLogin(string username)
        {
            Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}'", sqlConnection);
            int count = (int)command.ExecuteScalar();
            Connection();
            return count;
        }

        public bool CheckAdmin(string username)
        {
            Connection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Account WHERE username='{username}' AND role='ROLE_ADMIN'", sqlConnection);
            int result = (int)command.ExecuteScalar();
            Connection();

            return result > 0;
        }

        public void RegisterAccount(string username, string password, string role, string surname, string firstName, string middleName)
        {
            string query = $"INSERT INTO Account (username, password, role, surname, first_name, middle_name) VALUES ('{username.ToLower()}', '{PasswordEncoder.GetSHA256Hash(password)}', '{role}', N'{surname.ToLower()}', N'{firstName.ToLower()}', N'{middleName.ToLower()}')";
            Update(query);
        }

        public void UpdateUsername(string username)
        {
            string query = $"UPDATE Account SET username = '{username.ToLower()}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}'";
            Update(query);
        }

        public void UpdatePassword(string password)
        {
            string query = $"UPDATE Account SET password = '{PasswordEncoder.GetSHA256Hash(password)}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}'";
            Update(query);
        }

        public void UpdateUsernameAndPassword(string username, string password)
        {
            string query = $"UPDATE Account SET username = '{username.ToLower()}', password = '{password}' WHERE username = '{AuthManager.CurrentUsername.ToLower()}' AND password = '{PasswordEncoder.GetSHA256Hash(password)}'";
            Update(query);
        }

        public OrderedDictionary GetProductWithOrderId(string query)
        {
            OrderedDictionary productFromOrder = new OrderedDictionary();

            DataTable result = Select(query);

            foreach (DataRow row in result.Rows)
            {
                long orderId = (long)row["order_id"];
                long productId = (long)row["product_id"];
                string title = row["title"].ToString();

                // Создание объекта Product
                Product product = new Product
                {
                    id = productId,
                    title = title,
                    // Другие свойства продукта
                };

                // Добавление товара в список для данного заказа
                if (productFromOrder.Contains(orderId))
                {
                    List<Product> products = (List<Product>)productFromOrder[orderId];
                    products.Add(product);
                }
                else
                {
                    List<Product> products = new List<Product> { product };
                    productFromOrder.Add(orderId, products);
                }
            }

            return productFromOrder;
        }

        public long GetAccountId()
        {
            SqlCommand command = new SqlCommand($"Select account_id from account where username = '{AuthManager.CurrentUsername}'", sqlConnection);

            long id = Convert.ToInt64(command.ExecuteScalar());

            return id;
        }

        public void CreateOrder(ComboBoxDTO supplierDTO, double amount, string orderType, string orderDate, long carCheckId, long storeKeeperCheckId, string department, string arrival)
        {
            Connection();

            if (orderType.Equals("Выбытие"))
            {
                foreach (DictionaryEntry item in ComboBoxOrder.dicrtionaryWithId1)
                {
                    SqlCommand commandToQuantity = new SqlCommand($"SELECT presence FROM product WHERE product_id = '{(long)item.Key}'", sqlConnection);
                    decimal presence = (decimal)commandToQuantity.ExecuteScalar();

                    if (presence < (int)item.Value)
                    {
                        MessageBox.Show($"Количество на убытие указано не верно! Текущий остаток: {presence}");
                        return;
                    }
                }
            }

            SqlCommand command = new SqlCommand($"insert into ord (driver_check_id, account_id, amount, order_date, order_type, car_check_id, storekeeper_check_id, departure_point, arrival_point) output inserted.order_id values ('{supplierDTO.id}', '{GetAccountId()}', '{amount}', '{orderDate}', N'{orderType}', N'{carCheckId}', N'{storeKeeperCheckId}', N'{department}', N'{arrival}')", sqlConnection);
            long orderId = (long)command.ExecuteScalar();

            foreach (DictionaryEntry item in ComboBoxOrder.dicrtionaryWithId1)
            {
                Update($"insert into order_composition (product_id, order_id, quantity) values ('{(long)item.Key}', '{orderId}', '{(int)item.Value}')");
                if (orderType.Equals("Поступление"))
                    Update($"UPDATE product set presence = presence + '{(int)item.Value}' where product_id = '{(long)item.Key}'");
                if (orderType.Equals("Выбытие"))
                    Update($"UPDATE product set presence = presence - '{(int)item.Value}' where product_id = '{(long)item.Key}'");
            }

            Connection();
        }

        public void DeleteOrder(DataRowView selectedRow)
        {
            Connection();

            long orderId = (long)selectedRow.Row.ItemArray[0];

            string orderType = GetOrderType(orderId);

            Dictionary<long, int> products = GetOrderCompositionProducts(orderId);

            foreach (var product in products)
            {
                if (orderType.Equals("Поступление"))
                    Update($"UPDATE product SET presence = presence - {product.Value} WHERE product_id = {product.Key}");
                if (orderType.Equals("Выбытие"))
                    Update($"UPDATE product SET presence = presence + {product.Value} WHERE product_id = {product.Key}");
            }

            Update($"DELETE FROM order_composition WHERE order_id = {orderId}");
            Update($"DELETE FROM ord WHERE order_id = {orderId}");

            Connection();
        }

        private string GetOrderType(long orderId)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            SqlCommand command = new SqlCommand($"SELECT order_type FROM ord WHERE order_id = {orderId}", sqlConnection);

            return (string)command.ExecuteScalar();
        }

        private Dictionary<long, int> GetOrderCompositionProducts(long orderId)
        {

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            Dictionary<long, int> products = new Dictionary<long, int>();

            SqlCommand command = new SqlCommand($"SELECT product_id, quantity FROM order_composition WHERE order_id = {orderId}", sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                long productId = (long)reader["product_id"];
                int quantity = (int)reader["quantity"];
                products.Add(productId, quantity);
            }

            reader.Close();

            Connection();

            return products;
        }

        public void ReadProductToComboBox(ComboBox box)
        {
            ComboBoxToTable(selectProductAll, box);
        }

        public void ReadSupplierToComboBox(ComboBox boxi)
        {
            ComboBoxToTable(selectDriverAll, boxi);
        }
        public void ReadCarToComboBox(ComboBox boxi)
        {
            ComboBoxToTable(selectCarAll, boxi);
        }
        public void ReadStorekeeperToComboBox(ComboBox boxi)
        {
            ComboBoxToTable(selectStorekeeperAll, boxi);
        }

        public DataTable GetOrdersWithProducts()
        {
            Connection();

            string selectOrderWithoutProducts = "SELECT DISTINCT ord.order_id, driver.surname_driver, storekeeper.surname_storekeeper, car.car_number, account.surname, ord.amount, ord.order_date, ord.order_type, ord.departure_point, ord.arrival_point " +
                "FROM ord, storekeeper, storekeeper_check, driver, driver_check, car, car_check, account, order_composition " +
                "WHERE storekeeper.storekeeper_id = storekeeper_check.storekeeper_id " +
                "AND driver.driver_id = driver_check.driver_id " +
                "AND car.car_id = car_check.car_id " +
                "AND ord.account_id = account.account_id " +
                "AND driver_check.driver_check_id = ord.driver_check_id " +
                "AND car_check.car_check_id = ord.car_check_id " +
                "AND storekeeper_check.storekeeper_check_id = ord.storekeeper_check_id ";

            DataTable orderTable = Select(selectOrderWithoutProducts);
            orderTable.Columns.Add("product", typeof(DataTable));

            foreach (DataRow row in orderTable.Rows)
            {
                long orderId = (long)row["order_id"];
                string query = $"SELECT concat(product.title ,' ', quantity) as title FROM product JOIN order_composition ON product.product_id = order_composition.product_id WHERE order_composition.order_id = {orderId}";

                DataTable productTable = Select(query);
                
                row["product"] = productTable;
            }
            Connection();

            return orderTable;
        }

        public void ComboBoxToTable(string query, ComboBox box)
        {
            Connection();
            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            box.Items.Clear();
            while (reader.Read())
            {
                ComboBoxDTO dto = new ComboBoxDTO();
                dto.id = reader.GetInt64(0);
                dto.name = reader.GetString(1).ToString();
                box.Items.Add(dto);
            }
            reader.Close();
            Connection();
        }
    }
}