using System.Data.SqlClient;
using System;
using System.Windows;
using Application = Microsoft.Office.Interop.Word.Application;
using System.Windows.Media.Imaging;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System.Windows.Forms;
using Warehouse.View.AddPage;

namespace Warehouse.View.OutputDocuments
{
    internal class WaybillOutputView
    {
        private Database database;

        private Application wordApplication;
        private Microsoft.Office.Interop.Word.Document document;

        private string templatePath = @"D:\ДИПЛОМ\warehouse-main\Warehouse\Resources\WaybillTemplate.docx";

        public void generateCarWordDocument()
        {
            try
            {
                wordApplication = new Application();
                document = wordApplication.Documents.Open(templatePath);

                database = new Database();


                using (SqlConnection connection = database.getSqlConnection())
                {
                    string tableQuery = $"SELECT DISTINCT ord.order_id, driver.surname_driver, storekeeper.surname_storekeeper, car.car_number, account.surname, ord.amount, ord.order_date, ord.departure_point, ord.arrival_point" +
                                        "FROM ord, storekeeper, storekeeper_check, driver, driver_check, car, car_check, account, order_composition" +
                                        "WHERE storekeeper.storekeeper_id = storekeeper_check.storekeeper_id" +
                                        "AND driver.driver_id = driver_check.driver_id" +
                                        "AND car.car_id = car_check.car_id" +
                                        "AND ord.account_id = account.account_id" +
                                        "AND driver_check.driver_check_id = ord.driver_check_id" +
                                        "AND car_check.car_check_id = ord.car_check_id" +
                                        "AND storekeeper_check.storekeeper_check_id = ord.storekeeper_check_id ";

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(tableQuery, connection))
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        int index = 1;

                        while (reader.Read())
                        {
                            string date = Convert.ToDateTime(reader["ord.order_date"]).ToString("dd.MM.yyyy");
                            string id = reader["ord.order_id"].ToString();
                            string drv = reader["driver.surname_driver"].ToString();
                            string storekeep = reader["storekeeper.surname_storekeeper"].ToString();
                            string car = reader["car.car_number"].ToString();
                            string acc = reader["account.surname"].ToString();
                            string amount = reader["ord.amount"].ToString();
                            string dep = reader["ord.departure_point"].ToString();
                            string arv = reader["ord.arrival_point"].ToString();

                            replaceField($"{{date}}", date);
                            replaceField($"{{num}}", id);
                            replaceField($"{{driver}}", drv);
                            replaceField($"{{storekeeper}}", storekeep);
                            replaceField($"{{car}}", car);
                            replaceField($"{{acc}}", acc);
                            replaceField($"{{amount}}", amount);
                            replaceField($"{{departure}}", dep);
                            replaceField($"{{arrival}}", arv);

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
            catch
            {
                /*Show("Произошла ошибка. Закройте все экземпляры Microsoft Word и попробуйте снова!");*/

                foreach (var process in System.Diagnostics.Process.GetProcessesByName("WINWORD"))
                {
                    process.Kill();
                }

                return;
            }
        }

        private void replaceField(string field, string value)
        {
            document.Content.Find.ClearFormatting();
            document.Content.Find.Execute(FindText: field, ReplaceWith: value);
        }
    }
}
