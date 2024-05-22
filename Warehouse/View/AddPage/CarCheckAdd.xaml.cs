using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class CarCheckAdd : Window
    {
        DataGrid data;
        CarCheckStorage carCheckStorage = new CarCheckStorage();
        DriverStorage driverStorage = new DriverStorage();

        public CarCheckAdd(DataGrid data)
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

        private void ToSecondPage_Click(object sender, RoutedEventArgs e)
        {
            DriverComboBox.Visibility = Visibility.Collapsed;
            Date.Visibility = Visibility.Collapsed;
            Return.Visibility = Visibility.Collapsed;
            ToSecondPage.Visibility = Visibility.Collapsed;

            ArrivalDate.Visibility = Visibility.Visible;
            CarAdmissionBox.Visibility = Visibility.Visible;
            CarTemperatureBox.Visibility = Visibility.Visible;
            BackToFirstPage.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
        }

        private void BackToFirstPage_Click(object sender, RoutedEventArgs e)
        {
            DriverComboBox.Visibility = Visibility.Visible;
            Date.Visibility = Visibility.Visible;
            Return.Visibility = Visibility.Visible;
            ToSecondPage.Visibility = Visibility.Visible;

            ArrivalDate.Visibility = Visibility.Collapsed;
            CarAdmissionBox.Visibility = Visibility.Collapsed;
            CarTemperatureBox.Visibility = Visibility.Collapsed;
            BackToFirstPage.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO)DriverComboBox.SelectedItem;
            string carCheckDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd");
            string arrivalDate = ArrivalDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            string admissionCar = CarAdmissionBox.Text;
            string carTemperature = CarTemperatureBox.Text;

            if (ValidationCarCheck(carCheckDate, arrivalDate, admissionCar, carTemperature, dto)) 
            {
                carCheckStorage.CreateCarCheck(carCheckDate, arrivalDate, admissionCar, carTemperature, dto);
                carCheckStorage.ReadCarCheck(data);

                this.Close();
            }
        }

        internal bool ValidationCarCheck(string carCheckDate, string arrivalDate, string admissionCar, string carTemperature, ComboBoxDTO dto)
        {
            if (!ValidationCarCheckDate(carCheckDate))
                return false;

            if (!ValidationArrivalDate(arrivalDate))
                return false;

            if (!ValidationCarAdmission(admissionCar))
                return false;

            if (!ValidationCarTemperature(carTemperature))
                return false;

            if (!ValidationDriver(dto))
                return false;

            return true;
        }

        private bool ValidationCarCheckDate(string carCheckDate)
        {
            if (string.IsNullOrEmpty(carCheckDate))
            {
                MessageBox.Show("Пожалуйста, выберите дату водительского осмотра.");
                return false;
            }

            if (DateTime.Parse(carCheckDate) != DateTime.Today)
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

        private bool ValidationCarAdmission(string admissionCar)
        {
            if (string.IsNullOrEmpty(admissionCar))
            {
                MessageBox.Show("Пожалуйста, введите допуск осмотра автомобиля.");
                return false;
            }

            return true;
        }

        private bool ValidationCarTemperature(string carTemperature)
        {
            if (string.IsNullOrEmpty(carTemperature))
            {
                MessageBox.Show("Пожалуйста, введите температуру автомобиля.");
                return false;
            }

            if (!decimal.TryParse(carTemperature, out decimal temperature) || temperature < -18m || temperature > 4m)
            {
                MessageBox.Show("Температура автомобиля должна быть числом от -18°C до 4°C.");
                return false;
            }

            return true;
        }

        internal bool ValidationDriver(ComboBoxDTO dto)
        {
            if (dto == null)
            {
                MessageBox.Show("Пожалуйста, выберите водителя и машину.");
                return false;
            }

            return true;
        }
    }
}
