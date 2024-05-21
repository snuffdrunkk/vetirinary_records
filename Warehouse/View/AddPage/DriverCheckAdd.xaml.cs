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
        Database database = new Database();
        DriverCheckStorage driverCheckStorage = new DriverCheckStorage();
        public DriverCheckAdd(DataGrid data)
        {
            InitializeComponent();
            this.data = data;

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
    }
}
