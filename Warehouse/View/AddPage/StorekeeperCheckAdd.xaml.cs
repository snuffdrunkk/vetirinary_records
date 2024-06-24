using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class StorekeeperCheckAdd : Window
    {
        DataGrid data;
        StorekeeperCheckStorage storekeeperCheckStorage = new StorekeeperCheckStorage();
        StorekeeperStorage storekeeperStorage = new StorekeeperStorage();
        public StorekeeperCheckAdd(DataGrid data)
        {
            InitializeComponent();

            this.data = data;
            storekeeperStorage.ReadStorekeeperSurnameToComboBox(StorekeeperComboBox);

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
                StorekeeperAdmissionBox.SelectedIndex = 1;
                StorekeeperAdmissionBox.IsEnabled = false;
            }
            else
            {
                StorekeeperAdmissionBox.SelectedIndex = 0;
                StorekeeperAdmissionBox.IsEnabled = false;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO)StorekeeperComboBox.SelectedItem;
            string storekeeperCheckDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd");
            string startDate = StartDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            string endDate = EndDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            string admissionStorekeeper = StorekeeperAdmissionBox.Text;

            if (ValidationdStorekeeperCheck(startDate, endDate, admissionStorekeeper, dto))
            {
                storekeeperCheckStorage.CreateStorekeeperCheck(storekeeperCheckDate, startDate, endDate, admissionStorekeeper, dto);
                storekeeperCheckStorage.ReadStorekeeperCheck(data);

                this.Close();
            }
        }

        internal bool ValidationdStorekeeperCheck(string startDate, string endDate, string admissionStorekeeper, ComboBoxDTO dto)
        {

            if (!ValidationStartDate(startDate))
                return false;

            if (!ValidationEndDate(endDate))
                return false;

            if (!ValidationStorekeeperAdmission(admissionStorekeeper))
                return false;

            if (!ValidationStorekeeper(dto))
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

        private bool ValidationStorekeeperAdmission(string admissionStorekeeper)
        {
            if (string.IsNullOrEmpty(admissionStorekeeper))
            {
                MessageBox.Show("Пожалуйста, введите допуск кладовщика.");
                return false;
            }

            return true;
        }

        internal bool ValidationStorekeeper(ComboBoxDTO dto)
        {
            if (dto == null)
            {
                MessageBox.Show("Пожалуйста, выберите кладовщика.");
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
                StorekeeperAdmissionBox.SelectedIndex = 1;
                StorekeeperAdmissionBox.IsEnabled = false;
                return true;
            }
            else
            {
                StorekeeperAdmissionBox.SelectedIndex = 0;
                StorekeeperAdmissionBox.IsEnabled = false;
                return true;
            }
        }
    }
}
