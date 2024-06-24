using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class DriverCheckAdd : Window
    {
        DataGrid data;
        DriverCheckStorage driverCheckStorage = new DriverCheckStorage();
        DriverStorage driverStorage = new DriverStorage();
        public DriverCheckAdd(DataGrid data)
        {
            InitializeComponent();
            this.data = data;
            driverStorage.ReadDriverSurnameToComboBox(DriverComboBox);

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            Date.SelectedDate = DateTime.Today;
            Date.IsEnabled = false;

            if (DateTime.Today < EndDate.SelectedDate)
            {
                DriverAdmissionBox.SelectedIndex = 1;
                DriverAdmissionBox.IsEnabled = false;
            }
            else
            {
                DriverAdmissionBox.SelectedIndex = 0;
                DriverAdmissionBox.IsEnabled = false;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO)DriverComboBox.SelectedItem;
            string driverCheckDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd");
            string startDate = StartDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            string endDate = EndDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            string admissionDriver = DriverAdmissionBox.Text;

            if (ValidationdDriverCheck(startDate, endDate, admissionDriver, dto))
            {
                driverCheckStorage.CreateDriverCheck(driverCheckDate, startDate, endDate, admissionDriver, dto);
                driverCheckStorage.ReadDriverCheck(data);

                this.Close();
            }
        }

        internal bool ValidationdDriverCheck(string startDate, string endDate, string admissionDriver,  ComboBoxDTO dto)
        {

            if (!ValidationStartDate(startDate))
                return false;


            if (!ValidationEndDate(endDate))
                return false;

            if (!ValidationCarAdmission(admissionDriver))
                return false;

            if (!ValidationDriver(dto))
                return false;

            if (!ValidateDateRange(startDate, endDate))
                return false;

            if (!ValidateStorekeeperAdmission(endDate))
                return false;

            return true;
        }

        private bool ValidationStartDate(string startDate)
        {
            if (string.IsNullOrEmpty(startDate))
            {
                MessageBox.Show("Пожалуйста, выберите дату начала действия справки.");
                return false;
            }

            DateTime parsedStartDate;
            if (!DateTime.TryParse(startDate, out parsedStartDate))
            {
                MessageBox.Show("Неправильный формат даты начала действия справки.");
                return false;
            }

            if (parsedStartDate > DateTime.Today)
            {
                MessageBox.Show("Дата начала действия справки не может быть больше сегодняшней даты.");
                return false;
            }

            return true;
        }

        private bool ValidationEndDate(string endlDate)
        {
            if (string.IsNullOrEmpty(endlDate))
            {
                MessageBox.Show("Пожалуйста, выберите дату окончания действия справки.");
                return false;
            }

            return true;
        }

        private bool ValidationCarAdmission(string admissionDriver)
        {
            if (string.IsNullOrEmpty(admissionDriver))
            {
                MessageBox.Show("Пожалуйста, введите допуск водителя.");
                return false;
            }

            return true;
        }

        internal bool ValidationDriver(ComboBoxDTO dto)
        {
            if (dto == null)
            {
                MessageBox.Show("Пожалуйста, выберите водителя.");
                return false;
            }

            return true;
        }

        private bool ValidateDateRange(string startDate, string endDate)
        {
            if (!DateTime.TryParse(startDate, out DateTime startDateTime) ||
                !DateTime.TryParse(endDate, out DateTime endDateTime))
            {
                MessageBox.Show("Неверный формат даты.");
                return false;
            }

            if (endDateTime.Year - startDateTime.Year != 1)
            {
                MessageBox.Show("Диапазон между датами должен быть равен 1 году.");
                return false;
            }

            return true;
        }

        private bool ValidateStorekeeperAdmission(string endDate)
        {
            if (!DateTime.TryParse(endDate, out DateTime endDateTime))
            {
                MessageBox.Show("Неверный формат даты.");
                return false;
            }

            if (DateTime.Today < endDateTime)
            {
                DriverAdmissionBox.SelectedIndex = 1;
                DriverAdmissionBox.IsEnabled = false;
                return true;
            }
            else
            {
                DriverAdmissionBox.SelectedIndex = 0;
                DriverAdmissionBox.IsEnabled = false;
                return true;
            }
        }
    }
}
