using System.Data.SqlClient;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Application = Microsoft.Office.Interop.Word.Application;
using Warehouse.DTO;

namespace Warehouse.View.OutputDocuments
{
    public partial class TestimonyOutputView : Window
    {
        private Database database;

        private Application wordApplication;
        private Microsoft.Office.Interop.Word.Document document;

        private string templatePath = @"D:\ДИПЛОМ\warehouse-main\Warehouse\Resources\WaybillTemplate.docx";
        public TestimonyOutputView()
        {
            InitializeComponent();

            database = new Database();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            database.ComboBoxToTable("select ord.order_id, CAST(order_id AS VARCHAR) AS order_id from ord", WaybillComboBox);

            database = new Database();
        }

        public void generateCarWordDocument()
        {
            ComboBoxDTO contractor = (ComboBoxDTO)WaybillComboBox.SelectedItem;

            if (contractor != null)
            {
                try
                {
                    wordApplication = new Application();
                    document = wordApplication.Documents.Open(templatePath);

                    database = new Database();

                    using (SqlConnection connection = database.getSqlConnection())
                    {
                        string tableQuery = $"SELECT DISTINCT ord.order_id, driver.surname_driver, storekeeper.surname_storekeeper, car.car_number, account.surname, ord.amount, ord.order_date, ord.departure_point, ord.arrival_point " +
                                            "FROM ord, storekeeper, storekeeper_check, driver, driver_check, car, car_check, account, order_composition " +
                                            "WHERE storekeeper.storekeeper_id = storekeeper_check.storekeeper_id " +
                                            "AND driver.driver_id = driver_check.driver_id " +
                                            "AND car.car_id = car_check.car_id " +
                                            "AND ord.account_id = account.account_id " +
                                            "AND driver_check.driver_check_id = ord.driver_check_id " +
                                            "AND car_check.car_check_id = ord.car_check_id " +
                                            "AND storekeeper_check.storekeeper_check_id = ord.storekeeper_check_id " +
                                            $"AND ord.order_id = '{contractor.id}'";

                        connection.Open();

                        using (SqlCommand command = new SqlCommand(tableQuery, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            int index = 1;
                            while (reader.Read())
                            {
                                string date = Convert.ToDateTime(reader["order_date"]).ToString("dd.MM.yyyy");
                                string id = reader["order_id"].ToString();
                                string drv = reader["surname_driver"].ToString();
                                string storekeep = reader["surname_storekeeper"].ToString();
                                string car = reader["car_number"].ToString();
                                string acc = reader["surname"].ToString();
                                string amount = reader["amount"].ToString();
                                string dep = reader["departure_point"].ToString();
                                string arv = reader["arrival_point"].ToString();

                                replaceField($"{{date}}", date);
                                replaceField($"{{num}}", id);
                                replaceField($"{{driver}}", drv);
                                replaceField($"{{storekeeper}}", storekeep);
                                replaceField($"{{car}}", car);
                                replaceField($"{{acc}}", acc);
                                replaceField($"{{amount}}", amount);
                                replaceField($"{{departure}}", dep);
                                replaceField($"{{arrival}}", arv);
                                replaceField($"{{arrival}}", arv);

                                index++;
                            }

                            reader.Close();
                        }

                        string query = $"select string_agg(product.title + ' ' + cast(order_composition.quantity as varchar), ', ') as titles from order_composition join product on order_composition.product_id = product.product_id join ord on ord.order_id = order_composition.order_id where ord.order_id = '{contractor.id}'";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            int index = 1;
                            while (reader.Read())
                            {
                                string titles = reader["titles"].ToString();

                                replaceField($"{{product}}", titles);

                                index++;
                            }

                            reader.Close();
                        }

                        connection.Close();

                    }

                    string fileName = "Ветеринарное свидетельство";

                    string outputPath = @"D:\ДИПЛОМ\warehouse-main\Warehouse\Resources\" + fileName + ".docx";
                    document.SaveAs2(outputPath);
                    document.Close();
                    System.Diagnostics.Process.Start(outputPath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка. Закройте все экземпляры Microsoft Word и попробуйте снова!" + ex);

                    foreach (var process in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
                    {
                        process.Kill();
                    }

                    return;
                }
            } else
            {
                MessageBox.Show("Выберите накладную!");
                return;
            }
           
        }

        private void replaceField(string field, string value)
        {
            document.Content.Find.ClearFormatting();
            document.Content.Find.Execute(FindText: field, ReplaceWith: value);
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            generateCarWordDocument();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
