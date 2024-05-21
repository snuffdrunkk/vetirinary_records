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
        Database database = new Database();
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
            string driverCheckDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd");
            string arrivalDate = ArrivalDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            string admissionCar = CarAdmissionBox.Text;
            string carTemperature = CarTemperatureBox.Text;

            carCheckStorage.CreateCarCheck(driverCheckDate, arrivalDate, admissionCar, carTemperature, dto);
            carCheckStorage.ReadCarCheck(data);

            this.Close();
        }
    }
}
