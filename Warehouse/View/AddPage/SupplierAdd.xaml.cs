using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class SupplierAdd : Window
    {
        DataGrid grid;
        Database database = new Database();
        CarStorage carStorage = new CarStorage();
        DriverStorage driverStorage = new DriverStorage();

        public SupplierAdd(DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;
            carStorage.ReadCarNumberToComboBox(CarNumberComboBox);

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO)CarNumberComboBox.SelectedItem;
            string address = SupplierAdressBox.Text;
            string phone = SupplierPhoneBox.Text;
            string surname = SurnameBox.Text;
            string firstName = FirstNameBox.Text;
            string middleName = MiddleNameBox.Text;
            string medCertificate = MedicalCertificateComboBox.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationSupplierAdd(address, phone, surname, firstName, middleName, medCertificate, dto))
            {
                driverStorage.CreateDriver(address, phone, surname, firstName, middleName, medCertificate, dto);
                driverStorage.ReadDriver(grid);
                this.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
