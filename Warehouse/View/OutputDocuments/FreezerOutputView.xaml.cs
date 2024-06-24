using System.Data.SqlClient;
using System;
using System.Windows;
using Application = Microsoft.Office.Interop.Word.Application;
using System.Windows.Media.Imaging;


namespace Warehouse.View.OutputDocuments
{
    public partial class FreezerOutputView : Window
    {
        private Database database;

        private Application wordApplication;
        private Microsoft.Office.Interop.Word.Document document;

        private string templatePath = @"D:\ДИПЛОМ\warehouse-main\Warehouse\Resources\FreezerTemplate.docx";

        public FreezerOutputView()
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            database = new Database();

            FirstDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            SecondDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        public void generateFreezerWordDocument()
        {
            try
            {
                wordApplication = new Application();
                document = wordApplication.Documents.Open(templatePath);

                database = new Database();

                string firstDate = "";
                string secondDate = "";

                try
                {
                    firstDate = FirstDate.SelectedDate.Value.ToString("yyyy-MM-dd");
                    secondDate = SecondDate.SelectedDate.Value.ToString("yyyy-MM-dd");

                    replaceField("{f_date}", firstDate);
                    replaceField("{s_date}", secondDate);
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Выберите дату!");
                    return;
                }

                using (SqlConnection connection = database.getSqlConnection())
                {
                    string tableQuery = $"SELECT freezer_check_id, freezer.freezer_name, account.surname, freezer_check_date, washing_method, detergent, detergent_quantity, disinfection_method, disinfectant, disinfectant_quantity " +
                                        $"FROM freezer_check, freezer, account " +
                                        $"WHERE freezer_check.freezer_id = freezer.freezer_id " +
                                        $"AND freezer_check.account_id = account.account_id " +
                                        $"AND freezer_check_date " +
                                        $"BETWEEN '{firstDate}' AND '{secondDate}'";

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(tableQuery, connection))
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        int index = 1;

                        while (reader.Read())
                        {
                            string date = Convert.ToDateTime(reader["freezer_check_date"]).ToString("dd.MM.yyyy");
                            string id = reader["freezer_check_id"].ToString();
                            string frzN = reader["freezer_name"].ToString();
                            string surname = reader["surname"].ToString();
                            string w_met = reader["washing_method"].ToString();
                            string det = reader["detergent"].ToString();
                            string detQ = reader["detergent_quantity"].ToString();
                            string d_met = reader["disinfection_method"].ToString();
                            string dis = reader["disinfectant"].ToString();
                            string disQ = reader["disinfectant_quantity"].ToString();

                            replaceField($"{{i_{index}}}", id);
                            replaceField($"{{frz_{index}}}", frzN);
                            replaceField($"{{acc_{index}}}", surname);
                            replaceField($"{{check_{index}}}", date);
                            replaceField($"{{wMet_{index}}}", w_met);
                            replaceField($"{{det_{index}}}", det);
                            replaceField($"{{detQ_{index}}}", detQ);
                            replaceField($"{{dMet_{index}}}", d_met);
                            replaceField($"{{dis_{index}}}", dis);
                            replaceField($"{{disQ_{index}}}", disQ);

                            index++;
                        }

                        reader.Close();

                        for (int i = index; i <= 10; i++)
                        {
                            replaceField($"{{i_{i}}}", "");
                            replaceField($"{{drz{i}}}", "");
                            replaceField($"{{acc_{i}}}", "");
                            replaceField($"{{check_{i}}}", "");
                            replaceField($"{{wMet{i}}}", "");
                            replaceField($"{{det{i}}}", "");
                            replaceField($"{{detQ{i}}}", "");
                            replaceField($"{{dMet{i}}}", "");
                            replaceField($"{{dis{i}}}", "");
                            replaceField($"{{disQ{i}}}", "");
                        }
                    }

                    connection.Close();

                }

                string fileName = "Журнал о мойке и дезинфекции морозильных камер";

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
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            generateFreezerWordDocument();
        }
    }
}
