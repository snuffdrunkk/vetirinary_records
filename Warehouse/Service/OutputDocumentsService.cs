﻿using System.Data.SqlClient;
using System.Windows;
using System;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Warehouse.Service
{
    internal class OutputDocumentsService
    {
        private Database database;

        private Application wordApplication;
        private Microsoft.Office.Interop.Word.Document document;

        private string templatePath = @"D:\ДИПЛОМ\warehouse-main\Warehouse\Resources\CarTemplate.docx";

        public OutputDocumentsService()
        {
            database = new Database();
        }

        public void generateCarWordDocument()
        {
            try
            {
                wordApplication = new Application();
                document = wordApplication.Documents.Open(templatePath);

                database = new Database();
                
                using (SqlConnection connection = database.getSqlConnection())
                {
                    string tableQuery = $"SELECT car_check_id, car.car_number, account.username, car_check_date, arrival_date, admission_car, car_temperature FROM car_check, car, account WHERE car.car_id = car_check.car_id AND car_check.account_id = account.account_id";

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(tableQuery, connection))
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        int index = 1;

                        while (reader.Read())
                        {
                            string date = Convert.ToDateTime(reader["car_check_date"]).ToString("dd.MM.yyyy");
                            string dateArv = Convert.ToDateTime(reader["arrival_date"]).ToString("dd.MM.yyyy");
                            string id = reader["car_check_id"].ToString();
                            string carN = reader["car_number"].ToString();
                            string username = reader["username"].ToString();
                            string adm = reader["admission_car"].ToString();
                            string temp = reader["car_temperature"].ToString();

                            replaceField($"{{i_{index}}}", id);
                            replaceField($"{{c_{index}}}", carN);
                            replaceField($"{{acc_{index}}}", username);
                            replaceField($"{{check_{index}}}", date);
                            replaceField($"{{a_{index}}}", dateArv);
                            replaceField($"{{ad_{index}}}", adm);
                            replaceField($"{{car_{index}}}", temp);

                            index++;
                        }

                        reader.Close();

                        for (int i = index; i <= 8; i++)
                        {
                            replaceField($"{{date_{i}}}", "");
                            replaceField($"{{num_{i}}}", "");
                            replaceField($"{{cow_quantity_{i}}}", "");
                            replaceField($"{{morning_{i}}}", "");
                            replaceField($"{{midday_{i}}}", "");
                            replaceField($"{{evening_{i}}}", "");
                            replaceField($"{{summary_{i}}}", "");
                        }
                    }

                    connection.Close();

                }

                string fileName = "Документ";

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
        }

        private void replaceField(string field, string value)
        {
            document.Content.Find.ClearFormatting();
            document.Content.Find.Execute(FindText: field, ReplaceWith: value);
        }
    }
}