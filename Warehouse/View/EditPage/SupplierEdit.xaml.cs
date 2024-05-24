using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Service;
using Warehouse.Storage;
using Warehouse.View.AddPage;

namespace Warehouse.View.EditPage
{
    public partial class SupplierEdit : Window
    {
        long id;
        DataGrid grid;
        DriverStorage driverStorage = new DriverStorage();

        public SupplierEdit(long id, string address, string phoneNumber, string surname, string firstName, string middleName, DataGrid grid)
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            this.id = id;
            this.grid = grid;
            SupplierAdressBox.Text = address;
            SupplierPhoneBox.Text = phoneNumber;
            SurnameBox.Text = surname;
            FirstNameBox.Text = firstName;
            MiddleNameBox.Text = middleName;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string address = SupplierAdressBox.Text;
            string phone = SupplierPhoneBox.Text;
            string surname = SurnameBox.Text;
            string firstName = FirstNameBox.Text;
            string middleName = MiddleNameBox.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationSupplierEdit(address, phone, surname, firstName, middleName))
            {
                driverStorage.UpdateDriver(id, address, phone, surname, firstName, middleName);
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
