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
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO)DriverComboBox.SelectedItem;
            string driverCheckDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd");
            string arrivalDate = ArrivalDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            string admissionDriver = DriverAdmissionBox.Text;

            driverCheckStorage.CreateDriverCheck(driverCheckDate, arrivalDate, admissionDriver, dto);
            driverCheckStorage.ReadDriverCheck(data);

            this.Close();
        }

        internal bool ValidationCarCheck(string driverCheckDate, string arrivalDate, string admissionDriver,  ComboBoxDTO dto)
        {
            if (!ValidationDriverCheckDate(driverCheckDate))
                return false;

            if (!ValidationArrivalDate(arrivalDate))
                return false;

            if (!ValidationCarAdmission(admissionDriver))
                return false;

            if (!ValidationDriver(dto))
                return false;

            return true;
        }

        private bool ValidationDriverCheckDate(string driverCheckDate)
        {
            if (string.IsNullOrEmpty(driverCheckDate))
            {
                MessageBox.Show("Пожалуйста, выберите дату водительского осмотра.");
                return false;
            }

            if (DateTime.Parse(driverCheckDate) != DateTime.Today)
            {
                MessageBox.Show("Дата водительского осмотра должна быть сегодняшней.");
                return false;
            }

            return true;
        }

        private bool ValidationArrivalDate(string arrivalDate)
        {
            if (string.IsNullOrEmpty(arrivalDate))
            {
                MessageBox.Show("Пожалуйста, выберите дату прибытия автомобиля.");
                return false;
            }

            if (DateTime.Parse(arrivalDate) != DateTime.Today)
            {
                MessageBox.Show("Дата водительского осмотра должна быть сегодняшней.");
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
                MessageBox.Show("Пожалуйста, выберите водителя и.");
                return false;
            }

            return true;
        }
    }
}
