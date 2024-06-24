using System.Data.SqlClient;
using System;
using System.Windows;
using Application = Microsoft.Office.Interop.Word.Application;
using System.Windows.Media.Imaging;

namespace Warehouse.View.OutputDocuments
{
    public partial class CarOutputView : Window
    {
        private Database database;

        private Application wordApplication;
        private Microsoft.Office.Interop.Word.Document document;

        private string templatePath = @"D:\ДИПЛОМ\warehouse-main\Warehouse\Resources\CarTemplate.docx";

        public CarOutputView()
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

        public void generateCarWordDocument()
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
                    string tableQuery = $"SELECT car_check_id, car.car_number, account.surname, car_check_date, arrival_date, admission_car, car_temperature " +
                        $"FROM car_check, car, account " +
                        $"WHERE car.car_id = car_check.car_id " +
                        $"AND car_check.account_id = account.account_id " +
                        $"AND car_check_date " +
                        $"BETWEEN '{firstDate}' AND '{secondDate}'";

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
                            string surname = reader["surname"].ToString();
                            string adm = reader["admission_car"].ToString();
                            string temp = reader["car_temperature"].ToString();

                            replaceField($"{{i_{index}}}", id);
                            replaceField($"{{c_{index}}}", carN);
                            replaceField($"{{acc_{index}}}", surname);
                            replaceField($"{{check_{index}}}", date);
                            replaceField($"{{a_{index}}}", dateArv);
                            replaceField($"{{ad_{index}}}", adm);
                            replaceField($"{{car_{index}}}", temp);

                            index++;
                        }

                        reader.Close();

                        for (int i = index; i <= 10; i++)
                        {
                            replaceField($"{{i_{i}}}", "");
                            replaceField($"{{c_{i}}}", "");
                            replaceField($"{{acc_{i}}}", "");
                            replaceField($"{{check_{i}}}", "");
                            replaceField($"{{a_{i}}}", "");
                            replaceField($"{{ad_{i}}}", "");
                            replaceField($"{{car_{i}}}", "");
                        }
                    }

                    connection.Close();

                }

                string fileName = "Журнал проверок температуры машины водителя";

                string outputPath = @"D:\ДИПЛОМ\warehouse-main\Warehouse\Resources\" + fileName + ".docx";

                document.SaveAs2(outputPath);
                document.Close();

                System.Diagnostics.Process.Start(outputPath);

            }
            catch
            {
                MessageBox.Show("Произошла ошибка. Закройте все экземпляры Microsoft Word и попробуйте снова!");

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
            generateCarWordDocument();
        }
    }
}
