using System.Data.SqlClient;
using System;
using System.Windows;
using Application = Microsoft.Office.Interop.Word.Application;
using System.Windows.Media.Imaging;

namespace Warehouse.View.OutputDocuments
{
    public partial class StorekeeperOutputView : Window
    {
        private Database database;

        private Application wordApplication;
        private Microsoft.Office.Interop.Word.Document document;

        private string templatePath = @"D:\ДИПЛОМ\warehouse-main\Warehouse\Resources\StorekeeperTemplate.docx";

        public StorekeeperOutputView()
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

        public void generateStorekeeperWordDocument()
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
                    string tableQuery = $"SELECT storekeeper_check_id, storekeeper.surname_storekeeper, account.surname, storekeeper_check_date, sertificate_start_date, sertificate_end_date, admission_storekeeper " +
                        $"FROM storekeeper_check, storekeeper, account " +
                        $"WHERE storekeeper.storekeeper_id = storekeeper_check.storekeeper_id " +
                        $"AND storekeeper_check.account_id = account.account_id " +
                        $"AND storekeeper_check_date " +
                        $"BETWEEN '{firstDate}' AND '{secondDate}'";

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(tableQuery, connection))
                    {

                        SqlDataReader reader = command.ExecuteReader();

                        int index = 1;

                        while (reader.Read())
                        {
                            string date = Convert.ToDateTime(reader["storekeeper_check_date"]).ToString("dd.MM.yyyy");
                            string dateStart = Convert.ToDateTime(reader["sertificate_start_date"]).ToString("dd.MM.yyyy");
                            string dateEnd = Convert.ToDateTime(reader["sertificate_end_date"]).ToString("dd.MM.yyyy");
                            string id = reader["storekeeper_check_id"].ToString();
                            string drvN = reader["surname_storekeeper"].ToString();
                            string surname = reader["surname"].ToString();
                            string adm = reader["admission_storekeeper"].ToString();

                            replaceField($"{{i_{index}}}", id);
                            replaceField($"{{drv_{index}}}", drvN);
                            replaceField($"{{acc_{index}}}", surname);
                            replaceField($"{{check_{index}}}", date);
                            replaceField($"{{start_{index}}}", dateStart);
                            replaceField($"{{end_{index}}}", dateEnd);
                            replaceField($"{{adm_{index}}}", adm);

                            index++;
                        }

                        reader.Close();

                        for (int i = index; i <= 10; i++)
                        {
                            replaceField($"{{i_{i}}}", "");
                            replaceField($"{{drv_{i}}}", "");
                            replaceField($"{{acc_{i}}}", "");
                            replaceField($"{{check_{i}}}", "");
                            replaceField($"{{start_{i}}}", "");
                            replaceField($"{{end_{i}}}", "");
                            replaceField($"{{adm_{i}}}", "");
                        }
                    }

                    connection.Close();

                }

                string fileName = "Журнал проверок медосмотра кладовщика";

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
            generateStorekeeperWordDocument();
        }
    }
}
